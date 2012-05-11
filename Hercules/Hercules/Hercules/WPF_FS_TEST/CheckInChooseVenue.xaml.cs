using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;
using WP7Square.Helpers;
using WP7Square.ViewModels;
using WP7Square.Classes;
using Touchality.FoursquareApi;
using Location = WP7Square.Classes.Location;

namespace WP7Square
{
    public partial class CheckInChooseVenue : PhoneApplicationPage
    {
        public CheckInChooseVenue()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            var _root = Application.Current.RootVisual as PhoneApplicationFrame;
            var tran = (TransitioningContentControl)VisualTreeHelper.GetChild(_root, 0);
            tran.Transition = "SlideRight";
            base.OnBackKeyPress(e);
        }

        protected override void OnNavigatedTo(Microsoft.Phone.Navigation.PhoneNavigationEventArgs e)
        {
            Location location = LocationHelper.GetCurrentLocation();
            Foursquare.GetVenues(GetVenuesCallback, location.Latitude, location.Longitude);
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(Microsoft.Phone.Navigation.PhoneNavigationEventArgs e)
        {
            ShowLoading();
            base.OnNavigatedFrom(e);
        }

        void LoadMapCompleted(object sender, GetMapUriCompletedEventArgs e)
        {
            BitmapImage bitMap = new BitmapImage();
            bitMap.ImageOpened += BitMapImageOpened;
            bitMap.UriSource = new Uri(e.Result.Uri, UriKind.Absolute);
            imageMap.Source = bitMap;
        }

        void BitMapImageOpened(object sender, RoutedEventArgs e)
        {
            Pulsar p = LocationHelper.AddMyLocation(canvasMap);
            Utilities.ScrollToCenter(scrollMap);
            HideLoading();
        }

        private void NearbyVenues_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NearbyVenueViewModel model = null;

            if (e.AddedItems.Count != 0)
                model = (NearbyVenueViewModel)e.AddedItems[0];

            if (model == null || model.Id == null)
            {
                ShowLoading();
                Location location = LocationHelper.GetCurrentLocation();
                Foursquare.GetVenues(GetVenuesCallback, location.Latitude, location.Longitude);
                return;
            }

            //Set datacontext of details page to selected listbox item
            var _root = Application.Current.RootVisual as PhoneApplicationFrame;
            var tran = (TransitioningContentControl)VisualTreeHelper.GetChild(_root, 0);
            tran.Transition = "SlideLeft";
            Cache.Set("VenueDistance", model.Distance);
            NavigationService.Navigate(new Uri("/CheckInVenueDetails.xaml?id=" + model.Id, UriKind.RelativeOrAbsolute));
        }

        private void GetVenuesCallback(GetVenuesCompletedEventArgs e)
        {
            Location location = LocationHelper.GetCurrentLocation();            
            int ndx = 1;
            foreach (Venue venue in e.Result)
            {
                var pinLocation = new Location { Latitude = double.Parse(venue.geolat), Longitude = double.Parse(venue.geolong) };
                MapHelper.AddPushpin(canvasMap, location, pinLocation, 16, ndx.ToString());
                ndx++;
            }
            MapHelper.LoadMap(LoadMapCompleted, location.Latitude, location.Longitude,
                  (int)imageMap.Width, (int)imageMap.Height);

            var model = new CheckInChooseVenueViewModel(e.Result);
            this.DataContext = model;            
        }

        public void ShowLoading()
        {
            panelLoading.Visibility = Visibility.Visible;
            Canvas.SetZIndex(panelLoading, 99);            
        }

        public void HideLoading()
        {
            panelLoading.Visibility = Visibility.Collapsed;            
        }

        private void ApplicationBarAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Come back soon!", "Not implemented", MessageBoxButton.OK);
        }

        private void ApplicationBarRefresh_Click(object sender, EventArgs e)
        {
            Location location = LocationHelper.GetCurrentLocation();
            Foursquare.GetVenues(GetVenuesCallback, location.Latitude, location.Longitude);
        }
    }
}