using System;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;
using System.Drawing;

namespace MME.Hercules.Forms.User
{
    public partial class Email : Form
    {
        private Session currentSession;
        private Thread thread;
        public bool ispromo = false;
        public bool istable = false;
        public int mode = 0;

        public Email(Session currentSession)
        {
            Application.DoEvents();

            this.currentSession = currentSession;
            InitializeComponent();

            if (ConfigUtility.GetValue("BoothType") == "2")
            {
                istable = true;
            }

            this.mode = 0;
        }

        private void Email_Load(object sender, EventArgs e)
        {
            this.mode = 0;

            if (ConfigUtility.IsDeveloperMode)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.Fixed3D;
            }

            if (!ispromo)
            {
                this.Email_Load2(sender, e);
            }
            else
            {
                WindowUtility.SetScreen(pb, Hercules.Properties.Resources.SURVEY);
                this.keyboard.Visible = false;
                this.skip.Visible = false;
                this.finished.Visible = false;
                this.skipArea.Visible = false;
                this.textBox1.Visible = false;
                if (istable)
                {
                    Bitmap bm = Hercules.Properties.ImageResources.back_button;
                    this.pictureBoxBack.Image = bm;
                    this.pictureBoxBack.BackColor = System.Drawing.Color.Transparent;
                    this.pictureBoxBack.Parent = pb;
                    this.pictureBoxBack.Visible = true;
                }
            }
        }
        

        private void Email_Load2(object sender, EventArgs e)
        {
            this.keyboard.Visible = true;
            this.skip.Visible = true;
            this.finished.Visible = true;
            this.skipArea.Visible = true;
            this.textBox1.Visible = true;

            if (ConfigUtility.IsDeveloperMode)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.Fixed3D;
            }

            if (ispromo)
            {
                WindowUtility.SetScreen(pb, Hercules.Properties.Resources.PROMO_SCREEN);
            }
            else
            {
                WindowUtility.SetScreen(pb, Hercules.Properties.Resources.EMAIL_SCREEN);
            }
            keyboard.Parent = pb;
            keyboard.CurrentTextBox = textBox1;

            // adjust form/pb sizes...
            Size sz = WindowUtility.GetScreenSize(Hercules.Properties.Resources.EMAIL_SCREEN);
            this.Size = sz;
            pb.Size = sz;


            if (istable)
            {
                Bitmap bm = Hercules.Properties.ImageResources.backarrow;
                this.pictureBoxBack.Image = bm;
                this.pictureBoxBack.BackColor = System.Drawing.Color.Transparent;
                this.pictureBoxBack.Parent = pb;
                this.pictureBoxBack.Visible = true;
            }

            skipArea.Parent = pb;
            skip.Visible = (!ConfigUtility.GetConfig(ConfigUtility.Config, "ForceEmail").Equals("1"));

