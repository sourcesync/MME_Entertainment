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
    public partial class FormWPF : Form
    {
        public FormWPF()
        {
            InitializeComponent();
            
            HerculesWPFBlank.UserControlBlank wpfctl = new HerculesWPFBlank.UserControlBlank();
            ElementHost elhost = new ElementHost();
            elhost.Size = new Size(1024, 768);
            elhost.Location = new Point(0, 0);
            elhost.Child = wpfctl;
            this.Controls.Add(elhost);
        }

    }
}
