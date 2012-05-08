using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Imaging;


namespace TransPanel
{
    /// <summary>
    /// Summary description for TransPanel.
    /// </summary>
    public class TransPanel : Panel
    {

        Timer Wriggler = new Timer();
        public Bitmap bm;

        public TransPanel()
        {
            //
            // TODO: Add constructor logic here
            //

            Wriggler.Tick += new EventHandler(TickHandler);
            //this.Wriggler.Interval = 500;
            //this.Wriggler.Enabled = true;

            //this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

       //ControlStyles.UserPaint |
      // ControlStyles.AllPaintingInWmPaint,
      // true);
            this.UpdateStyles();
        }

        protected void TickHandler(object sender, EventArgs e)
        {
            this.InvalidateEx();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT
                return cp;
            }
        }

        protected void InvalidateEx()
        {
            if (Parent == null)
                return;

            Rectangle rc = new Rectangle(this.Location, this.Size);
            Parent.Invalidate(rc, true);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //do not allow the background to be painted 
        }

        Random r = new Random();

        protected override void OnPaint(PaintEventArgs e)
        {
            /*
            int h = this.Height / 2;
            int w = this.Width / 2;

            Pen p = new Pen(Color.Black, 2);
            int x, y;
            for (x = 0, y = 0; x < w; x += w / 10, y += h / 10)
            {
                e.Graphics.DrawEllipse(p, x + r.Next(10) - 5, y + r.Next(10) - 5, this.Width - (2 * x), this.Height - (2 * y));
            }
             * */

            /*
            //gw
            ColorMatrix cm = new ColorMatrix();
            cm.Matrix00 = cm.Matrix11 = cm.Matrix22 = cm.Matrix44 = 1;
            cm.Matrix33 = 0.5f;
            ImageAttributes ia = new ImageAttributes();
            ia.SetColorMatrix(cm);

            b.CompositingMode = CompositingMode.SourceCopy;
            Graphics g = e.Graphics;
            IntPtr hb = bm.GetHbitmap();
            Image img = Image.FromHbitmap(hb);
            g.DrawImage(img, 0, 0);
            Image ImageBitmap = img;
            g.DrawImage(ImageBitmap,
                new Rectangle(0, 0, ImageBitmap.Width, ImageBitmap.Height),
                0, 0, ImageBitmap.Width, ImageBitmap.Height, GraphicsUnit.Pixel,
                ia);
            //g.DrawImage(ImageBitmap,
            //    new Rectangle(0, 0, ImageBitmap.Width, ImageBitmap.Height),

            img.Dispose();
            //gw
             * */

            float[][] matrixItems ={ 
            new float[] {1, 0, 0, 0, 0},
            new float[] {0, 1, 0, 0, 0},
            new float[] {0, 0, 1, 0, 0},
             new float[] {0, 0, 0, 0.5f, 0}, 
            new float[] {0, 0, 0, 0, 1}};
            ColorMatrix colorMatrix = new ColorMatrix(matrixItems);

            // Create an ImageAttributes object and set its color matrix.
            ImageAttributes imageAtt = new ImageAttributes();
            
            /*
            imageAtt.SetColorMatrix(
               colorMatrix,
               ColorMatrixFlag.Default,
               ColorAdjustType.Bitmap);
            */

            // First draw a wide black line.
            e.Graphics.DrawLine(
               new Pen(Color.Black, 25),
               new Point(10, 15),
               new Point(200, 15));

            /*
            // Now draw the semitransparent bitmap image.
            Bitmap bitmap = this.bm;
            int iWidth = bitmap.Width;
            int iHeight = bitmap.Height;
            e.Graphics.DrawImage(
               bitmap,
               new Rectangle(30, 0, iWidth, iHeight),  // destination rectangle
               0.0f,                          // source rectangle x 
               0.0f,                          // source rectangle y
               iWidth,                        // source rectangle width
               iHeight,                       // source rectangle height
               GraphicsUnit.Pixel,
               imageAtt);
            */



            //p.Dispose();
        }


    }
}