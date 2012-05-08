using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace TranspControl
{
    //[Designer(typeof(TranspControlDesigner))]
    public class TranspControl:Control
    {
        public bool drag = false;
        public bool enab = false;
        private Color fillColor = Color.White;
		private int opacity = 100;
        private int alpha;
        public Bitmap bm;
		
		public TranspControl()
		{
            //System.IO.Stream str = new System.IO.StreamReader( "c:\\tmp\\Tulips.jpg");
           
            
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, true);
            this.BackColor = Color.Transparent;
		}
		
		public Color FillColor
		{
			get
			{
				return this.fillColor;
			}
			set
			{
				this.fillColor = value;
                if (this.Parent != null) Parent.Invalidate(this.Bounds, true);
			}
		}
		
		public int Opacity
		{
			get
			{
				if (opacity > 100) {opacity = 100;}
				else if (opacity < 1) {opacity = 1;}
				return this.opacity;
			}
			set
			{
				this.opacity = value;
                if (this.Parent != null) Parent.Invalidate(this.Bounds, true);
			}
		}
		
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x20;
				return cp;
			}
		}
		
		protected override void OnPaint(PaintEventArgs e)
		{       
            Graphics g = e.Graphics;
			Rectangle bounds = new Rectangle(0, 0, this.Width-1, this.Height-1);

            Color frmColor = this.Parent.BackColor;
            Brush brushColor;
            Brush bckColor; 

            alpha = (opacity * 255) / 100;

            
            if (drag)
            {
                Color dragFillColor;
                Color dragBckColor;

                if (BackColor != Color.Transparent)
                {
                    int Rb = BackColor.R * alpha / 255 + frmColor.R * (255 - alpha) / 255;
                    int Gb = BackColor.G * alpha / 255 + frmColor.G * (255 - alpha) / 255;
                    int Bb = BackColor.B * alpha / 255 + frmColor.B * (255 - alpha) / 255;
                    dragBckColor = Color.FromArgb(Rb, Gb, Bb);
                }
                else dragBckColor = frmColor;

                if (fillColor != Color.Transparent)
                {
                    int Rf = fillColor.R * alpha / 255 + frmColor.R * (255 - alpha) / 255;
                    int Gf = fillColor.G * alpha / 255 + frmColor.G * (255 - alpha) / 255;
                    int Bf = fillColor.B * alpha / 255 + frmColor.B * (255 - alpha) / 255;
                    dragFillColor = Color.FromArgb(Rf, Gf, Bf);
                }
                else dragFillColor = dragBckColor;

                alpha = 255;
                brushColor = new SolidBrush(Color.FromArgb(alpha, dragFillColor));
                bckColor = new SolidBrush(Color.FromArgb(alpha, dragBckColor));                
            }
            else
            {
                Color color = fillColor;             
                brushColor = new SolidBrush(Color.FromArgb(alpha, color));
                bckColor = new SolidBrush(Color.FromArgb(alpha, this.BackColor));
            }

			Pen pen = new Pen(this.ForeColor);

             /*
            if (this.BackColor != Color.Transparent | drag)
            {               
                g.FillRectangle(bckColor, bounds);
            }

            if (FillColor != Color.Transparent | drag)
            {
                g.FillEllipse(brushColor, bounds);
            }
            else g.FillEllipse(new SolidBrush(Color.FromArgb(1, Color.White)), bounds);
             
            g.DrawEllipse(pen, bounds);
            */

            //gw
            ColorMatrix cm = new ColorMatrix();
            cm.Matrix00 = cm.Matrix11 = cm.Matrix22 = cm.Matrix44 = 1;
            cm.Matrix33 = 0.8f;
            ImageAttributes ia = new ImageAttributes();
            ia.SetColorMatrix(cm);

            IntPtr hb = bm.GetHbitmap();
            Image img = Image.FromHbitmap(hb);
            g.DrawImage(img, 0, 0);
            Image ImageBitmap = img;
            g.DrawImage(ImageBitmap,
                new Rectangle(0, 0, ImageBitmap.Width, ImageBitmap.Height),
                0, 0, ImageBitmap.Width, ImageBitmap.Height, GraphicsUnit.Pixel,
                ia);
            img.Dispose();
            //gw

			pen.Dispose();
			brushColor.Dispose();
            bckColor.Dispose();
            

			g.Dispose();
            base.OnPaint(e);
		}

        protected override void OnBackColorChanged(EventArgs e)
        {
            if (this.Parent != null) Parent.Invalidate(this.Bounds, true);
            base.OnBackColorChanged(e);         
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            this.Invalidate();
            base.OnParentBackColorChanged(e);
        }
    }

    /*
    internal class TranspControlDesigner : ControlDesigner
    {
        private TranspControl myControl;

        protected override void OnMouseDragMove(int x, int y)
        {
            myControl = (TranspControl)(this.Control);
            myControl.drag = true;
            base.OnMouseDragMove(x, y);
        }
      
        protected override void OnMouseLeave()
        {
            myControl = (TranspControl)(this.Control);
            myControl.drag = false;
            base.OnMouseLeave();
        }
    }
     * */

}
