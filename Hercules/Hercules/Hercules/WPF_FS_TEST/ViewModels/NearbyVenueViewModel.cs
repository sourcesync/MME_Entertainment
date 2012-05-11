using System;
using System.ComponentModel;
using System.Diagnostics;
using WP7Square.Classes;


namespace WP7Square.ViewModels
{
    public class NearbyVenueViewModel : INotifyPropertyChanged
    {
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

        private string _address;
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                this.NotifyPropertyChanged("Address");
            }
        }

        private string _distance;
        public string Distance
        {
            get
            {
                return _distance;
            }
            set
            {
                _distance = value;
                this.NotifyPropertyChanged("Distance");
            }
        }

        private Location _venueLocation;
        public Location VenueLocation
        {
            get
            {
                return _venueLocation;
            }
            set
            {
                _venueLocation = value;
                this.NotifyPropertyChanged("VenueLocation");
            }
        }

        private string _id;
        public string Id
        {
            get
            {
                return _id; ;
            }
            set
            {
                _id = value;
                this.NotifyPropertyChanged("Id");
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
