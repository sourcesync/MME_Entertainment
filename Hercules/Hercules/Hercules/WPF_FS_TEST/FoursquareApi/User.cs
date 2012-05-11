using System;
using System.Net;
using System.Collections.Generic;

namespace Touchality.FoursquareApi
{
    public class User
    {
        public string id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string photo { get; set; }
        public string gender { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string friendstatus { get; set; }
        public string city { get; set; }
        public CheckIn checkin { get; set; }
        public Venue venue { get; set; }
        public IList<Badge> badges { get; set; }
    }
}
