using Google.Maps;
using Google.Maps.Geocoding;
using Google.Maps.StaticMaps;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskGrab.Data;
using TaskGrab.Util;

namespace TaskGrab.Pages.MainView
{

    public class DistanceAndAngle
    {
        public double Angle { get; set; }
        public double Distance { get; set; }
    }
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// </summary>
    public partial class MapView : Page
    {

        string[] map_marker_locations = new string[] {
            "Ranchlands, Calgary Nw",
            "Citadel, Calgary NW",
            "Arbour Lake, Calgary NW",
            "Dalhousie, Calgary NW",
            "Silver Springs, Calgary NW",
            "Simons Valley, Calgary NW"
        };

        Communities communities;


        public MapView()
        {
            communities = new();
            InitializeComponent();
            MakeMap("Citadel, Calgary AB", 13);
        }

        private void MakeMap(string center, int zoom)
        {
            double width = MainGrid.ActualWidth;
            double height = MainGrid.ActualHeight;

            double lat1 = communities.GetLocation(center).latitude;
            double lon1 = communities.GetLocation(center).longitude;

            double metersPerPx = 156543.03392 * Math.Cos(lat1 * Math.PI / 180) / Math.Pow(2, zoom);


            var map = new StaticMapRequest();
            map.Center = new Google.Maps.Location(center);
            try
            {
                map.Size = new MapSize((int)width, (int)height);
            }
            catch
            {
                width = 480;
                height = 770;
                map.Size = new MapSize((int) width, (int) height);
            }
            map.Zoom = zoom;
            map.Scale = 2;

            List<MapMarkers> markers = new();
            foreach (string location in map_marker_locations)
            {
                Button marker = new Button()
                {
                    Style = FindResource("MapMarkerButton") as Style,
                    Content = "22"
                };
                MarkerCanvas.Children.Add(marker);

                double lat2 = communities.GetLocation(location).latitude;
                double lon2 = communities.GetLocation(location).longitude;

                //double distance = distanceInKmBetweenEarthCoordinates(lat1, lon1, lat2, lon2) * 1000;
                //double angle = angleFromCoordinate(lat1, lon1, lat2, lon2);
                DistanceAndAngle da = getDAFromPoints(lat1, lon1, lat2, lon2);

                double x = (da.Distance / metersPerPx * Math.Cos(da.Angle));
                double y = (da.Distance / metersPerPx * Math.Sin(da.Angle));

                x = x + 240 - 25;
                y = (-y) + 385 - 25;

                Canvas.SetLeft(marker, x);
                Canvas.SetTop(marker, y);

            }


            map.Markers.AddRange(markers);


            StaticMapService service = new StaticMapService();
            BitmapImage img = new BitmapImage();
            try
            {
                img.BeginInit();
                img.StreamSource = service.GetStream(map);
                img.EndInit();
                this.Map.Source = img;
            }
            catch (Exception ex)
            {
                MessageBox.Show("A handled exception just occurred: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        double degreesToRadians(double degrees)
        {
            return (degrees * Math.PI) / 180.0;
        }

        private double angleFromCoordinate(double lat1, double long1, double lat2,double long2)
        {

            double dLon = degreesToRadians(long2 - long1);

            double y = Math.Sin(dLon) * Math.Cos(degreesToRadians(lat2));
            double x = Math.Cos(degreesToRadians(lat1)) * Math.Sin(degreesToRadians(lat2)) - Math.Sin(degreesToRadians(lat1))
                    * Math.Cos(degreesToRadians(lat2)) * Math.Cos(dLon);

            double brng = Math.Atan2(y, x);

            brng = ConvertRadiansToDegrees(brng);
            brng = (brng + 360.0) % 360.0;
            brng = 360.0 - brng; // count degrees counter-clockwise - remove to make clockwise

            return degreesToRadians(brng + 90);
        }

        double ConvertRadiansToDegrees(double radians)
        {
            double degrees = (180.0 / Math.PI) * radians;
            return (degrees);
        }
        double distanceInKmBetweenEarthCoordinates(double lat1, double lon1, double lat2, double lon2)
        {
            double earthRadiusKm = 6371.0;

            double dLat = degreesToRadians(lat2 - lat1);
            double dLon = degreesToRadians(lon2 - lon1);

            lat1 = degreesToRadians(lat1);
            lat2 = degreesToRadians(lat2);

            var a = Math.Sin(dLat / 2.0) * Math.Sin(dLat / 2.0) +
                    Math.Sin(dLon / 2.0) * Math.Sin(dLon / 2.0) * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));
            return earthRadiusKm * c;
        }

        DistanceAndAngle getDAFromPoints(double lat1, double lon1, double lat2, double lon2)
        {

            double dLon = degreesToRadians(lon2 - lon1);

            double cosLat1 = Math.Cos(degreesToRadians(lat1));
            double cosLat2 = Math.Cos(degreesToRadians(lat2));

            double y = Math.Sin(dLon) * cosLat2;
            double x = cosLat1 * Math.Sin(degreesToRadians(lat2)) - Math.Sin(degreesToRadians(lat1))
                    * cosLat2 * Math.Cos(dLon);

            double brng = Math.Atan2(y, x);

            brng = ConvertRadiansToDegrees(brng);
            brng = (brng + 360.0) % 360.0;
            brng = 360.0 - brng; // count degrees counter-clockwise - remove to make clockwise
            brng = degreesToRadians(brng + 90);

            double earthRadiusKm = 6371.0;

            double dLat = degreesToRadians(lat2 - lat1);

            var a = Math.Sin(dLat / 2.0) * Math.Sin(dLat / 2.0) +
                    Math.Sin(dLon / 2.0) * Math.Sin(dLon / 2.0) * cosLat1 * cosLat2;
            var c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));
            double distance = earthRadiusKm * c * 1000;

            return new DistanceAndAngle()
            {
                Distance = distance,
                Angle = brng   
            };
            
        }
    }
}