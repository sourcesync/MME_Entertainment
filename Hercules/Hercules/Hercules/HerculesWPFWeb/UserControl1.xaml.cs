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

namespace HerculesWPFWeb
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

        private void webBrowser1_Loaded(object sender, RoutedEventArgs e)
        {


            this.webBrowser1.Navigate(new Uri("http://www.whitecastle.com", UriKind.RelativeOrAbsolute));
        }

        public void Load()
        {

            this.webBrowser1.Navigate(new Uri("http://www.whitecastle.com", UriKind.RelativeOrAbsolute));
        }
    }
}
