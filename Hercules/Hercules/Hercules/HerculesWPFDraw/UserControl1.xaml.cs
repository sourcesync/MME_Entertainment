using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Ink;

namespace HerculesWPFDraw
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();

            this.inkCanvas1.EditingMode = InkCanvasEditingMode.InkAndGesture;

            DrawingAttributes inkAttributes = new DrawingAttributes();

            inkAttributes.Height = 5;
            inkAttributes.Width = 5;
            this.inkCanvas1.DefaultDrawingAttributes = inkAttributes;
        }

        public void Restart()
        {
            this.inkCanvas1.Strokes.Clear();
        }
    }
}
