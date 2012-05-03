using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
 

namespace FormCallingWPF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ElementHost elhost = new ElementHost();
            elhost.Size = new Size(1024,768);
            elhost.Location = new Point(0, 0);

            HerculesWPFMain.UserControlMain wpfctl = new HerculesWPFMain.UserControlMain();
            //HerculesWpfControlLibrary.UserControlBlank wpfctl = new HerculesWpfControlLibrary.UserControlBlank();
            //WpfControlLibrary1.UserControl1 wpfctl = new WpfControlLibrary1.UserControl1();
            //UserControl1 wpfctl = new UserControl1();
            //MyWPFControl wpfctl = new MyWPFControl();
            elhost.Child = wpfctl;

            this.Controls.Add(elhost);
 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //UserControlTest tst = new UserControlTest();
            
            //this.Controls.Add(tst);

        }

        private void elementHost1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
    }
}
