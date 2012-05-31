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
using System.Reflection;

namespace HerculesWPFWeb
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private bool silenced = false;

        public UserControl1()
        {
            InitializeComponent();

            this.webBrowser1.Navigated += new NavigatedEventHandler(webBrowser1_Navigated);
           
        }

        public void HideScriptErrors(WebBrowser wb, bool Hide)
        {
            FieldInfo fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fiComWebBrowser == null) return;
            object objComWebBrowser = fiComWebBrowser.GetValue(wb);
            if (objComWebBrowser == null) return;
            objComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { Hide });
        }

        void webBrowser1_Navigated(object sender, NavigationEventArgs e)
        {
            if (!silenced)
            {
                this.HideScriptErrors(this.webBrowser1, true);
                silenced = true;
            }

            String str = e.WebResponse.ResponseUri.ToString();
        }

        private void webBrowser1_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "WebURL")))
            {
                String weburl = ConfigUtility.GetConfig(ConfigUtility.Config, "WebURL");
                this.webBrowser1.Navigate(new Uri(weburl, UriKind.RelativeOrAbsolute));
            }
            else
            {
                this.webBrowser1.Navigate(new Uri("http://www.whitecastle.com/company", UriKind.RelativeOrAbsolute));
            }
            */
        }

        public void Load()
        {
            /*
            if (!string.IsNullOrEmpty(ConfigUtility.GetConfig(ConfigUtility.Config, "WebURL")))
            {
                String weburl = ConfigUtility.GetConfig(ConfigUtility.Config, "WebURL");
                this.webBrowser1.Navigate(new Uri(weburl, UriKind.RelativeOrAbsolute));
            }
            else
            {
                this.webBrowser1.Navigate(new Uri("http://www.whitecastle.com/company", UriKind.RelativeOrAbsolute));
            }
            */
        }
    }
}
