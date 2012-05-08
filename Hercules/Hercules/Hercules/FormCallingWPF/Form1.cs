using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

using System.Windows.Media.Imaging;

namespace FormCallingWPF
{
    public partial class Form1 : Form
    {
        public HerculesWPFMaster.UserControl1 wpfctl;

        public Form1()
        {
            InitializeComponent();

            wpfctl = new HerculesWPFMaster.UserControl1();

           // wpfctl = new HerculesWPFMaster.UserControl1();

            this.elementHost1.Child = wpfctl;

            Bitmap bm = new Bitmap("c:\\tmp\\promo-icon-tight-small.png");
            this.pictureBox1.Image = bm;

            /*
            MyButton btn = new MyButton();
            btn.bm = bm;
            btn.Location = new System.Drawing.Point(this.button1.Location.X, this.button1.Location.Y+15);
            btn.Name = "transpControl2";
            btn.Size = new System.Drawing.Size(this.button1.Width+100, this.button1.Height+100);
            btn.TabIndex = 0;
            btn.Text = "transpControl2";
            btn.UseVisualStyleBackColor = true;
            //this.Controls.Add(btn);
            btn.BringToFront();
            */

            TransPanel.TransPanel ctl = new TransPanel.TransPanel();
            ctl.bm = bm;
            ctl.Location = new System.Drawing.Point(this.button1.Location.X+20, this.button1.Location.Y+20);
            ctl.Name = "transpControl2";
            ctl.Size = new System.Drawing.Size(bm.Width, bm.Height);
            ctl.TabIndex = 0;
            ctl.Text = "transpControl2";
            this.Controls.Add(ctl);
            ctl.BringToFront();

            /*
            this.tp = new FormTransparent();
            //this.Controls.Add(this.tp);
            this.tp.BringToFront();
            this.tp.Location = new System.Drawing.Point(this.button1.Location.X + 20,
                this.button1.Location.Y + 20);
            */

            this.elementHost1.BackColorTransparent = true;
            this.elementHost1.SendToBack();
        }

        FormTransparent tp = null;


        private void button1_Click(object sender, EventArgs e)
        {
            //UserControlTest tst = new UserControlTest();
            
            //this.Controls.Add(tst);

            BitmapSource src =  this.wpfctl.RenderToFile();
            Bitmap bm = BitmapConversion.ToWinFormsBitmap(src);

            

            this.button1.BackgroundImage = bm;
            this.pictureBox1.BackgroundImage = bm;

        }

        private void elementHost1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.tp.Show();
            //this.tp.BringToFront();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);

            e.Graphics.DrawLine(
               new Pen(Color.Yellow, 25),
               new Point(00, 515),
               new Point(300, 515));
        }

        private void elementHost1_RegionChanged(object sender, EventArgs e)
        {
            System.Console.WriteLine("changed");
        }
    }
}
