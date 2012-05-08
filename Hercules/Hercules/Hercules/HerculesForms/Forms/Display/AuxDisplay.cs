using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MME.Hercules.Forms.Display
{
    public partial class AuxDisplay : Form
    {
        public AuxDisplay()
        {
            InitializeComponent();
        }

        private void AuxDisplay_Load(object sender, EventArgs e)
        {
            WindowUtility.SetScreen(pb, "auxdisplaylogo.jpg");
        }
    }
}
