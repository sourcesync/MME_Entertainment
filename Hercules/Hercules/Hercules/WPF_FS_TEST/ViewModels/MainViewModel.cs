using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Media;
using System.Collections.ObjectModel;
using WP7Square.ViewModels;


namespace WP7Square
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            var lightBrush = new SolidColorBrush(Color.FromArgb(25, 255, 255, 255));
            var darkBrush = new SolidColorBrush(Color.FromArgb(50, 255, 255, 255));
            this.Items = new ObservableCollection<MainMenuItemViewModel>() {
                                new MainMenuItemViewModel() { IconPath = "images/check_in.png", LineOne = "Check-In", LineTwo = "Check-In at my location", TargetView = "CheckInChooseVenue.xaml" },
                                new MainMenuItemViewModel() { IconPath = "images/check-ins.png", LineOne = "Friends", LineTwo = "Where are my friends?", TargetView="Friends.xaml" },
                                new MainMenuItemViewModel() { IconPath = "images/history.png" ,LineOne = "Society", LineTwo = "What's going on?", TargetView=string.Empty  },
                                new MainMenuItemViewModel() { IconPath = "images/setup.png", LineOne = "Settings", LineTwo = "Do it my way", TargetView=string.Empty  },
                                new MainMenuItemViewModel() { IconPath = "images/logout.png", LineOne = "Log Out", LineTwo = "Get me out of here", TargetView="Login.xaml"  },
                        };
        }

        public ObservableCollection<MainMenuItemViewModel> Items
        {
            get;
            private set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            // Ensure that the string being passed in is actually the name of a property.
            Debug.Assert(this.GetType().GetProperty(propertyName) != null);

            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}