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

namespace HerculesWPFSendADrink
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

        public void Start()
        {
            this.image2.Visibility = System.Windows.Visibility.Visible;
            this.image3.Visibility = System.Windows.Visibility.Visible;
            this.image4.Visibility = System.Windows.Visibility.Visible;
        }

        private void image2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.image2.Visibility = System.Windows.Visibility.Hidden;
            this.image3.Visibility = System.Windows.Visibility.Visible;
            this.image4.Visibility = System.Windows.Visibility.Hidden;
        }

        private void image3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.image2.Visibility = System.Windows.Visibility.Hidden;
            this.image3.Visibility = System.Windows.Visibility.Hidden;
            this.image4.Visibility = System.Windows.Visibility.Visible;
        }

        private void image4_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.image2.Visibility = System.Windows.Visibility.Visible;
            this.image3.Visibility = System.Windows.Visibility.Hidden;
            this.image4.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
