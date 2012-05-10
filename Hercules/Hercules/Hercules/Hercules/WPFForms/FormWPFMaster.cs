using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace MME.Hercules.WPFForms
{
    public partial class FormWPFMaster : Form
    {
        HerculesWPFMaster.UserControl1 ctlmaster = null;
        ElementHost masterhost = null;
        public int option = 0;

        public FormWPFMaster()
        {
            InitializeComponent();

            this.ctlmaster = new HerculesWPFMaster.UserControl1();
            this.ctlmaster.evt = new HerculesWPFMaster.UserControl1.UserControlMasterDelegate(this.master_selected);
            this.ctlmaster.Visibility = System.Windows.Visibility.Visible;
            this.masterhost = new ElementHost();
            this.masterhost.Size = new Size(1024, 768);
            this.masterhost.Location = new Point(0, 0);
            this.masterhost.Child = this.ctlmaster;
            this.Controls.Add(this.masterhost);


            //  overlay buttons...

            
            Bitmap bm = Hercules.Properties.ImageResources.backarrow;

            this.SuspendLayout();
            this.pictureBox1.Image = bm;
            this.ResumeLayout();
            /*
            this.SuspendLayout();
            MyPictureBox pictureBox1 = new MyPictureBox();
            pictureBox1.Image = bm;
            pictureBox1.Location = new System.Drawing.Point(33, 83);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(1024, 768);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            //this.Controls.Add(pictureBox1);
            //pictureBox1.BringToFront();
            */

            /*
            TransPanel.TransPanel ctl = new TransPanel.TransPanel();
            ctl.bm = bm;
            ctl.Location = new System.Drawing.Point(0, 0);
            ctl.Name = "transpControl2";
            ctl.Size = new System.Drawing.Size(bm.Width, bm.Height);
            ctl.TabIndex = 0;
            ctl.Text = "transpControl2";
            this.Controls.Add(ctl);
            ctl.BringToFront();

            this.masterhost.SendToBack();
            */

            this.ResumeLayout();


            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            //this.Controls.Add(this.masterhost);
        }

        private void master_selected(int option)
        {
            if (option == -2) // blank
            {
                this.pictureBox1.Visible = false;
            }
            else if (option == -1) // main
            {
                this.pictureBox1.Visible = false;
            }
            if (option == 0) // menu
            {
                this.pictureBox1.Visible = false;
            }
            else if (option == 1) // photo
            {
                this.pictureBox1.Visible = false;
                this.DialogResult = DialogResult.OK;
            }
            else if (option == 2) // web
            {
                this.pictureBox1.Visible = true;
                //this.pictureBox1.Parent = this.masterhost;
                //this.pictureBox1.BringToFront();
            }
            else if (option == 3) // event
            {
                this.pictureBox1.Visible = false;
            }
            else if (option == 4) // promo...
            {
                this.pictureBox1.Visible = false;
                this.DialogResult = DialogResult.OK;
            }
            else if (option == 5) // checkin...
            {
                this.pictureBox1.Visible = false;
                this.DialogResult = DialogResult.OK;
            }

            this.option = option;
        }

        private void FormWPFMaster_Load(object sender, EventArgs e)
        {
            if (ConfigUtility.IsDeveloperMode)
                this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.ctlmaster.ShowMain();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.ctlmaster.ShowMain();
        }

    }
}
