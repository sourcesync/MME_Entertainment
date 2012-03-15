using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MME.Hercules.Forms
{
    public partial class Error : Form
    {
        public Error(string msg)
        {
            InitializeComponent();

            this.msg.Text = msg;
        }

        private void Error_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                CameraUtility.Shutdown();
                Environment.Exit(0);
            }

        }
    }
}
