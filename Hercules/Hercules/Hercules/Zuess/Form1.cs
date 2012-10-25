using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Zuess
{
    public partial class Form1 : Form
    {

        public System.Timers.Timer timer = null;
        public System.Timers.ElapsedEventHandler thandler = null;

        public delegate void UIStuffDelegate();
        public UIStuffDelegate myDelegate;

        public int fno = 0;
        public String path = "";
        public String image = "";

        public Form1()
        {
            InitializeComponent();
        }

        public Size GenerateImageDimensions(int currW, int currH, int destW, int destH)
        {
            //double to hold the final multiplier to use when scaling the image
            double multiplier = 0;

            //string for holding layout
            string layout;

            //determine if it's Portrait or Landscape
            if (currH > currW) layout = "portrait";
            else layout = "landscape";

            switch (layout.ToLower())
            {
                case "portrait":
                    //calculate multiplier on heights
                    if (destH > destW)
                    {
                        multiplier = (double)destW / (double)currW;
                    }

                    else
                    {
                        multiplier = (double)destH / (double)currH;
                    }
                    break;
                case "landscape":
                    //calculate multiplier on widths
                    if (destH > destW)
                    {
                        multiplier = (double)destW / (double)currW;
                    }

                    else
                    {
                        multiplier = (double)destH / (double)currH;
                    }
                    break;
            }

            //return the new image dimensions
            return new Size((int)(currW * multiplier), (int)(currH * multiplier));
        }

        //Resize the image
        private void SetImage(PictureBox pb)
        {
            try
            {
                //create a temp image
                Image img = pb.Image;

                //calculate the size of the image
                Size imgSize = GenerateImageDimensions(img.Width, img.Height, this.pictureBox1.Width, this.pictureBox1.Height);

                //create a new Bitmap with the proper dimensions
                Bitmap finalImg = new Bitmap(img, imgSize.Width, imgSize.Height);

                //create a new Graphics object from the image
                Graphics gfx = Graphics.FromImage(img);

                //clean up the image (take care of any image loss from resizing)
                gfx.InterpolationMode = 
                    System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                //empty the PictureBox
                pb.Image = null;

                //center the new image
                pb.SizeMode = PictureBoxSizeMode.CenterImage;

                //set the new image
                pb.Image = finalImg;
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void UIStuff()
        {
            if (image.EndsWith(".jpg"))
            {
                Bitmap bm = new Bitmap(this.image);
                this.pictureBox1.Image = bm;
                this.SetImage(this.pictureBox1);

                this.TopMost = true;
                this.FormBorderStyle = FormBorderStyle.None;
                this.Size = new Size(1024, 768);
            }
        }

        private void tcall(object o, System.Timers.ElapsedEventArgs args)
        {
            try
            {
                System.IO.DirectoryInfo dirinfo =
                    new System.IO.DirectoryInfo(this.path);
                System.IO.FileInfo[] files = dirinfo.GetFiles();

                System.IO.FileInfo f = files[fno];

                this.image = f.FullName;
                this.Invoke(new UIStuffDelegate(this.UIStuff));

                fno++;
                if (fno == files.Length) fno = 0;

            }
            catch
            {
                fno = 0;
            }
        }

        private void RestartTimer()
        {
            this.KillTimer();

            this.timer = new System.Timers.Timer();
            this.timer.Interval = 3000.0;
            this.thandler = new System.Timers.ElapsedEventHandler(this.tcall);
            this.timer.Elapsed += this.thandler;
            this.timer.Start();
        }

        private void KillTimer()
        {
            //this.TopMost = false;
            //this.FormBorderStyle = FormBorderStyle.Fixed3D;
            //this.Size = new Size(1024, 768);

            if (this.timer != null)
            {
                this.timer.Stop();
                this.timer.Elapsed -= this.thandler;
                this.timer.Dispose();
                this.timer = null;
            }
        }
               
        private void Form1_Click(object sender, EventArgs e)
        {
            this.KillTimer();

            Form2 frm = new Form2();
            frm.path = this.path;
            DialogResult res = frm.ShowDialog();
            if (res == DialogResult.OK)
            {
                //this.label1.Text = "Folder:  " + frm.path;
                this.path = frm.path;


                this.timer = new System.Timers.Timer();
                this.timer.Interval = 3000.0;
                this.thandler = new System.Timers.ElapsedEventHandler(this.tcall);
                this.timer.Elapsed += this.thandler;
                this.timer.Start();
                
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Form1_Click(sender, e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.Location = new Point(0, 0);

                this.path = "Images";

                this.tcall(null, null);

                this.RestartTimer();
            }
            catch
            {
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                char c = e.KeyChar;
                if ((int)c == 27)
                {
                    Application.Exit();
                }
            }
            catch
            {
            }
        }
    }
}
