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
using Touchality.FoursquareApi;
using WP7Square.Helpers;

namespace WP7Square.ViewModels
{
    public class FriendDetailViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ItemViewModel> _items;

        public FriendDetailViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>() {
                                new ItemViewModel() { LineOne = "Loading..." },
                        };
        }

        private string _venueDisplay;
        public string VenueDisplay
        {
            get
            {
                return _venueDisplay;
            }
            set
            {
                _venueDisplay = value;
                this.NotifyPropertyChanged("VenueDisplay");
            }
        }

        private Visibility _showMap;
        public Visibility ShowMap
        {
            get
            {
                return _showMap;
            }
            set
            {
                _showMap = value;
                this.NotifyPropertyChanged("ShowMap");
            }
        }

        private string _latLong;
        public string LatLong
        {
            get
            {
                return _latLong;
            }
            set
            {
                _latLong = value;
                this.NotifyPropertyChanged("LatLong");
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                this.NotifyPropertyChanged("Name");
            }
        }

        private string _elapsed;
        public string Elapsed
        {
            get
            {
                return _elapsed;
            }
            set
            {
                _elapsed = value;
                this.NotifyPropertyChanged("Elapsed");
            }
        }

        private string _photoUri;
        public string PhotoUri
        {
            get
            {
                return _photoUri;
            }
            set
            {
                _photoUri = value;
                this.NotifyPropertyChanged("PhotoUri");
            }
        }

        public FriendDetailViewModel(User user)
        {            
            this.Name = string.Format("{0} {1}", user.firstname, user.lastname);
            this.PhotoUri = user.photo;
            if (user.venue != null)
            {
                string prefix = user.venue.name != "Off the Grid" ? "@ " : string.Empty;
                this.VenueDisplay = prefix + user.venue.name;
                this.LatLong = string.Format("{0}, {1}", user.venue.geolat, user.venue.geolong);
                this.Elapsed = Utilities.GetElapsedText(DateTime.Parse(user.checkin.created).AddHours(-8));
                this.ShowMap = Visibility.Visible;
            }
            else
            {
                this.VenueDisplay = "Off the Grid";
                this.LatLong = string.Empty;
                this.ShowMap = Visibility.Collapsed;
                
            }

           //user.checkin.
                
            Items = new ObservableCollection<ItemViewModel>();
            foreach (var item in user.badges)
            {
                Items.Add(new ItemViewModel
                {
                    LineOne = item.name,
                    LineTwo = item.description,
                    PhotoUri = item.icon,
                });
            }
        }

        public ObservableCollection<ItemViewModel> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new ObservableCollection<ItemViewModel>();
                }

                return _items;
            }

            set
            {
                _items = value;
            }
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
