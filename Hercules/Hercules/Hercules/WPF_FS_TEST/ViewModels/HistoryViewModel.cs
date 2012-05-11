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

namespace WP7Square.ViewModels
{
    public class HistoryViewModel
    {
        public long HistoryId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Created { get; set; }
        public string Data { get; set; }
    }
}
