using System;

namespace WP7Square.Classes
{
    public class GeoUtils
    {
        //Constants for the Clustering
        //(addressable area in Bing Maps)
        private const double MinLatitude = -85.05112878;
        private const double MaxLatitude = 85.05112878;
        private const double MinLongitude = -180;
        private const double MaxLongitude = 180;

        private const double EquatorialEarthRadiusInMeters = 6378137;
        private const double AxisBInMeters = 6356752.31424518; // From WGS84 earth flattening coefficient definition.

        /// <summary>
        /// Converts a point from latitude/longitude WGS-84 coordinates (in degrees)
        /// into pixel XY coordinates at a specified level of detail.
        /// </summary>
        /// <param name="latitude">Latitude of the point, in degrees.</param>
        /// <param name="longitude">Longitude of the point, in degrees.</param>
        /// <param name="levelOfDetail">Level of detail, from 1 (lowest detail)
        /// to 23 (highest detail).</param>
        /// <param name="pixelX">Output parameter receiving the X coordinate in pixels.</param>
        /// <param name="pixelY">Output parameter receiving the Y coordinate in pixels.</param>
        public static void LatLongToPixel(double latitude, double longitude, int levelOfDetail, ref int pixelX, ref int pixelY)
        {
            latitude = Clip(latitude, MinLatitude, MaxLatitude);
            longitude = Clip(longitude, MinLongitude, MaxLongitude);

            double x = (longitude + 180) / 360;
            double sinLatitude = Math.Sin(latitude * Math.PI / 180);
            double y = 0.5 - Math.Log((1 + sinLatitude) / (1 - sinLatitude)) / (4 * Math.PI);

            UInt32 mapSize = Offset(levelOfDetail);
            pixelX = (int)Clip(x * mapSize + 0.5, 0, mapSize - 1);
            pixelY = (int)Clip(y * mapSize + 0.5, 0, mapSize - 1);
        }

        public static void PixelToLatLong(int pixelX, int pixelY, int lvl, ref double latitude, ref double longitude)
        {
            UInt32 mapSize = Offset(lvl);

            longitude = (((double)pixelX * 360) / (256 * Math.Pow(2, lvl))) - 180;
            double e = Math.Exp((0.5 - (double)pixelY / 256 / Math.Pow(2, lvl)) * 4 * Math.PI);
            latitude = Math.Asin((e - 1) / (e + 1)) * 180 / Math.PI;
        }

        /// <summary>
        /// Clips a number to the specified minimum and maximum values.
        /// </summary>
        /// <param name="n">The number to clip.</param>
        /// <param name="minValue">Minimum allowable value.</param>
        /// <param name="maxValue">Maximum allowable value.</param>
        /// <returns>The clipped value.</returns>
        private static double Clip(double n, double minValue, double maxValue)
        {
            return Math.Min(Math.Max(n, minValue), maxValue);
        }

        /// <summary>
        /// Determines the map width and height (in pixels) at a specified level
        /// of detail.
        /// </summary>
        /// <param name="levelOfDetail">Level of detail, from 1 (lowest detail)
        /// to 23 (highest detail).</param>
        /// <returns>The map width and height in pixels.</returns>
        public static UInt32 Offset(int levelOfDetail)
        {
            return (uint)(256 << levelOfDetail);
        }

