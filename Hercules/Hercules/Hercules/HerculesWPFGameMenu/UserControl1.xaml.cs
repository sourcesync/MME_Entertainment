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
using MME.HerculesConfig;

namespace HerculesWPFGameMenu
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


            BitmapSource src = WindowUtility.GetScreenBitmapWPF("table_main_menu_bg.jpg");
            this.image2.Source = src;

            /*
            BitmapImage bm = WindowUtility.GetScreenBitmapWPF("plain_button.png");
            this.image1.Source = bm;
            this.image3.Source = bm;
            this.image4.Source = bm;
             * */

            this.myForeBrush = this.myBlackBrush;
            System.Windows.Media.Color scolor = Colors.Black;
            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "GameMenuTextColor")))
            {
                String colorstr = ConfigUtility.GetConfig(ConfigUtility.Config, "GameMenuTextColor");
                if (colorstr == "white")
                {
                    this.myForeBrush = this.myWhiteBrush;
                    scolor = Colors.White;
                }

            }
            this.label1.Foreground = this.myForeBrush;
            this.label2.Foreground = this.myForeBrush;
            this.label3.Foreground = this.myForeBrush;

        }


        public delegate void UserControlGameMainToggle(int option);
        public UserControlGameMainToggle tevt = null;

        private void image1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.tevt != null)
            {
                this.tevt(0);
            }
        }

        private void image3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.tevt != null)
            {
                this.tevt(1);
            }
        }

        private void image4_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.tevt != null)
            {
                this.tevt(2);
            }
        }
    }
}
