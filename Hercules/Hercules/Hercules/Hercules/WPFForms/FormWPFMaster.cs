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
        }

        private void master_selected(int option)
        {
            if (option == 0) //menu
            {
            }
            else if (option == 1)//photo
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void FormWPFMaster_Load(object sender, EventArgs e)
        {
            this.ctlmaster.ShowMain();
        }
    }
}
