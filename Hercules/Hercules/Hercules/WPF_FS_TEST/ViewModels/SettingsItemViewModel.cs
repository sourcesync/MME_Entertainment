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
using System.Diagnostics;

namespace WP7Square.ViewModels
{
    public class SettingsItemViewModel : INotifyPropertyChanged
    {
        private string _iconPath;
        private string _lineOne;
        private string _lineTwo;
        private string _targetView;
        private Brush _brush;

        public string IconPath
        {
            get
            {
                return _iconPath;
            }
            set
            {
                _iconPath = value;
                this.NotifyPropertyChanged("IconPath");
            }
        }

        public string LineOne
        {
            get
            {
                return _lineOne;
            }
            set
            {
                _lineOne = value;
                this.NotifyPropertyChanged("LineOne");
            }
        }

        public string LineTwo
        {
            get
            {
                return _lineTwo;
            }
            set
            {
                _lineTwo = value;
                this.NotifyPropertyChanged("LineTwo");
            }
        }

        public string TargetView
        {
            get
            {
                return _targetView;
            }

            set
            {
                _targetView = value;
                this.NotifyPropertyChanged("TargetView");
            }
        }

        public Brush BackgroundBrush
        {
            get
            {
                return _brush;
            }

            set
            {
                _brush = value;
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
