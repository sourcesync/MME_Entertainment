using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace WP7Square
{
    public partial class Error : PhoneApplicationPage
    {
        public Error()
        {
            InitializeComponent();
        }

        public static Exception Exception;

        protected override void OnNavigatedTo(Microsoft.Phone.Navigation.PhoneNavigationEventArgs e)
        {
            DetailsText.Visibility = Visibility.Collapsed;
            DetailsText.Text = string.Empty;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var _root = Application.Current.RootVisual as PhoneApplicationFrame;
            var tran = (TransitioningContentControl)VisualTreeHelper.GetChild(_root, 0);
            tran.Transition = "SlideRight";
            NavigationService.Navigate(new Uri("/MainMenu.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ShowDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            DetailsText.Visibility = Visibility.Visible;
            DetailsText.Text = Exception.ToString();
        }
    }
}