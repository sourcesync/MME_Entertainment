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
using System.Windows.Navigation;
using WP7Square.ViewModels;

namespace WP7Square
{
    public partial class MainMenu : PhoneApplicationPage
    {
        public MainMenu()
        {
            InitializeComponent();
            //Set the data context of the listbox control to the sample data
            this.Loaded += new RoutedEventHandler(ListPage_Loaded);
        }

        void ListPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new MainViewModel();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var item = (MainMenuItemViewModel)e.AddedItems[0];

                var _root = Application.Current.RootVisual as PhoneApplicationFrame;
                var tran = (TransitioningContentControl)VisualTreeHelper.GetChild(_root, 0);
                tran.Transition = item.LineOne == "Log Out" ? "SlideRight" : "SlideLeft";
                if (!string.IsNullOrEmpty(item.TargetView))
                {
                    NavigationService.Navigate(new Uri("/" + item.TargetView, UriKind.RelativeOrAbsolute));
                }
            }
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            var _root = Application.Current.RootVisual as PhoneApplicationFrame;
            var tran = (TransitioningContentControl)VisualTreeHelper.GetChild(_root, 0);
            tran.Transition = "SlideRight";

            base.OnBackKeyPress(e);
        }
    }
}