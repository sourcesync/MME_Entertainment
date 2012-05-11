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
using dev.virtualearth.net.webservices.v1.common;
using dev.virtualearth.net.webservices.v1.imagery;

namespace WP7Square.Classes
{
    public static class MapHelper
    {
        // Bing Maps key
        public const string ApplicationId = null;

        public static FrameworkElement AddPushpin(Canvas canvas, Location centerLocation, Location pinLocation, int mapLvl, string text)
        {
            int pixelX = 0;
            int pixelY = 0;
            GeoUtils.LatLongToPixel(centerLocation.Latitude, centerLocation.Longitude, mapLvl, ref pixelX, ref pixelY);
            int offX = pixelX - (int)(canvas.Width / 2);
            int offY = pixelY - (int)(canvas.Height / 2);
            GeoUtils.LatLongToPixel(pinLocation.Latitude, pinLocation.Longitude, 16, ref pixelX, ref pixelY);
            pixelX = pixelX - offX;
            pixelY = pixelY - offY;

            Canvas pinCanvas = new Canvas { Width = 25, Height = 50 };
            
            // create the pin
            var p = new System.Windows.Shapes.Polygon();
            p.Points = new PointCollection
                               {
                                   new Point(0, 0), 
                                   new Point(25, 0), 
                                   new Point(25, 25), 
                                   new Point(0, 50),
                               };
            p.Fill = new SolidColorBrush(Colors.Black);
            p.Stroke = new SolidColorBrush(Colors.White);
            p.StrokeThickness = 2;
            pinCanvas.Children.Add(p);

            // add the text
            TextBlock txt = new TextBlock();
            txt.Text = text;
            txt.Margin = new Thickness(4, 2, 4, 0);
            txt.Foreground = new SolidColorBrush(Colors.White);
            txt.FontWeight = FontWeights.Bold;
            txt.FontSize = 14;
            pinCanvas.Children.Add(txt);

            pinCanvas.Margin = new Thickness(pixelX, pixelY - 50, 0, 0);
            canvas.Children.Add(pinCanvas);

            return pinCanvas;
        }

        public static void LoadMap(EventHandler<GetMapUriCompletedEventArgs> callback, double latitude, double longitude, int width, int height)
        {
            if(string.IsNullOrEmpty(ApplicationId))
            {
                MessageBox.Show("You need to get bing maps developer id http://www.bing.com/developers");
                return;
            }

            MapUriRequest mapUriRequest = new MapUriRequest();

            // Set credentials using a valid Bing Maps Key            
            mapUriRequest.Credentials = new Credentials();
            mapUriRequest.Credentials.ApplicationId = ApplicationId;

            // Set the location of the requested image
            mapUriRequest.Center = new dev.virtualearth.net.webservices.v1.common.Location();
            mapUriRequest.Center.Latitude = latitude;
            mapUriRequest.Center.Longitude = longitude;

            // Set the map style and zoom level
            MapUriOptions mapUriOptions = new MapUriOptions();
            mapUriOptions.Style = MapStyle.Road;
            mapUriOptions.ZoomLevel = 16;

            // Set the size of the requested image to match the size of the image control
            mapUriOptions.ImageSize = new SizeOfint();
            mapUriOptions.ImageSize.Height = height;
            mapUriOptions.ImageSize.Width = width;

            mapUriRequest.Options = mapUriOptions;

            ImageryServiceClient imageryService = new ImageryServiceClient("BasicHttpBinding_IImageryService");

            // Make the image request 
            imageryService.GetMapUriCompleted += callback;
            imageryService.GetMapUriAsync(mapUriRequest);


        }
    }
}
