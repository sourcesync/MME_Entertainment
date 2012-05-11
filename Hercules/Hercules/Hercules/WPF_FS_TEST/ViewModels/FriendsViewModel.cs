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
using System.Collections.Generic;
using WP7Square.Helpers;

namespace WP7Square.ViewModels
{
    public class FriendsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<FriendViewModel> _items;

        public FriendsViewModel(IList<CheckIn> checkins)
        {
            if (checkins == null) return;
            foreach (var item in checkins)
            {
                string prefix = item.venue.name != "Off the Grid" ? "@ " : string.Empty;
                this.Items.Add(new FriendViewModel
                {
                    Id = item.id,
                    UserId = item.user.id,
                    LineOne = item.user.firstname + " " + item.user.lastname,
                    LineTwo = prefix + item.venue.name,
                    //LineThree = Utilities.GetElapsedText(DateTime.Parse(item.created)),
                    LineThree = Utilities.GetElapsedText(DateTime.Parse(item.created).AddHours(-8)),
                    PhotoUri = item.user.photo,
                });
            }
        }

        public FriendsViewModel()
        {
            this.Items.Add(new FriendViewModel() { LineOne = "Loading...", LineTwo = "", TargetView = "FriendDetail.xaml" });
        }

        public ObservableCollection<FriendViewModel> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new ObservableCollection<FriendViewModel>();
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