            // set skip button
            skipArea.Location = new Point(Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config,"SkipEmailButtonX")),
                              Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config,"SkipEmailButtonY")));

            skipArea.Width = Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config, "SkipEmailButtonWidth"));
            skipArea.Height = Convert.ToInt32(ConfigUtility.GetConfig(ConfigUtility.Config, "SkipEmailButtonHeight"));

            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "SkipEmailButtonColor")))
                skipArea.BackColor = ColorTranslator.FromHtml(ConfigUtility.GetConfig(ConfigUtility.Config, "SkipEmailButtonColor"));
            else
                skipArea.BackColor = Color.Transparent;


            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "FinishedEmailButtonColor")))
                finished.BackColor = ColorTranslator.FromHtml(ConfigUtility.GetConfig(ConfigUtility.Config, "FinishedEmailButtonColor"));
            else
                finished.BackColor = Color.Transparent;

            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "FinishedEmailButtonColor")))
                another.BackColor = ColorTranslator.FromHtml(ConfigUtility.GetConfig(ConfigUtility.Config, "FinishedEmailButtonColor"));
            else
                another.BackColor = Color.Transparent;

            // adjust keyboard location...
            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "KeyboardY")))
            {
                keyboard.Location =
                    new Point(keyboard.Location.X, keyboard.Location.Y +
                        int.Parse(ConfigUtility.GetConfig(ConfigUtility.Config, "KeyboardY")));



                this.Refresh();
                System.Threading.Thread.Sleep(1);
            }

            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "KeyboardDisplayY")))
            {
                this.textBox1.Location =
                    new Point(this.textBox1.Location.X, this.textBox1.Location.Y +
                        int.Parse(ConfigUtility.GetConfig(ConfigUtility.Config, "KeyboardDisplayY")));

                this.skip.Location =
                    new Point(this.skip.Location.X, this.skip.Location.Y +
                        int.Parse(ConfigUtility.GetConfig(ConfigUtility.Config, "KeyboardDisplayY")));
                //this.skip.Visible = true;

                this.finished.Location =
                    new Point(this.finished.Location.X, this.finished.Location.Y +
                        int.Parse(ConfigUtility.GetConfig(ConfigUtility.Config, "KeyboardDisplayY")));

                this.another.Location =
                    new Point(this.another.Location.X, this.another.Location.Y +
                        int.Parse(ConfigUtility.GetConfig(ConfigUtility.Config, "KeyboardDisplayY")));

                this.Refresh();
                System.Threading.Thread.Sleep(1);
            }

            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "KeyboardFinishY")))
            {
                int y = int.Parse(ConfigUtility.GetConfig(ConfigUtility.Config, "KeyboardFinishY"));
                this.finished.Location = 
                    new Point( this.finished.Location.X, y );
                this.skip.Location =
                    new Point(this.skip.Location.X, y);
                this.another.Location =
                    new Point(this.another.Location.X, y);
            }

            if (!this.ispromo)
            {
                thread = new Thread(PlayIntroSounds);
                thread.Start();
            }

        }

        private void PlayIntroSounds()
        {
            Thread.Sleep(800);
            SoundUtility.Play(Hercules.Properties.SoundResources.ENTER_EMAIL);
        }

        private void skip_Click(object sender, EventArgs e)
        {
            SoundUtility.StopSpeaking();
            SoundUtility.Play(Hercules.Properties.SoundResources.SELECTION_BUTTON);

            Thread.Sleep(1000);

            //this.textBox1.Text = "george@devnullenterprises.com";
            //this.Refresh();
            //Thread.Sleep(1000);

            this.currentSession.EmailAddress = this.textBox1.Text;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        public static bool isValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        private void finished_Click(object sender, EventArgs e)
        {
            if (!isValidEmail(this.textBox1.Text))
            {
                alertbox.Visible = true;
                return;
            }

            SoundUtility.StopSpeaking();

            if (!ispromo)
            {
                if (thread.ThreadState == ThreadState.Running)
                    thread.Abort();

                thread = null;
            }
            
            if (pb.Image != null)
                pb.Image.Dispose();

            // Changed to support multiple emails...
            this.currentSession.EmailAddress += (this.textBox1.Text + ";");
           
            SoundUtility.Play(Hercules.Properties.SoundResources.SELECTION_BUTTON);
            Thread.Sleep(1000);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

       

        private void Email_FormClosed(object sender, FormClosedEventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            alertbox.Visible = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

 

        private void skipArea_Click(object sender, EventArgs e)
        {
            if (!ispromo)
            {
                if (thread.ThreadState == ThreadState.Running)
                    thread.Abort();

                thread = null;
            }

            if (pb.Image != null)
                pb.Image.Dispose();

            SoundUtility.Play(Hercules.Properties.SoundResources.SELECTION_BUTTON);
            Thread.Sleep(1000);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void pictureBoxBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            return;
        }

        private void pb_Click(object sender, EventArgs e)
        {
            /*
            if (this.mode == 0)
            {
                this.mode = 1;
                this.Email_Load2(sender, e);
            }
             * */
        }

        private void Email_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyValue == 27)
            {
                Application.Exit();
            }
        }

        private void another_Click(object sender, EventArgs e)
        {
            if (!isValidEmail(this.textBox1.Text))
            {
                alertbox.Visible = true;
                return;
            }

            SoundUtility.StopSpeaking();

            if (!ispromo)
            {
                if (thread.ThreadState == ThreadState.Running)
                    thread.Abort();

                thread = null;
            }

            if (pb.Image != null)
                pb.Image.Dispose();


            this.currentSession.EmailAddress += (this.textBox1.Text + ";");

            SoundUtility.Play(Hercules.Properties.SoundResources.SELECTION_BUTTON);
            //Thread.Sleep(1000);


            this.DialogResult = System.Windows.Forms.DialogResult.Retry;
            
        }

    }
}
