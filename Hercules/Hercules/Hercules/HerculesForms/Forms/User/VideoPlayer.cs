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
    public partial class VideoPlayer : Form
    {
        Session currentSession;

        public VideoPlayer(Session current)
        {
            this.currentSession = current;

            InitializeComponent();
        }

        private void TimerThread()
        {
            if (ConfigUtility.SequenceConfig != null)
            {
                if (!string.IsNullOrEmpty(ConfigUtility.GetConfig2(ConfigUtility.SequenceConfig, "page", "duration")))
                {
                    string sleepstr = ConfigUtility.GetConfig2(ConfigUtility.SequenceConfig, "page", "duration");
                    int sleepval = int.Parse(sleepstr);
                    Thread.Sleep(sleepval);
                }
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void VideoPlayer_Load(object sender, EventArgs e)
        {
            if (ConfigUtility.IsDeveloperMode)
                this.WindowState = FormWindowState.Normal;      

            webBrowser1.Navigate("file://" + Application.StartupPath + "\\Skins\\" + ConfigUtility.Skin + "\\playmovie.html");
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Thread thread = new Thread(TimerThread);
            thread.Start();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
