using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WFScentTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool open1 = false;
        private bool open2 = false;

        private void button1_Click(object sender, EventArgs e)
        {
            if (!this.open1)
            {
                this.open1 = MME.Hercules.PhidgetUtility2.InitPhidgetBoard1(259354);
                if (!this.open1)
                {
                    System.Windows.Forms.MessageBox.Show("cannot open board 1");
                    return;
                }
            }


            MME.Hercules.PhidgetUtility2.Trigger(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (!this.open2)
            {
                this.open2 = MME.Hercules.PhidgetUtility2.InitPhidgetBoard2(259314);
                if (!this.open2)
                {
                    System.Windows.Forms.MessageBox.Show("cannot open board 2");
                    return;
                }
            }

            MME.Hercules.PhidgetUtility2.Trigger(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (!this.open2)
            {
                this.open2 = MME.Hercules.PhidgetUtility2.InitPhidgetBoard2(259314);
                if (!this.open2)
                {
                    System.Windows.Forms.MessageBox.Show("cannot open board 2");
                    return;
                }
            }

            MME.Hercules.PhidgetUtility2.Trigger(2);
        }
    }
}
