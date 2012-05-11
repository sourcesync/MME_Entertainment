using System;
using System.Net;
using System.Collections.Generic;

namespace Touchality.FoursquareApi
{
    public class VenueGroup
    {
        public string Type { get; set; }
        public IList<Venue> Venues { get; set; }
    }
}
