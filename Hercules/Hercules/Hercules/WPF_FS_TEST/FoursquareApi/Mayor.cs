using System;
using System.Net;

namespace Touchality.FoursquareApi
{
    public class Mayor
    {
        public string type { get; set; }
        public string checkins { get; set; }
        public User user { get; set; }
        public string message { get; set; }
        public int count { get; set; }
    }
}
