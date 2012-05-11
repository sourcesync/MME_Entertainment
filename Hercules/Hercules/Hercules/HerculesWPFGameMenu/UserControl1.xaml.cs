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

namespace HerculesWPFGameMenu
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
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