        /// <summary>
        /// Computes the distance between points on the WGS84 ellipsoid.        
        /// </summary>
        /// <remarks>
        /// Took from http://www.mathworks.com/matlabcentral/fx_files/5379/1/vdist.m which is based on T. Vincenty's algorithm.
        /// Code is specifically license free.
        /// </remarks>
        /// <param name="latitudeA"></param>
        /// <param name="longitudeA"></param>
        /// <param name="latitudeB"></param>
        /// <param name="longitudeB"></param>
        /// <returns></returns>
        public static double CalculateDistance(double latitudeA, double longitudeA, double latitudeB, double longitudeB)
        {
            if (Math.Abs(latitudeA) > 90 || Math.Abs(latitudeB) > 90)
            {
                throw new ArgumentException();
            }

            // Convert to radians.
            latitudeA *= Math.PI / 180.0;
            longitudeA *= Math.PI / 180.0;
            latitudeB *= Math.PI / 180.0;
            longitudeB *= Math.PI / 180.0;

            // Correct for errors at exact poles by adjusting 0.6 millimeters.
            if (Math.Abs(Math.PI / 2.0 - Math.Abs(latitudeA)) < 1e-10)
            {
                latitudeA = Math.Sign(latitudeA) * (Math.PI / 2.0 - 1e-10);
            }
            if (Math.Abs(Math.PI / 2.0 - Math.Abs(latitudeB)) < 1e-10)
            {
                latitudeB = Math.Sign(latitudeB) * (Math.PI / 2.0 - 1e-10);
            }

            // Limit the longitude.
            longitudeA %= Math.PI * 2.0;
            longitudeB %= Math.PI * 2.0;
            double longitudeDelta = Math.Abs(longitudeB - longitudeA);
            if (longitudeDelta > Math.PI)
            {
                longitudeDelta = Math.PI * 2.0 - longitudeDelta;
            }

            double f = (EquatorialEarthRadiusInMeters - AxisBInMeters) / EquatorialEarthRadiusInMeters;
            double uA = Math.Atan((1.0 - f) * Math.Tan(latitudeA));
            double uB = Math.Atan((1.0 - f) * Math.Tan(latitudeB));
            double lambda = longitudeDelta;
            double lambdaOld = 0;
            double sigma = 0;
            double cos2Sigmam = 0;
            double alpha = 0;
            double cosAlpha2 = 0;

            int i = 0;
            do
            {
                if (i > 50)
                {
                    lambda = Math.PI;
                    break;
                }
                lambdaOld = lambda;

                double cosUA = Math.Cos(uA);
                double cosUB = Math.Cos(uB);
                double sinUA = Math.Sin(uA);
                double sinUB = Math.Sin(uB);

                double sinSigma = Math.Sqrt(
                      Math.Pow(cosUB * Math.Sin(lambda), 2.0) +
                      Math.Pow(cosUA * sinUB - sinUA * cosUB * Math.Cos(lambda), 2.0));
                double cosSigma =
                      sinUA * sinUB + cosUA * cosUB * Math.Cos(lambda);
                sigma = Math.Atan2(sinSigma, cosSigma);
                alpha = Math.Asin(cosUA * cosUB * Math.Sin(lambda) / Math.Sin(sigma));
                cosAlpha2 = Math.Pow(Math.Cos(alpha), 2.0);
                cos2Sigmam = Math.Cos(sigma) - 2.0 * sinUA * sinUB / cosAlpha2;
                double c = f / 16.0 * cosAlpha2 * (4.0 + f * (4.0 - 3.0 * cosAlpha2));
                lambda =
                      longitudeDelta + (1.0 - c) * f * Math.Sin(alpha) * (sigma + c * Math.Sin(sigma) *
                      (cos2Sigmam + c * Math.Cos(sigma) * (-1.0 + 2.0 * cos2Sigmam * cos2Sigmam)));

                // Correct for convergence failure in the case of essentially antipodal points.
                if (lambda > Math.PI)
                {
                    lambda = Math.PI;
                    break;
                }

                ++i;
            } while (Math.Abs(lambda - lambdaOld) > 1e-12);

            uB = cosAlpha2 * (EquatorialEarthRadiusInMeters * EquatorialEarthRadiusInMeters - AxisBInMeters * AxisBInMeters) / (AxisBInMeters * AxisBInMeters);
            double a = 1.0 + uB / 16384.0 * (4096.0 + uB * (-768.0 + uB * (320.0 - 175.0 * uB)));
            double b = uB / 1024.0 * (256.0 + uB * (-128.0 + uB * (74.0 - 47.0 * uB)));
            double deltaSigma =
                  b * Math.Sin(sigma) * (cos2Sigmam + b / 4.0 * (Math.Cos(sigma) * (-1.0 * 2.0 * cos2Sigmam * cos2Sigmam)
                  - b / 6.0 * cos2Sigmam * (-3.0 + 4.0 * Math.Pow(Math.Sin(sigma), 2.0)) * (-3.0 + 4.0 * cos2Sigmam * cos2Sigmam)));
            return AxisBInMeters * a * (sigma - deltaSigma);
        }
    }
}