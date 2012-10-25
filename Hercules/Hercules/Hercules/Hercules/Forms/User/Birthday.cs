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
    public partial class Birthday : Form
    {

        private Session currentSession;

        public Birthday(Session currentSession)
        {
            InitializeComponent();

            this.currentSession = currentSession;
        }

        private void Birthday_Load(object sender, EventArgs e)
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

            pb.Load(string.Format("Skins\\{0}\\Screens\\birthdate.jpg",
               ConfigUtility.Skin));

            sz = WindowUtility.GetScreenSize(Hercules.Properties.Resources.EMAIL_SCREEN);
            this.Size = sz;
            pb.Size = sz;

            this.timeoutlabel.Visible = false;
            this.b1.Parent = pb;
            
            //this.b2.Parent = pb;
            //this.b3.Parent = pb;


            this.Refresh();

            sz = WindowUtility.GetScreenSize(Hercules.Properties.Resources.EMAIL_SCREEN);
            this.Size = sz;
            pb.Size = sz;
            //System.Windows.Forms.MessageBox.Show(sz.ToString());

            this.numericUpDown2.BackColor = ColorTranslator.FromHtml("#008751");
            this.numericUpDown2.Visible = false;
            this.numericUpDown2.Value = 1;
            this.numericUpDown3.BackColor = ColorTranslator.FromHtml("#008751");
            this.numericUpDown3.Visible = false;
            this.numericUpDown3.Value = 1;
            this.numericUpDown4.BackColor = ColorTranslator.FromHtml("#008751");
            this.numericUpDown4.Visible = false;
            this.numericUpDown4.Value = 1900;

            SoundUtility.Play("Start.wav");
        }

        private void b1_Click(object sender, EventArgs e)
        {

            //  Get date, calc age...
            int month = (int)this.numericUpDown2.Value;
            int day = (int)this.numericUpDown3.Value;
            int year = (int)this.numericUpDown4.Value;

            System.DateTime bd = new System.DateTime(year, month, day );
            
            System.DateTime now = System.DateTime.Now;


            if ((bd.Day == 1) && (bd.Month == 1) && (bd.Year == 1900))
            {
                this.label1.Text = "In order to proceed, you need to enter your birthdate.";
                this.alertbox.Visible = true;
                this.alertbox.BringToFront();
                return;
            }


            String str = bd.ToShortDateString();
            this.currentSession.BirthDate = bd;

            System.TimeSpan span = now.Subtract(bd);


            if (span.TotalDays < 21 * 365)
            {
                this.label1.Text = "In order to proceed, you must be at least 21 years old.";
                this.alertbox.BringToFront();
                alertbox.Visible = true;
                return;
            }
            else
            {
                SoundUtility.StopSpeaking();
                SoundUtility.Play(Hercules.Properties.SoundResources.SELECTION_BUTTON);
                //Thread.Sleep(1000);

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void b2_Click(object sender, EventArgs e)
        {
            SoundUtility.StopSpeaking();
            SoundUtility.Play(Hercules.Properties.SoundResources.SELECTION_BUTTON);
            Thread.Sleep(1000);

            //this.currentSession.MaxCopies = 2;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void b3_Click(object sender, EventArgs e)
        {
            SoundUtility.StopSpeaking();
            SoundUtility.Play(Hercules.Properties.SoundResources.SELECTION_BUTTON);
            Thread.Sleep(1000);

           // this.currentSession.MaxCopies = 3;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }


        private void colortypepb_MouseDown(object sender, MouseEventArgs e)
        {
            //PictureBox pbtype = (PictureBox)sender;
            //pbtype.BackColor = System.Drawing.Color.Red;
        }

        private void pb_MouseDown(object sender, MouseEventArgs e)
        {

            this.numericUpDown2.BackColor = ColorTranslator.FromHtml("#008751");
            this.numericUpDown2.Visible = true;
            this.numericUpDown3.BackColor = ColorTranslator.FromHtml("#008751");
            this.numericUpDown3.Visible = true;
            this.numericUpDown4.BackColor = ColorTranslator.FromHtml("#008751");
            this.numericUpDown4.Visible = true; 
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            this.alertbox.SendToBack();
            alertbox.Visible = false;
            return;
        }

        private void Birthday_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyValue == 27)
            {
                Application.Exit();
            }
        }

        private void Birthday_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 27)
            {
                Application.Exit();
            }
        }

    }
}
