using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WP7Square
{
    public class FriendViewModel : INotifyPropertyChanged
    {
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

        private string _userId;
        public string UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
                this.NotifyPropertyChanged("UserId");
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

        private string lineOne;
        public string LineOne
        {
            get
            {
                return this.lineOne;
            }
            set
            {
                this.lineOne = value;
                this.NotifyPropertyChanged("LineOne");
            }
        }

        private string lineTwo;
        public string LineTwo
        {
            get
            {
                return this.lineTwo;
            }
            set
            {
                this.lineTwo = value;
                this.NotifyPropertyChanged("LineTwo");
            }
        }

        private string lineThree;
        public string LineThree
        {
            get
            {
                return this.lineThree;
            }
            set
            {
                this.lineThree = value;
                this.NotifyPropertyChanged("LineThree");
            }
        }

        private string targetView;

        public string TargetView
        {
            get
            {
                return this.targetView;
            }

            set
            {
                this.targetView = value;
                this.NotifyPropertyChanged("TargetView");
            }
        }

        private Brush backgroundBrush;
        public Brush BackgroundBrush
        {
            get
            {
                return this.backgroundBrush;
            }

            set
            {
                this.backgroundBrush = value;
                this.NotifyPropertyChanged("BackgroundBrush");
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