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

        private System.Timers.Timer timer = null;
        private System.Timers.ElapsedEventHandler timer_handler = null;
        private System.EventHandler timer_invoke_handler = null;

        private String end_transaction_string = "0500";

        public Payment(Session currentSession)
        {
            InitializeComponent();

            this.currentSession = currentSession;
            this.cb =  new System.EventHandler(this.BillCollectorEvent);

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
            //  timer done...
            if (MME.Hercules.Utility.BillCollector.bc != null)
            {
                MME.Hercules.Utility.BillCollector.bc.sync = null;
                MME.Hercules.Utility.BillCollector.bc.cb = null;
            }
            this.DialogResult = DialogResult.Cancel;
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            //  get payment amount to look for...
            String val = ConfigUtility.GetValue("BillCollectorInitString");
            if ((val != null) && (val != ""))
            {
                this.end_transaction_string = val.Substring(1);
            }

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
            if (MME.Hercules.Utility.BillCollector.bc != null)
            {
                MME.Hercules.Utility.BillCollector.bc.sync = this;
                MME.Hercules.Utility.BillCollector.bc.cb = this.cb;
                MME.Hercules.Utility.BillCollector.bc.send_status_command();
            }

            //  get timer args and start timer...
            string timeout = ConfigUtility.GetConfig(ConfigUtility.Config, "PAYMENT_MODE_TIMEOUT");
            int itime = 60 * 1000;
            if ( (timeout!=null)&&(timeout!="") )
                itime = int.Parse(timeout);
            this.timer.Interval = itime * 1000;
            this.timer.Start();
        }

        private void BillCollectorEvent(object sender, EventArgs e)
        {
            bool done = false;

            String message = (String)sender;

            if ( (message.StartsWith("V")) || (message.StartsWith("C") ) )
            {
                 SoundUtility.Play("selection.wav");

                if ( message.EndsWith( this.end_transaction_string ) )
                {
                    done = true;
                }

                if (!done)
                {
                    try
                    {
                    String tail = message.Substring(message.Length - 4);
                    float cur_amount = float.Parse(tail);
                    float max_amount = float.Parse(this.end_transaction_string);
                    if (cur_amount >= max_amount)
                    {
                        done = true;
                    }
                    }
                    catch
                    {
                    }

                }
            }
            else if (message.StartsWith("Ready"))
            {
                if (message.EndsWith( this.end_transaction_string ) )
                {
                    SoundUtility.Play("selection.wav");
                    done = true;
                }

                if (!done)
                {
                    try
                    {
                        String tail = message.Substring(message.Length - 4);
                        float cur_amount = float.Parse(tail);
                        float max_amount = float.Parse(this.end_transaction_string);
                        if (cur_amount >= max_amount)
                        {
                            done = true;
                        }
                    }
                    catch
                    {
                    }
                }
            }

            if (done)
            {
                this.DoneOK();
            }
            else
            {
                //  restart the timer...
                this.timer.Stop();
                this.timer.Start();
            }

        }

        private void DoneOK()
        {
            this.timer.Stop();

            if (MME.Hercules.Utility.BillCollector.bc != null)
            {
                MME.Hercules.Utility.BillCollector.bc.sync = null;
                MME.Hercules.Utility.BillCollector.bc.cb = null;
                MME.Hercules.Utility.BillCollector.bc.send_clear_command();
            }

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
