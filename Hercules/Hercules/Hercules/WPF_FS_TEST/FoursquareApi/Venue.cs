using System;
using System.Net;

namespace Touchality.FoursquareApi
{
    public class Venue
    {
        public string id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string crossstreet { get; set; }
        public string geolat { get; set; }
        public string geolong { get; set; }

        public string distance { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string phone { get; set; }
        public string twitter { get; set; }

        public Stats stats { get; set; }
        public User mayor { get; set; }

        public double Dist
        {
            get
            {
                double result = double.MaxValue;
                double.TryParse(distance, out result);
                return result;
            }
        }
    }
}
