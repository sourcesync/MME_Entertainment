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
            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "WebURL")))
            {
                String weburl = ConfigUtility.GetConfig(ConfigUtility.Config, "WebURL");
                this.webBrowser1.Navigate(new Uri(weburl, UriKind.RelativeOrAbsolute));
            }
            else
            {
                this.webBrowser1.Navigate(new Uri("http://www.whitecastle.com/company", UriKind.RelativeOrAbsolute));
            }

        }

        public void Load()
        {
            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "WebURL")))
            {
                String weburl = ConfigUtility.GetConfig(ConfigUtility.Config, "WebURL");
                this.webBrowser1.Navigate(new Uri(weburl, UriKind.RelativeOrAbsolute));
            }
            else
            {
                this.webBrowser1.Navigate(new Uri("http://www.whitecastle.com/company", UriKind.RelativeOrAbsolute));
            }
        }
    }
}
