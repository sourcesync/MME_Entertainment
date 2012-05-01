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

namespace WpfSandbox
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControlMain : UserControl
    {

        private System.Collections.ArrayList images = new System.Collections.ArrayList();

        public UserControlMain()
        {
            InitializeComponent();

            this.images.Add(this.image2);
            this.images.Add(this.image3);
            this.images.Add(this.image4);
            this.images.Add(this.image5);
            this.images.Add(this.image6);
            this.images.Add(this.image7);
            this.images.Add(this.image8);

            this.ResizeImages(1.4);
        }

        void ResizeImages(double factor)
        {
            for (int i = 0; i < this.images.Count; i++)
            {
                Image img = (Image)this.images[i];
                double w = img.Width;
                img.Width = factor * img.Width;
                double h = img.Height;
                img.Height = factor * img.Height;

                double offx = img.Width - w;
                double offy = img.Height - h;
                FrameworkElement element = img as FrameworkElement;
                double x = (double)element.GetValue(Canvas.LeftProperty);
                double y = (double)element.GetValue(Canvas.TopProperty);
                element.SetValue(Canvas.LeftProperty, x - offx / 2.0);
                element.SetValue(Canvas.TopProperty, y - offy / 2.0);
            }
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            WindowWhiteCastle a = WindowWhiteCastle.getParent(this);
            a.ShowMenu();
        }

        private void image5_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void image5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowWhiteCastle a = WindowWhiteCastle.getParent(this);
            a.ShowMenu();
        }
    }
}
