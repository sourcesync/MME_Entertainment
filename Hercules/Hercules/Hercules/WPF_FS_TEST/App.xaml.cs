using System;
using System.Windows;
using Touchality.FoursquareApi;
using Microsoft.Phone.Controls;

namespace WP7Square
{
    public partial class App : Application
    {
        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += new EventHandler<ApplicationUnhandledExceptionEventArgs>(Application_UnhandledException);
            InitializeComponent();
        }

        //Code to execute on application startup
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            PhoneApplicationFrame frame = RootVisual as PhoneApplicationFrame;
            frame.Source = new Uri("/Login.xaml", UriKind.Relative);
        }

        private void SignInCallback(GetUserCompletedEventArgs e)
        {
        }

        //Code to execute on application exit
        private void Application_Exit(object sender, EventArgs e)
        {
        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred, break in the debugger
                System.Diagnostics.Debugger.Break();
            }

            e.Handled = true;
            Error.Exception = e.ExceptionObject;
            (RootVisual as Microsoft.Phone.Controls.PhoneApplicationFrame).Source = new Uri("/Error.xaml", UriKind.Relative);
        }
    }
}