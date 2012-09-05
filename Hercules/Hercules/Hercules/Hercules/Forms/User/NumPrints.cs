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
    public partial class NumPrints : Form
    {

        private Session currentSession;

        public NumPrints(Session currentSession)
        {
            InitializeComponent();

            this.currentSession = currentSession;
        }

        private void NumPrints_Load(object sender, EventArgs e)
        {
            if (ConfigUtility.IsDeveloperMode)
                this.WindowState = FormWindowState.Normal;

            WindowUtility.SetScreen(pb, "chooseprint.jpg");
                //Hercules.Properties.Resources.PHOTOTYPE_SCREEN);

            this.timeoutlabel.Visible = false;
            this.b1.Parent = pb;
            this.b2.Parent = pb;
            this.b3.Parent = pb;
        }

        private void b1_Click(object sender, EventArgs e)
        {

            SoundUtility.StopSpeaking();
            SoundUtility.Play(Hercules.Properties.SoundResources.SELECTION_BUTTON);
            Thread.Sleep(1000);

            this.currentSession.MaxCopies = 1;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void b2_Click(object sender, EventArgs e)
        {
            SoundUtility.StopSpeaking();
            SoundUtility.Play(Hercules.Properties.SoundResources.SELECTION_BUTTON);
            Thread.Sleep(1000);

            this.currentSession.MaxCopies = 2;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void b3_Click(object sender, EventArgs e)
        {
            SoundUtility.StopSpeaking();
            SoundUtility.Play(Hercules.Properties.SoundResources.SELECTION_BUTTON);
            Thread.Sleep(1000);

            this.currentSession.MaxCopies = 3;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }


        private void colortypepb_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pbtype = (PictureBox)sender;
            pbtype.BackColor = System.Drawing.Color.Red;
        }

    }
}
