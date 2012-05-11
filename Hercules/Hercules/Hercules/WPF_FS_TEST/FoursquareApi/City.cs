using System;
using System.Net;

namespace Touchality.FoursquareApi
{
    public class City : IComparable<City>
    {
        public string id { get; set; }
        public string name { get; set; }
        public string shortname { get; set; }
        public string timezone { get; set; }
        public double geolat { get; set; }
        public double geolong { get; set; }
        public double distance { get; set; }

        public static Comparison<City> DistanceComparison = (city1, city2) => city1.distance.CompareTo(city2.distance);

        public static Comparison<City> NameComparison = (city1, city2) => city1.name.CompareTo(city2.name);

        public int CompareTo(City other)
        {
            return name.CompareTo(other.name);
        }
    }
}
