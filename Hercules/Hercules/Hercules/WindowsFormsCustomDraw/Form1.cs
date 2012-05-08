using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace WindowsFormsCustomDraw
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //Bitmap bm = new Bitmap("c:\\tmp\\Tulips.jpg");
            Bitmap bm = new Bitmap("c:\\tmp\\promo-icon-tight-small.png");

            String str = bm.PixelFormat.ToString();
            int sz = System.Drawing.Image.GetPixelFormatSize(bm.PixelFormat);

            /*
            this.transpControl1.bm = bm;
            this.SuspendLayout();
            this.transpControl1.Width = bm.Width;
            this.transpControl1.Height = bm.Height;
            this.ResumeLayout();
            */

            TransPanel.TransPanel ctl = new TransPanel.TransPanel();
            ctl.bm = bm;
            this.SuspendLayout();
            ctl.BackColor = System.Drawing.Color.Blue; // System.Drawing.Color.Transparent;
            //ctl.FillColor = System.Drawing.Color.White;
            ctl.Location = new System.Drawing.Point(0, 0);
            ctl.Name = "transpControl2";
            //ctl.Opacity = 100;
            ctl.Size = new System.Drawing.Size(bm.Width, bm.Height);
            ctl.TabIndex = 0;
            ctl.Text = "transpControl2";
            this.Controls.Add(ctl);
            this.ResumeLayout();
             
        }
    }
}
