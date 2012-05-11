using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;
using WP7Square.ViewModels;
using Touchality.FoursquareApi;
using WP7Square.Classes;

namespace WP7Square
{
    public partial class FriendDetail : PhoneApplicationPage
    {
        private Venue _venue = null;
        private Location _pinLocation = null;

        private string _userId = string.Empty;

        public FriendDetail()
        {
            InitializeComponent();
        }

        public void ShowLoading()
        {
            LayoutRoot.RowDefinitions[2].Height = new GridLength(0);
            panelLoading.Visibility = Visibility.Visible;
        }

        public void HideLoading()
        {
            LayoutRoot.RowDefinitions[2].Height = new GridLength(300);
            panelLoading.Visibility = Visibility.Collapsed;
        }

        protected override void OnNavigatedTo(Microsoft.Phone.Navigation.PhoneNavigationEventArgs e)
        {
            _userId = NavigationContext.QueryString["id"];
            Foursquare.GetUser(GetUserCallback, _userId);
            DataContext = new FriendDetailViewModel();
            base.OnNavigatedTo(e);
        }

        public void GetUserCallback(GetUserCompletedEventArgs e)
        {
            _venue = null;
            if (e.Result.venue != null)
            {
                _venue = e.Result.venue;
                _pinLocation = new Location
                    {
                        Latitude = double.Parse(e.Result.venue.geolat),
                        Longitude = double.Parse(e.Result.venue.geolong),
                    };
            }
            var model = new FriendDetailViewModel(e.Result);
            DataContext = model;
            if (_pinLocation != null)
            {
                MapHelper.LoadMap(LoadMapCompleted, _pinLocation.Latitude, _pinLocation.Longitude, (int)imageMap.Width,
                                  (int)imageMap.Height);
            }
            else
            {
                LayoutRoot.RowDefinitions[2].Height = new GridLength(0);                
            }
        }


        void LoadMapCompleted(object sender, GetMapUriCompletedEventArgs e)
        {
            BitmapImage bitMap = new BitmapImage();
            bitMap.UriSource = new Uri(e.Result.Uri, UriKind.Absolute);
            bitMap.ImageOpened += BitMapImageOpened;
            imageMap.Source = bitMap;
        }

        void BitMapImageOpened(object sender, RoutedEventArgs e)
        {
            BitmapImage bitMap = sender as BitmapImage;
            if (bitMap == null) return;
            Cache.Set("mapSource", bitMap);
            MapHelper.AddPushpin(canvasMap, _pinLocation, _pinLocation, 16, " ●");
            HideLoading();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            var root = Application.Current.RootVisual as PhoneApplicationFrame;
            var tran = (TransitioningContentControl)VisualTreeHelper.GetChild(root, 0);
            tran.Transition = "SlideRight";
            base.OnBackKeyPress(e);
        }

        private void ApplicationBarPhone_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Come back soon!", "Not implemented", MessageBoxButton.OK);
        }

        private void ApplicationBarEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Come back soon!", "Not implemented", MessageBoxButton.OK);
        }

        private void ApplicationBarDirections_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Come back soon!", "Not implemented", MessageBoxButton.OK);
            //if (_venue == null) return;
            //Cache.Set("directions", _venue);
            //NavigationService.Navigate(new Uri(string.Format("/Directions.xaml?id={0}", _venue.id), UriKind.Relative));
        }
    }
}