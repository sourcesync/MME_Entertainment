using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Zuess
{
    public partial class Form2 : Form
    {
        public String path = "";



        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult res = this.folderBrowserDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                this.path = this.folderBrowserDialog1.SelectedPath;

                this.label1.Text = "Folder: " + this.path;

                this.DialogResult = DialogResult.OK;

            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.label1.Text = "Folder: " + this.path;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
