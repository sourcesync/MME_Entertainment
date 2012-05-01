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
    /// Interaction logic for UserControlMenu.xaml
    /// </summary>
    public partial class UserControlMenu : UserControl
    {
        public UserControlMenu()
        {
            InitializeComponent();
        }

        private void image1_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            WindowWhiteCastle a = WindowWhiteCastle.getParent(this);
            a.ShowMain();
        }

        private void image5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowWhiteCastle a = WindowWhiteCastle.getParent(this);
            a.ShowChoose(0);
        }

        private void image3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowWhiteCastle a = WindowWhiteCastle.getParent(this);
            a.ShowChoose(1);
        }

        private void image4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowWhiteCastle a = WindowWhiteCastle.getParent(this);
            a.ShowChoose(2);
        }

        private void image2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowWhiteCastle a = WindowWhiteCastle.getParent(this);
            a.ShowChoose(3);
        }

        private void image6_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowWhiteCastle a = WindowWhiteCastle.getParent(this);
            a.ShowChoose(4);
        }

        private void image7_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowWhiteCastle a = WindowWhiteCastle.getParent(this);
            a.ShowChoose(5);
        }
    }
}
