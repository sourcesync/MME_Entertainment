using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace WP7Square.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public SettingsViewModel()
        {
            var lightBrush = new SolidColorBrush(Color.FromArgb(25, 255, 255, 255));
            var darkBrush = new SolidColorBrush(Color.FromArgb(50, 255, 255, 255));
            this.Items = new ObservableCollection<SettingsItemViewModel>() {
                                new SettingsItemViewModel() { IconPath = "images/check_in.png", LineOne = "Setting 1", LineTwo = "Setting 1 Line 2", TargetView = "CheckInChooseVenue.xaml", BackgroundBrush = lightBrush },
                                new SettingsItemViewModel() { IconPath = "images/check-ins.png", LineOne = "Setting 2", LineTwo = "Setting 2 Line 2", TargetView="Friends.xaml", BackgroundBrush = darkBrush  },
                                new SettingsItemViewModel() { IconPath = "images/history.png" ,LineOne = "Setting 3", LineTwo = "Setting 3 Line 2", TargetView="Society.xaml", BackgroundBrush = lightBrush  },
                                new SettingsItemViewModel() { IconPath = "images/setup.png", LineOne = "Setting 4", LineTwo = "Setting 4 Line 2", TargetView="Settings.xaml", BackgroundBrush = darkBrush  },
                                new SettingsItemViewModel() { IconPath = "images/logout.png", LineOne = "Setting 5", LineTwo = "Setting 5 Line 2", TargetView="Login.xaml", BackgroundBrush = lightBrush  },
                        };
        }

        public ObservableCollection<SettingsItemViewModel> Items
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
