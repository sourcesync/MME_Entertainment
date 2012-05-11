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
    public class ItemViewModel : INotifyPropertyChanged
    {
        private string lineOne;
        /// <summary>Sample ViewModel property; this property is used in the view to display its value using a Binding.</summary>
        /// <returns></returns>
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
        /// <summary>Sample ViewModel property; this property is used in the view to display its value using a Binding.</summary>
        /// <returns></returns>
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
        /// <summary>Sample ViewModel property; this property is used in the view to display its value using a Binding.</summary>
        /// <returns></returns>
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

        private Visibility _indicatorVisible;
        public Visibility IndicatorVisible
        {
            get
            {
                return _indicatorVisible;
            }
            set
            {
                _indicatorVisible = value;
                this.NotifyPropertyChanged("IndicatorVisible");
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