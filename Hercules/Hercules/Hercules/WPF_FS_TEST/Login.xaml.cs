using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Phone.Controls;
using Touchality.FoursquareApi;
using System.IO.IsolatedStorage;

namespace WP7Square
{
    public partial class Login : PhoneApplicationPage
    {
        private IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;

        public Login()
        {
            InitializeComponent();

            string username = string.Empty;
            string password = string.Empty;

            if (appSettings.Contains("Username"))
                username = (string)appSettings["Username"];
            if (appSettings.Contains("Username"))
                password = (string)appSettings["Password"];

            EmailTextBox.Text = username;
            PasswordTextBox.Password = password;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = EmailTextBox.Text;
            string password = PasswordTextBox.Password;

            //todo disable button
            ErrorTextBox.Visibility = Visibility.Collapsed;
            Foursquare.SignIn(SignInCallback, username, password);

            if(!string.IsNullOrEmpty(username))
                appSettings["Username"] = username;
            if (!string.IsNullOrEmpty(password))
                appSettings["Password"] = password;
            appSettings.Save();

            var root = Application.Current.RootVisual as PhoneApplicationFrame;
            var tran = (TransitioningContentControl)VisualTreeHelper.GetChild(root, 0);
            tran.Transition = "SlideLeft";
        }

        private void SignInCallback(GetUserCompletedEventArgs a)
        {
            if (a.Result == null)
            {
                // show error
                // enable button
                ErrorTextBox.Visibility = Visibility.Visible;
                ErrorTextBox.Text = "Unable to login";
                if (a.Error != null && a.Error is FoursquareServiceException)
                {
                    MessageBox.Show(a.Error.Message, "Woops", MessageBoxButton.OK);
                }                
                return;
            }
            NavigationService.Navigate(new Uri("/MainMenu.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}