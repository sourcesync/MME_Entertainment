using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;
using Touchality.FoursquareApi;
using WP7Square.Classes;
using System.Windows;
using System;

namespace WP7Square
{
    public partial class CheckInConfirmation : PhoneApplicationPage
    {
        public CheckInConfirmation()
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
            BindVenue();
            imageMap.Source = Cache.Get("mapSource") as BitmapImage;
            Location pinLocation = Cache.Get("pinLocation") as Location;
            MapHelper.AddPushpin(canvasMap, pinLocation, pinLocation, 16, " ●");
            HideLoading();
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(Microsoft.Phone.Navigation.PhoneNavigationEventArgs e)
        {
            ShowLoading();
            base.OnNavigatedFrom(e);
        }

        private void BindVenue()
        {
            Venue venue = Cache.Get("venue") as Venue;
            if(venue == null) return;
            VenueName.Text = venue.name;
            VenueAddress.Text = venue.address;
            CrossStreet.Text = venue.crossstreet;
            TextBlockMessage.Text = NavigationContext.QueryString["message"];
            Location location = LocationHelper.ToLocation(venue.geolat, venue.geolong);
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void ConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            var root = Application.Current.RootVisual as PhoneApplicationFrame;
            var tran = (TransitioningContentControl)VisualTreeHelper.GetChild(root, 0);
            tran.Transition = "SlideLeft";
            NavigationService.Navigate(new Uri("/MainMenu.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}