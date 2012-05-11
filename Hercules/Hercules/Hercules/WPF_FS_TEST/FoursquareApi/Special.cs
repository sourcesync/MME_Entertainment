using System;
using System.Net;

namespace Touchality.FoursquareApi
{
    public class Special
    {
        public string id { get; set; }
        public string type { get; set; }
        public string message { get; set; }
        public Venue venue { get; set; }
    }
}
