using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HerculesForms
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();

            
        }

        private void Test_Load(object sender, EventArgs e)
        {
            //this.webBrowser1.Location = "http://www.google.com";
            this.webBrowser1.Navigate("http://www.google.com");
        }
    }
}
