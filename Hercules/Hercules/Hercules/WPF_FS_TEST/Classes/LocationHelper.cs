using System.Device.Location;
using System.Windows;
using System.Windows.Controls;

namespace WP7Square.Classes
{
    public static class LocationHelper
    {
        private static Location _locationFromLastGpsUpdate = null;
        private static GeoPositionStatus _geoPositionStatus = GeoPositionStatus.Initializing;

        private static GeoCoordinateWatcher _geoWatcher = null;

        public static Location GetCurrentLocation()
        {
            //if (_geoWatcher == null)
            //{
            //    _geoWatcher = new GeoCoordinateWatcher();                
            //    _geoWatcher.PositionChanged += geoWatcher_PositionChanged;
            //    _geoWatcher.StatusChanged += _geoWatcher_StatusChanged;
            //    _geoWatcher.TryStart(false, TimeSpan.FromSeconds(10));                            
            //}

            //if (_locationFromLastGpsUpdate == null && (_geoWatcher.Position == null || _geoWatcher.Position.Location == null))
            //{
            return new Location
            {
                Latitude = 38.898748,
                Longitude = -77.037684
            };
            //}

            //_locationFromLastGpsUpdate = new Location
            //{
            //    Latitude = _geoWatcher.Position.Location.Latitude,
            //    Longitude = _geoWatcher.Position.Location.Longitude,
            //};

            //return _locationFromLastGpsUpdate;
        }

        static void _geoWatcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            _geoPositionStatus = e.Status;
        }

        static void geoWatcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            _locationFromLastGpsUpdate = new Location
            {
                Latitude = _geoWatcher.Position.Location.Latitude,
                Longitude = _geoWatcher.Position.Location.Longitude,
            };
        }

        public static Location ToLocation(string geoLat, string geoLong)
        {
            double latitude = 0.0;
            double longitude = 0.0;
            if (double.TryParse(geoLat, out latitude) && double.TryParse(geoLong, out longitude))
            {
                return new Location
                {
                    Latitude = latitude,
                    Longitude = longitude
                };
            }
            return GetCurrentLocation();
        }

        public static Pulsar AddMyLocation(Canvas canvas)
        {
            double pulsarOffset = 58.5;
            Pulsar p = new Pulsar
                           {
                               Margin = new Thickness((canvas.Width / 2) - pulsarOffset, (canvas.Height / 2) - pulsarOffset, 0, 0)
                           };
            canvas.Children.Add(p);
            return p;
        }

    }
}
