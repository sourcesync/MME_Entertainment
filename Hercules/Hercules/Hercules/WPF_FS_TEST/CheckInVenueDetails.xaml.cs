using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;
using Touchality.FoursquareApi;
using WP7Square.Classes;
using WP7Square.Helpers;
using Location = WP7Square.Classes.Location;

namespace WP7Square
{
    public partial class CheckInVenueDetails : PhoneApplicationPage
    {
        private Venue venue;
        private Location pinLocation;
        
        public CheckInVenueDetails()
        {
            InitializeComponent();            
        }

        public void ShowLoading()
        {
            panelLoading.Visibility = Visibility.Visible;            
        }

        public void HideLoading()
        {
            panelLoading.Visibility = Visibility.Collapsed;            
        }

        protected override void OnNavigatedTo(Microsoft.Phone.Navigation.PhoneNavigationEventArgs e)
        {
            ShowLoading();
            Foursquare.GetVenue(GetVenueCallback, NavigationContext.QueryString["id"]);
            CheckInButton.IsEnabled = false;                        
            base.OnNavigatedTo(e);            
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
            MapHelper.AddPushpin(canvasMap, pinLocation, pinLocation, 16, " ●");
            HideLoading();            
        }

        private void GetVenueCallback(GetVenueCompletedEventArgs args)
        {
            if (args.Result == null)
            {
                if (args.Error == null || string.IsNullOrEmpty(args.Error.Message))
                {
                    MessageBox.Show("Unknown error");
                    return;
                }
                MessageBox.Show(args.Error.Message);
                return;
            }
            venue = args.Result;
            CheckInButton.IsEnabled = true;

            #region data clean up

            if (string.IsNullOrEmpty(venue.address))
                venue.address = "3950 Las Vegas Blvd S";

            if (string.IsNullOrEmpty(venue.city))
                venue.city = "Las Vegas";

            if(string.IsNullOrEmpty(venue.state))
                venue.state = "NV";

            if(string.IsNullOrEmpty(venue.zip))
                venue.zip = "89119";

            CityStateZip.Text = string.Format("{0}, {1} {2}", venue.city, venue.state, venue.zip);

            if (string.IsNullOrEmpty(venue.phone) && venue.zip == "89119")
                venue.phone = "7026320648";

            if (string.IsNullOrEmpty(venue.twitter) && venue.zip == "89119")
                venue.twitter = "@LVHotels";

            if (!string.IsNullOrEmpty(venue.phone))
                venue.phone = Utilities.FormatPhoneNumber(venue.phone);

            #endregion

            VenueName.Text = venue.name;
            VenueAddress.Text = venue.address;
            CrossStreet.Text = venue.crossstreet;
            Distance.Text = venue.distance;
            Phone.Text = venue.phone;
            Twitter.Text = venue.twitter;

            string distanceStr = Cache.Get("VenueDistance") as string;            
            if (distanceStr != null)
            {
                Distance.Text = distanceStr;
            }

            pinLocation = LocationHelper.ToLocation(args.Result.geolat, args.Result.geolong);
            Cache.Set("pinLocation", pinLocation);
            MapHelper.LoadMap(LoadMapCompleted, pinLocation.Latitude, pinLocation.Longitude, (int)imageMap.Width, (int)imageMap.Height);            
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (venue == null)
            {
                return;
            }

            string shout = ShoutTextBox.Text;

            if (string.IsNullOrEmpty(shout))
            {
                shout = string.Empty;
            }

            Location currentLocation = LocationHelper.GetCurrentLocation();
            CheckInButton.IsEnabled = false;
            Foursquare.CheckIn(CheckInCallback, venue.id, string.Empty, shout, false, false, false, currentLocation.Latitude, currentLocation.Longitude);
        }

        public void CheckInCallback(CheckInCompletedEventArgs e)
        {
            string message = e.Result.message;
            var root = Application.Current.RootVisual as PhoneApplicationFrame;
            var tran = (TransitioningContentControl)VisualTreeHelper.GetChild(root, 0);
            tran.Transition = "SlideLeft";

            var url = string.Format("/CheckInConfirmation.xaml?id={0}&message={1}", venue.id, message);
            Cache.Set("venue", venue);
            NavigationService.Navigate(new Uri(url, UriKind.Relative));
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            var root = Application.Current.RootVisual as PhoneApplicationFrame;
            var tran = (TransitioningContentControl)VisualTreeHelper.GetChild(root, 0);
            tran.Transition = "SlideRight";
            base.OnBackKeyPress(e);
        }

        private void GetDirectionsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Come back soon!", "Not implemented", MessageBoxButton.OK);
            //Cache.Set("directions", venue);
            //NavigationService.Navigate(new Uri(string.Format("/Directions.xaml?id={0}", venue.id), UriKind.Relative));
        }
    }
}