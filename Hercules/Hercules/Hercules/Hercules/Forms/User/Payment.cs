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
        private System.EventHandler cb = null;

        public Payment(Session currentSession)
        {
            InitializeComponent();

            this.currentSession = currentSession;
            this.cb =  new System.EventHandler(this.BillCollectorEvent); 
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

            


            this.Refresh();

            sz = WindowUtility.GetScreenSize(Hercules.Properties.Resources.EMAIL_SCREEN);
            this.Size = sz;
            pb.Size = sz;
            

            this.started = false;

            SoundUtility.Play("Start.wav");

            //  Set bill callback stuff...
            MME.Hercules.Utility.BillCollector.bc.sync = this;
            MME.Hercules.Utility.BillCollector.bc.cb = this.cb;

            MME.Hercules.Utility.BillCollector.bc.send_status_command();

        }

        private void BillCollectorEvent(object sender, EventArgs e)
        {
            bool done = false;

            String message = (String)sender;

            if ( (message.StartsWith("V")) || (message.StartsWith("C") ) )
            {
                 SoundUtility.Play("selection.wav");

                if ( message.EndsWith("0500") )
                {
                    done = true;
                }
            }
            else if (message.StartsWith("Ready"))
            {
                if (message.EndsWith("0500"))
                {
                    SoundUtility.Play("selection.wav");
                    done = true;
                }
            }

            if (done)
            {
                //  Set bill callback stuff...
                MME.Hercules.Utility.BillCollector.bc.sync = null;
                MME.Hercules.Utility.BillCollector.bc.cb = null;
                MME.Hercules.Utility.BillCollector.bc.send_clear_command();
                this.DialogResult = DialogResult.OK;
            }

        }


    }
}
