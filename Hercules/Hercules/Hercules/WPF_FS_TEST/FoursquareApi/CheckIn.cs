using System;
using System.Net;
using System.Collections.Generic;

namespace Touchality.FoursquareApi
{
    public class CheckIn : IComparable<CheckIn>
    {

        public string id { get; set; }
        public string created { get; set; }
        public string timezone { get; set; }
        public string ismayor { get; set; }
        public double distance { get; set; }

        public string message { get; set; }        
        public User user { get; set; }
        public Venue venue { get; set; }
        public string display { get; set; }
        public string shout { get; set; }                

        public Mayor mayor { get; set; }
        public IList<Badge> badges { get; set; }
        public IList<Score> scoring { get; set; }
        public IList<Special> specials { get; set; }

        public static Comparison<CheckIn> DistanceComparison = (checkIn1, checkIn2) => checkIn1.distance.CompareTo(checkIn2.distance);

        public static Comparison<CheckIn> NameComparison = (checkIn1, checkIn2) => checkIn1.display.CompareTo(checkIn2.display);

        public int CompareTo(CheckIn other)
        {
            return display.CompareTo(other.display);
        }
    }
}
