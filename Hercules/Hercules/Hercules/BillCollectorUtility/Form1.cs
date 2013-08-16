using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BillCollectorUtility
{
    public partial class Form1 : Form
    {
        MMEBillCollector.MMEBillCollector bc = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (bc != null) return;

            bc = new MMEBillCollector.MMEBillCollector("COM3");

            if (bc.init())
            {

            }
            else
            {
                bc = null;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (bc!=null) bc.finish();
            bc = null;
        }
    }
}
