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
    public partial class CustomControl1 : Control
    {
        public CustomControl1()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            Color frmColor = this.Parent.BackColor;
            Brush brushColor;
            Brush bckColor;

            int alpha = 255;
            Color color = System.Drawing.Color.Red;
            brushColor = new SolidBrush(Color.FromArgb(alpha, color));
            bckColor = new SolidBrush(Color.FromArgb(alpha, this.BackColor));

            Pen pen = new Pen(this.ForeColor);

            g.FillEllipse(brushColor, bounds);

            g.DrawEllipse(pen, bounds);

            pen.Dispose();
            brushColor.Dispose();
            bckColor.Dispose();
            g.Dispose();
            base.OnPaint(e);
        }

    }
}
