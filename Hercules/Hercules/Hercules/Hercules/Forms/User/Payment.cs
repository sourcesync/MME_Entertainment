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
    public partial class Payment : Form
    {

        private Session currentSession;

        private bool started = false;

        public Payment(Session currentSession)
        {
            InitializeComponent();

            this.currentSession = currentSession;
        }

        private void Payment_Load(object sender, EventArgs e)
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

            pb.Load(string.Format("Skins\\{0}\\Screens\\payment.jpg",
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
            this.numericUpDown4.Value = 1950;

            this.started = false;

            SoundUtility.Play("Start.wav");

            //  Set bill callback stuff...
            MME.Hercules.Utility.BillCollector.bc.sync = this;
            MME.Hercules.Utility.BillCollector.bc.cb = new System.EventHandler(this.BillCollectorEvent);


        }

        private void BillCollectorEvent(object sender, EventArgs e)
        {
            String message = (String)sender;

            if ( (message.StartsWith("V")) || (message.StartsWith("C") ) )
            {
                 SoundUtility.Play("selection.wav");

                if ( message.EndsWith("0500") )
                {
                    MME.Hercules.Utility.BillCollector.bc.send_clear_command();
                    this.DialogResult = DialogResult.OK;
                }
            }

        }

        private void b1_Click(object sender, EventArgs e)
        {
            if (!started)
            {
                this.label1.Text = "Please enter your birthdate.";
                this.alertbox.Visible = true;
                this.alertbox.BringToFront();
                return;
            }


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
            this.started = true;
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
