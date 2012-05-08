using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfCustomDraw
{
    public partial class UserControl1 : Image
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            Pen pen = new Pen(Brushes.Black, 1);
            Rect rect = new Rect(20, 20, 50, 60);

            drawingContext.DrawRectangle(null, pen, rect);
        }
    }
}
