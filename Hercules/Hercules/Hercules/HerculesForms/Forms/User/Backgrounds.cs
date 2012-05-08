using System;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using System.Drawing;

namespace MME.Hercules.Forms.User
{
    public partial class Backgrounds : Form
    {
        private List<Background> bgs = new List<Background>();
        private DateTime now;
        private Thread thread;
        private Session currentSession;

        public Backgrounds(Session currentSession)
        {
            InitializeComponent();
            this.currentSession = currentSession;
        }

        public void LoadBackgrounds()
        {
            bgs.Clear();
            /*
            instructions.Text = string.Format(Hercules.Properties.Resources.CHOOSE_BG_INSTRUCTIONS_TEXT,
                ConfigUtility.PhotoCount,
                (ConfigUtility.PhotoCount == 1) ? "" : "s");
            */
            int startx = 185;
            int y = 188;
            int x = startx;
            int padding = 10;
            int xspacing = 18;
            int yspacing = 20;

            if (ConfigUtility.PhotoBackgrounds <= 2)
                y += 60;

            for (int i = 0; i < ConfigUtility.PhotoBackgrounds; i++)
            {
                //gw
                
                if (this.show_pose)
                {
                    PictureBox pose = new PictureBox();
                    pose.Name = "pose" + (i + 1).ToString();
                    string fname_base = "bg" + (i + 1).ToString();
                    pose.Load(string.Format("Skins\\{0}\\Backgrounds\\{1}pose.jpg",
                        ConfigUtility.Skin,
                        fname_base));
                    pose.Width = 139;
                    pose.Height = 115;
                    pose.Parent = pb;
                    pose.Visible = false;
                    this.Controls.Add(pose);
                }
                //gw
                    
                PictureBox bg = new PictureBox();

                bg.Name = "bg" + (i+1).ToString();
                bg.Load(string.Format("Skins\\{0}\\Backgrounds\\{1}preview.jpg", 
                    ConfigUtility.Skin,
                    bg.Name));
                bg.Width = 278 + (padding*2);
                bg.Height = 230 + (padding*2);
                bg.Parent = pb;
                bg.BackColor = System.Drawing.Color.Transparent;
                bg.Padding = new Padding(padding);

                if (i == 0) 
                    x -= bg.Width;

                if (i == 2)
                {
                    x = startx - bg.Width;
                    y += bg.Height + yspacing;
                }

                x += (bg.Width + xspacing);

                bg.Location = new Point(x, y);
                bg.Click += new EventHandler(bg_Click);

                // Counter
                Label counter = new Label();
                counter.Name = "lb" + (i + 1).ToString();
                counter.Font = new System.Drawing.Font("Arial", 28f, FontStyle.Bold);
                counter.ForeColor = System.Drawing.Color.Yellow;
                counter.Text = "";
                counter.Visible = false;
                counter.Location = new Point(bg.Location.X+20, bg.Location.Y+20);
                counter.Width = 40;
                counter.Height = 45;
                counter.BackColor = System.Drawing.Color.Transparent;
                counter.Parent = bg;

              
                Background background = new Background(bg, i+1);
                background.lb = counter;

                bgs.Add(background);
                
                this.Controls.Add(bg);
                this.Controls.Add(counter);

            }

            foreach (Background b in bgs)
                b.bg.BringToFront();
        }

        public void FinishedSelecting()
        {
            int count = 0;

            this.currentSession.SelectedBackgrounds.Clear();

            // if no backgrounds are selected, then use one of each
            if (TotalSelected == 0)
            {
                while (count < ConfigUtility.PhotoCount)
                {
                    foreach (Background bg in bgs)
                    {
                        count++;
                        this.currentSession.SelectedBackgrounds.Add(bg.id);
                    }
                }
            }
            else
            {    
                foreach (Hercules.Background bg in bgs)
                {
                    for (int i = 1; i <= bg.Count; i++)
                    {
                        this.currentSession.SelectedBackgrounds.Add(bg.id);
                    }
                }

                if (this.currentSession.SelectedBackgrounds.Count < ConfigUtility.PhotoCount)
                {
                    while (count < ConfigUtility.PhotoCount)
                    {
                        foreach (Hercules.Background bg in bgs)
                        {
                            if (bg.Count > 0)
                            {
                                count++;
                                this.currentSession.SelectedBackgrounds.Add(bg.id);
                            }
                        }
                    }

                }
            }

            foreach (Background b in bgs)
            {
                if (b.bg != null && b.bg.Image != null)
                    b.bg.Image.Dispose();
            }

            SoundUtility.StopSpeaking();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }


