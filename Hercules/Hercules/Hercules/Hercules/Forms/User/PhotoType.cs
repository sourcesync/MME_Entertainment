using System;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;

namespace MME.Hercules.Forms.User
{
    public partial class PhotoType : Form
    {
        private DateTime now;
        private Thread thread;
        private Session currentSession;

        public PhotoType(Session currentSession)
        {
            InitializeComponent();
            this.currentSession = currentSession;
        }

        public void Timeout()
        {
            while (true)
            {
                TimeSpan ts = now - DateTime.Now;

                if (ts.Seconds < 1)
                {
                    SoundUtility.StopSpeaking();
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }

                if (ts.Seconds == 28)
                    SoundUtility.Play(Hercules.Properties.SoundResources.SELECT_PHOTO_TYPE);

                this.Invoke((MethodInvoker)delegate
                {
                    timeoutlabel.Text = string.Format(Hercules.Properties.Resources.MAKE_SELECTION_TEXT,
                        ts.Seconds,
                        ts.Seconds != 1 ? "s" : "");
                });

                Thread.Sleep(1000);
            }
        }

        private void PhotoType_Load(object sender, EventArgs e)
        {
            if (ConfigUtility.IsDeveloperMode)
                this.WindowState = FormWindowState.Normal;

            //gw
            if (this.color_choice == ColorType.BW_Color )
            {
                this.bwpb.Location = new System.Drawing.Point(256, 271);
                this.colorpb.Location = new System.Drawing.Point(530, 271);
                this.sb.Visible = false;
            }
            else
            {
                this.bwpb.Location = new System.Drawing.Point(256-150, 271);
                this.colorpb.Location = new System.Drawing.Point(530-150, 271);
                this.sb.Visible = true;
                this.sb.Location = new System.Drawing.Point(801-150, 271);
            }
            //gw

            WindowUtility.SetScreen(pb, Hercules.Properties.Resources.PHOTOTYPE_SCREEN);
            WindowUtility.SetScreen(bwpb, Hercules.Properties.Resources.SELECTBW_IMAGE);
            WindowUtility.SetScreen(colorpb, Hercules.Properties.Resources.SELECTCOLOR_IMAGE);

            //gw
            if (this.color_choice == ColorType.BW_Color_Sepia)
            {
                WindowUtility.SetScreen(sb, Hercules.Properties.Resources.SELECTSEPIA_IMAGE);
            }
            //gw

            bwpb.Click += new EventHandler(pb_Click);
            colorpb.Click += new EventHandler(pb_Click);
            //gw
            sb.Click += new EventHandler(pb_Click);
            //gw

            //gw
            if (this.color_choice == ColorType.BW_Color)
            {
                SoundUtility.Play(Hercules.Properties.SoundResources.SELECT_PHOTO_TYPE);
            }
            else if (this.color_choice == ColorType.BW_Color_Sepia)
            {
                SoundUtility.Play(Hercules.Properties.SoundResources.SELECT_PHOTO_TYPE_W_SEPIA);
            }
            //gw

            this.timeoutlabel.Parent = pb;
            now = DateTime.Now.AddSeconds(Convert.ToInt32(ConfigUtility.GetValue("Timeout")));

            this.timeoutlabel.Visible = false;

            //thread = new Thread(Timeout);
            //thread.Start();
        }

        private void PhotoType_FormClosed(object sender, FormClosedEventArgs e)
        {
          
        }

        private void pb_Click(object sender, EventArgs e)
        {
            PictureBox pbtype = (PictureBox)sender;

            // Set the color type for the session
            
            //gw
            // this.currentSession.SelectedColorType = (pbtype.Name.Equals("bwpb")) ? ColorType.BW : ColorType.Color;
            if (pbtype.Name.Equals( "bwpb" ))
                this.currentSession.SelectedColorType = ColorType.BW;
            else if (pbtype.Name.Equals("colorpb"))
                this.currentSession.SelectedColorType = ColorType.Color;
            else if (pbtype.Name.Equals("sb"))
                this.currentSession.SelectedColorType = ColorType.Sepia;

            // gw

            //thread.Abort();

            thread = null;

            SoundUtility.StopSpeaking();
            SoundUtility.Play(Hercules.Properties.SoundResources.SELECTION_BUTTON);
            Thread.Sleep(1000);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void colortypepb_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pbtype = (PictureBox)sender;
            pbtype.BackColor = System.Drawing.Color.Red;
        }

        private void bwpb_Click(object sender, EventArgs e)
        {

        }

        private void sb_Click(object sender, EventArgs e)
        {

        }
    }
}
