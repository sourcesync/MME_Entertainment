using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing;

namespace FormCallingWPF
{
    class MyButton : Button
    {
        public System.Drawing.Bitmap bm;

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //do not allow the background to be painted 
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

        protected override void OnPaint(PaintEventArgs pevent)
        {

            float[][] matrixItems ={ 
            new float[] {1, 0, 0, 0, 0},
            new float[] {0, 1, 0, 0, 0},
            new float[] {0, 0, 1, 0, 0},
             new float[] {0, 0, 0, 0.5f, 0}, 
            new float[] {0, 0, 0, 0, 1}};
            ColorMatrix colorMatrix = new ColorMatrix(matrixItems);

            // Create an ImageAttributes object and set its color matrix.
            ImageAttributes imageAtt = new ImageAttributes();

            
            imageAtt.SetColorMatrix(
               colorMatrix,
               ColorMatrixFlag.Default,
               ColorAdjustType.Bitmap);
            

            // First draw a wide black line.
            //pevent.Graphics.DrawLine(
            //   new Pen(Color.Black, 25),
            //   new Point(10, 15),
            //   new Point(200, 15));



            
            // Now draw the semitransparent bitmap image.
            Bitmap bitmap = this.bm;
            int iWidth = bitmap.Width;
            int iHeight = bitmap.Height;
            pevent.Graphics.DrawImage(
               bitmap,
               new Rectangle(30, 0, iWidth, iHeight),  // destination rectangle
               0.0f,                          // source rectangle x 
               0.0f,                          // source rectangle y
               iWidth,                        // source rectangle width
               iHeight,                       // source rectangle height
               GraphicsUnit.Pixel,
               imageAtt);
            

            //base.OnPaint(pevent);
        }

    }

}
