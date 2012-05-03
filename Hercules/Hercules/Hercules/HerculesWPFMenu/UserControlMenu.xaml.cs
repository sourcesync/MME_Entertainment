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

namespace HerculesWPFMenu
{
    /// <summary>
    /// Interaction logic for UserControlMenu.xaml
    /// </summary>
    public partial class UserControlMenu : UserControl
    {

        public delegate void UserControlMenuDelegate(int option);
        public UserControlMenuDelegate evt = null;

        public UserControlMenu()
        {
            InitializeComponent();
        }

        private void image1_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (evt != null) this.evt(-1);
            //WindowWhiteCastle a = WindowWhiteCastle.getParent(this);
            //a.ShowMain();
        }

        private void image5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //WindowWhiteCastle a = WindowWhiteCastle.getParent(this);
            //a.ShowChoose(0);
            if (evt != null) this.evt(0);
        }

        private void image3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //WindowWhiteCastle a = WindowWhiteCastle.getParent(this);
            //a.ShowChoose(1);
            if (evt != null) this.evt(1);
        }

        private void image4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //WindowWhiteCastle a = WindowWhiteCastle.getParent(this);
            //a.ShowChoose(2);
            if (evt != null) this.evt(2);
        }

        private void image2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //WindowWhiteCastle a = WindowWhiteCastle.getParent(this);
            //a.ShowChoose(3);
            if (evt != null) this.evt(3);
        }

        private void image6_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //WindowWhiteCastle a = WindowWhiteCastle.getParent(this);
            //a.ShowChoose(4);
            if (evt != null) this.evt(4);
        }

        private void image7_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //WindowWhiteCastle a = WindowWhiteCastle.getParent(this);
            //a.ShowChoose(5);
            if (evt != null) this.evt(5);
        }
    }
}
