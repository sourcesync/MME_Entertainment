using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDIDB;
using System.Windows.Forms;

using System.Drawing;
namespace MME.Hercules.WPFForms
{

    public class Class1 : System.Windows.Forms.Panel
    {

        public DBGraphics memGraphics;

        public Class1()
        {
            memGraphics = new DBGraphics();

            
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            //public override void OnPaint( object sender, System.Windows.Forms.PaintEventArgs e)


            if (memGraphics.CanDoubleBuffer())
            {
                // Fill in Background (for effieciency only the area that has been clipped)
                memGraphics.g.FillRectangle(new SolidBrush(SystemColors.Window),
                    0, 0, memGraphics.width, memGraphics.height);
                //e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width, e.ClipRectangle.Height);

                // Do our drawing using memGraphics.g instead e.Graphics

                // Render to the form
                //memGraphics.Render(e.Graphics);
            }
        }

    }
}