        public void bg_Click(Object sender, EventArgs e)
        {
            PictureBox bg = (PictureBox)sender;

            int index = Convert.ToInt32(bg.Name.Replace("bg", "")) - 1;

            bgs[index].Selected = true;
            bgs[index].Count++;

            bgs[index].bg.BackColor = (bgs[index].Selected) ? Color.Red : Color.Transparent;
            bgs[index].lb.Visible = true;
            bgs[index].lb.Text = bgs[index].Count.ToString();
            bgs[index].lb.BringToFront();

            this.Refresh();
            Thread.Sleep(1);

            if (TotalSelected == ConfigUtility.PhotoCount)
            {
                SoundUtility.PlaySync(Hercules.Properties.SoundResources.ITEM_SELECTED);
            }
            else
            {
                SoundUtility.Play(Hercules.Properties.SoundResources.ITEM_SELECTED);
            }

            //gw
            if (this.show_pose)
            {
                string ctl_name = "pictureBox" + (TotalSelected).ToString();
                System.Windows.Forms.Control[] ctl = this.Controls.Find( ctl_name, true );
                if (ctl.Length == 1)
                {
                    PictureBox pb = (PictureBox)ctl[0];
                    pb.Visible = true;
                    string pose_name = "pose" + (index + 1).ToString();
                    PictureBox pose = (PictureBox)this.Controls.Find(pose_name, true)[0];
                    //pose.Location = new Point(pb.Location.X, pb.Location.Y);
                    //pose.Visible = true;
                    pb.Image = pose.Image;
                }
            }
            //gw


            if (TotalSelected == ConfigUtility.PhotoCount)
            {
                FinishedSelecting();
            }
        }


        public void Timeout()
        {
            while (true)
            {
                TimeSpan ts = now - DateTime.Now;

                if (ts.Seconds < 1)
                    FinishedSelecting();
      
                this.Invoke((MethodInvoker)delegate
                {
                    timeoutlabel.Text = string.Format(Hercules.Properties.Resources.MAKE_SELECTION_TEXT,
                        ts.Seconds,
                        ts.Seconds != 1 ? "s" : "");
                });

                Thread.Sleep(1000);
            }
        }

        private void Backgrounds_Load(object sender, EventArgs e)
        {
            if (ConfigUtility.IsDeveloperMode)
                this.WindowState = FormWindowState.Normal;

            //gw
            this.show_pose = ConfigUtility.GetValue("SHOW_POSE_THUMBNAILS").Equals("1");
            //gw

            WindowUtility.SetScreen(pb, Hercules.Properties.Resources.CHOOSEBACKGROUND_SCREEN);

            SoundUtility.Play(Hercules.Properties.SoundResources.CHOOSE_BACKGROUND);
            LoadBackgrounds();
 
            this.timeoutlabel.Parent = pb;
            //instructions.Parent = pb;

            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "TEXT_COLOR")))
                timeoutlabel.ForeColor = ColorTranslator.FromHtml(ConfigUtility.GetConfig(ConfigUtility.Config, "TEXT_COLOR"));


            now = DateTime.Now.AddSeconds(Convert.ToInt32(ConfigUtility.GetValue("Timeout")));

            thread = new Thread(Timeout);
            thread.Start();
        }

        private void Backgrounds_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (thread != null)
                thread.Abort();

            foreach (Background background in bgs)
                if (background.bg.Image != null)
                    background.bg.Image.Dispose();
        }


        private int TotalSelected
        {
            get
            {
                int count = 0;
                foreach (Background b in bgs)
                    count += b.Count;

                return count;             
            }

        }

    }
}
