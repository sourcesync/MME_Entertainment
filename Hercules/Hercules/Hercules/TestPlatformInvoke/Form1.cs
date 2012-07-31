using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TestPlatformInvoke
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern int MessageBox(int hWnd, String text,
                            String caption, uint type);


            [DllImport("boxengine.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
            public static extern int fnboxengine();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                fnboxengine();
                MessageBox(0, "yo", "yo", 0);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }
    }
}
