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
using MME.HerculesConfig;

namespace HerculesWPFDraw
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {

        SolidColorBrush myBlackBrush = new SolidColorBrush(Colors.Black);
        SolidColorBrush myWhiteBrush = new SolidColorBrush(Colors.White);
        SolidColorBrush myForeBrush = null;

        public UserControl1()
        {
            InitializeComponent();

            //this.inkCanvas1.S

            this.inkCanvas1.EditingMode = InkCanvasEditingMode.InkAndGesture;


            this.myForeBrush = this.myBlackBrush;
            
            System.Windows.Media.Color scolor = Colors.Black;
            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "GameForeColor")))
            {
                String colorstr = ConfigUtility.GetConfig(ConfigUtility.Config, "GameForeColor");
                if (colorstr == "white")
                {
                    this.myForeBrush = this.myWhiteBrush;
                    scolor = Colors.White;
                }
                 
            }



            DrawingAttributes inkAttributes = new DrawingAttributes();
            inkAttributes.Height = 5;
            inkAttributes.Width = 5;
            this.inkCanvas1.DefaultDrawingAttributes = inkAttributes;
            this.inkCanvas1.DefaultDrawingAttributes.Color = scolor;

            this.label1.Foreground = this.myForeBrush;

            BitmapSource src = WindowUtility.GetScreenBitmapWPF("gamebg.jpg");
            this.image2.Source = src;
        }

        public void Restart()
        {
            this.inkCanvas1.Strokes.Clear();
        }
    }
}
