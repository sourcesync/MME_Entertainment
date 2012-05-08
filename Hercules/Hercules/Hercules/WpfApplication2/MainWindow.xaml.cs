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

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.ctl = new HerculesWPFWeb.UserControl1();
        }

        private HerculesWPFWeb.UserControl1 ctl;

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

            //this.AddChild(this.ctl);
            //this.ctl.Load();
            
            
            this.webBrowser1.Navigate(new Uri("http://www.google.com", UriKind.RelativeOrAbsolute));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.webBrowser1.Navigate(new Uri("http://www.google.com", UriKind.RelativeOrAbsolute));
            this.ctl.Load();
        }
    }
}
