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
        private int rotation = 0;

        public void SetRotation(int i)
        {
            if (i == 0)
            {

                this.RenderTransform = new RotateTransform(0, 1024 / 2.0, 768 / 2.0);
            }
            else
            {
                this.RenderTransform = new RotateTransform(180, 1024 / 2.0, 768 / 2.0);
            }
        }

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

        public delegate void UserControlMenuToggle(int option);
        public UserControlMenuToggle tevt = null;

        private void toggle(object sender)
        {
            if (this.tevt == null) return;
            if (sender == this.imagea)
                this.tevt(0);
            else
                this.tevt(1);
        }

        private void imagea_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.toggle(sender);
        }

        private void imageb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.toggle(sender);
        }
    }

    
       
}
