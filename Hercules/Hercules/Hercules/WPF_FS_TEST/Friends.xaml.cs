using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Navigation;
using WP7Square.Helpers;
using WP7Square.ViewModels;
using WP7Square.Classes;
using Touchality.FoursquareApi;
using System.Windows.Media.Imaging;
using Location = WP7Square.Classes.Location;

namespace WP7Square
{
    public partial class Friends : PhoneApplicationPage
    {
        private Location _location;        

        public Friends()
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

        protected override void OnNavigatedFrom(PhoneNavigationEventArgs e)
        {
            ShowLoading();
            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(PhoneNavigationEventArgs e)
        {
            ShowLoading();
            _location = LocationHelper.GetCurrentLocation();
            Foursquare.GetCheckIns(GetCheckInsCallback, _location.Latitude, _location.Longitude);
            base.OnNavigatedTo(e);
        }

        private void FriendsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Set datacontext of details page to selected listbox item
            var _root = Application.Current.RootVisual as PhoneApplicationFrame;
            var tran = (TransitioningContentControl)VisualTreeHelper.GetChild(_root, 0);
            tran.Transition = "SlideLeft";
            FriendViewModel friendViewModel = FriendsList.SelectedItem as FriendViewModel;
            if (friendViewModel == null)
            {
                return;
            }
            if (DoNotNav)
            {
                DoNotNav = false;
                return;
            }
            NavigationService.Navigate(new Uri("/FriendDetail.xaml?id=" + friendViewModel.UserId, UriKind.RelativeOrAbsolute));
        }


        private void GetCheckInsCallback(GetCheckInsCompletedEventArgs e)
        {            
            var viewModel = new FriendsViewModel(e.Result);
            DataContext = viewModel;            
            AddPushpins(e.Result);
            MapHelper.LoadMap(LoadMapCompleted, _location.Latitude, _location.Longitude, (int)imageMap.Width, (int)imageMap.Height);
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
            Utilities.ScrollToCenter(scrollMap);
            LocationHelper.AddMyLocation(canvasMap);
            HideLoading();
        }

        private void AddPushpins(IList<CheckIn> checkins)
        {
            if (checkins == null) return;

            for (int i = 0; i < checkins.Count; i++)
            {
                var checkin = checkins[i];
                double latitude, longitude;
                if (checkin == null) continue;
                if (checkin.venue == null) continue;
                if (!double.TryParse(checkin.venue.geolat, out latitude)) continue;
                if (!double.TryParse(checkin.venue.geolong, out longitude)) continue;

                Location pinLocation = new Location { Latitude = double.Parse(checkin.venue.geolat), Longitude = double.Parse(checkin.venue.geolong) };
                FrameworkElement pin = MapHelper.AddPushpin(canvasMap, _location, pinLocation, 16, (i + 1).ToString());
                pin.MouseEnter += PushpinMouseEnter;
                pin.Tag = i;
            }
        }

        public bool DoNotNav;
        void PushpinMouseEnter(object sender, MouseEventArgs e)
        {
            FrameworkElement pin = sender as FrameworkElement;
            if (pin == null) return;
            int ndx = (int)pin.Tag;
            DoNotNav = true;

            if (ContentPopup.Visibility == Visibility.Visible)
            {
                ContentPopup.Visibility = Visibility.Collapsed;
                return;
            }

            double left = (pin.Margin.Left + pin.Width / 2);
            double top = (pin.Margin.Top + pin.Height / 2) - ContentPopupRectangle.Height;

            FriendViewModel friendViewModel = FriendsList.Items[ndx] as FriendViewModel;
            if (friendViewModel != null)
            {
                ContentPopupText.Text = friendViewModel.LineOne;
                ContentPopupText2.Text = friendViewModel.LineTwo;
                ContentPopupText3.Text = friendViewModel.LineThree;
                ContentPopupImage.Source = new BitmapImage(new Uri(friendViewModel.PhotoUri, UriKind.RelativeOrAbsolute));
            }
            Canvas.SetZIndex(ContentPopup, 89);
            ContentPopup.Margin = new Thickness(left, top, 0, 0);
            ContentPopup.Visibility = Visibility.Visible;
            FriendsList.SelectedIndex = ndx;
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {            
            var root = Application.Current.RootVisual as PhoneApplicationFrame;
            var tran = (TransitioningContentControl)VisualTreeHelper.GetChild(root, 0);
            tran.Transition = "SlideRight";
            base.OnBackKeyPress(e);
        }

        private void ApplicationBarAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Come back soon!", "Not implemented", MessageBoxButton.OK);
        }

        private void ApplicationBarRefresh_Click(object sender, EventArgs e)
        {
            Location location = LocationHelper.GetCurrentLocation();
            Foursquare.GetCheckIns(GetCheckInsCallback, location.Latitude, location.Longitude);
        }

        private void ApplicationBarBadge_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Come back soon!", "Not implemented", MessageBoxButton.OK);
        }

        private void ContentPopup_MouseEnter(object sender, MouseEventArgs e)
        {
            ContentPopup.Visibility = Visibility.Collapsed;
        }

        private void ContentPopup_MouseLeave(object sender, MouseEventArgs e)
        {
            ContentPopup.Visibility = Visibility.Collapsed;
        }

        private void ContentPopup_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Set datacontext of details page to selected listbox item
            var root = Application.Current.RootVisual as PhoneApplicationFrame;
            var tran = (TransitioningContentControl)VisualTreeHelper.GetChild(root, 0);
            tran.Transition = "SlideLeft";
            FriendViewModel friendViewModel = FriendsList.SelectedItem as FriendViewModel;
            if (friendViewModel == null)
            {
                return;
            }

            NavigationService.Navigate(new Uri("/FriendDetail.xaml?id=" + friendViewModel.UserId, UriKind.RelativeOrAbsolute));
        }

    }
}