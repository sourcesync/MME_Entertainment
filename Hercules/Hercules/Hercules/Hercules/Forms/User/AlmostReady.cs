using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MME.Hercules.Forms.User
{
    public partial class AlmostReady : Form
    {

        private Session currentSession;

        private bool started = false;

        private System.Timers.Timer timer = null;
        private System.Timers.ElapsedEventHandler timer_handler = null;
        private System.EventHandler timer_invoke_handler = null;


        public AlmostReady(Session currentSession)
        {
            InitializeComponent();

            this.currentSession = currentSession;

            this.timer = new System.Timers.Timer();
            this.timer_handler = new System.Timers.ElapsedEventHandler(timer_Elapsed);
            this.timer.Elapsed += this.timer_handler;
            this.timer_invoke_handler = new System.EventHandler(this.timer_Done);

        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.timer.Stop();
            this.Invoke(this.timer_invoke_handler);
        }

        void timer_Done(object sender, EventArgs e)
        {
            this.DoneOK();
        }

        private void AlmostReady_Load(object sender, EventArgs e)
        {
            

            // adjust form/pb sizes...
            Size sz = WindowUtility.GetScreenSize(Hercules.Properties.Resources.EMAIL_SCREEN);
            this.Size = sz;
            pb.Size = sz;

            if (ConfigUtility.IsDeveloperMode)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.Fixed3D;
            }

            pb.Load(string.Format("Skins\\{0}\\Screens\\almostready.jpg",
               ConfigUtility.Skin));

            sz = WindowUtility.GetScreenSize(Hercules.Properties.Resources.EMAIL_SCREEN);
            this.Size = sz;
            pb.Size = sz;

            
            this.Refresh();

            sz = WindowUtility.GetScreenSize(Hercules.Properties.Resources.EMAIL_SCREEN);
            this.Size = sz;
            pb.Size = sz;
            

            this.started = false;

            SoundUtility.Play("AlmostReady.wav");


            //  get timer args and start timer...
            string timeout = ConfigUtility.GetConfig(ConfigUtility.Config, "ALMOST_READY_MODE_TIMEOUT");
            int itime = 60 * 1000;
            if ( (timeout!=null)&&(timeout!="") )
                itime = int.Parse(timeout);
            this.timer.Interval = itime * 1000;
            this.timer.Start();
        }


        private void DoneOK()
        {
            this.timer.Stop();
            SoundUtility.Stop();
            this.DialogResult = DialogResult.OK;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.DoneOK();
        }

        private void pb_Click(object sender, EventArgs e)
        {
            // if ((MousePosition.X < 10) && (MousePosition.Y < 10))
            //    this.DoneOK();

        }

        private void pb_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                this.DoneOK();
        }


    }
}
