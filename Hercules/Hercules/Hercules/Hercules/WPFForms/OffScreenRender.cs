using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GDIDB;

namespace MME.Hercules.WPFForms
{
    public partial class OffScreenRender : Form
    {
        public Class1 pnl;
        
        public OffScreenRender()
        {
            InitializeComponent();
            this.pnl = this.panel1;

        }

        private void OffScreenRender_Load(object sender, EventArgs e)
        {
            this.pnl.memGraphics.CreateDoubleBuffer(this.CreateGraphics(), this.ClientRectangle.Width, this.ClientRectangle.Height);
        }
    }
}
