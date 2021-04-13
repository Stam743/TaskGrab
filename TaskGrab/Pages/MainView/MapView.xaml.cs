using Google.Maps;
using Google.Maps.Geocoding;
using Google.Maps.StaticMaps;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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

        Communities communities;
        Dictionary<string, int> community_count;
        
        int current_zoom = 13;

        double width;
        double height;

        double center_lat;
        double center_lon;

        public MapView()
        {
            communities = new();
            InitializeComponent();
            GenerateCommunitiesCount();
            SetCenterLocation("Citadel, Calgary AB");
            MakeMap();

        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                current_zoom++;

            else if (e.Delta < 0)
                current_zoom--;

            if (current_zoom > 20)
                current_zoom = 20;
            else if (current_zoom < 1)
                current_zoom = 1;

            MakeMap();
        }

        private void GenerateCommunitiesCount()
        {
            TaskGrabContext task_grab_context;
            task_grab_context = new TaskGrabContext();
            community_count = new Dictionary<string, int>();
            Debug.WriteLine("Counting tasks in communities...");
            DbSet<Data.Task> tasks = task_grab_context.Tasks;
            foreach (Data.Task task in tasks)
            {

                if (community_count.ContainsKey(task.location))
                    community_count[task.location] += 1;
                else
                    community_count.Add(task.location, 1);
            }
        }
        private void MakeMap()
        {
            DrawMap();

            double metersPerPx = 156543.03392 * Math.Cos(center_lat * Math.PI / 180) / Math.Pow(2, current_zoom);

            List<MapMarkers> markers = new();

            MarkerCanvas.Children.Clear();

            int[] x_points = new int[community_count.Count];
            double marker_size = current_zoom * 3.84;
            
            foreach (KeyValuePair<string, int> kvp in community_count)
            {
       
                Util.Location community_location = communities.GetLocation(kvp.Key);

                if (community_location == null)
                    continue;
               
                double c_lat = community_location.latitude;
                double c_lon = community_location.longitude;


                DistanceAndAngle da = getDAFromPoints(center_lat, center_lon, c_lat, c_lon);

                double x = (da.Distance / metersPerPx * Math.Cos(da.Angle));
                double y = (da.Distance / metersPerPx * Math.Sin(da.Angle));

                x = x + (480.0 / 2.0);
                y = (-y) + (770.0 / 2.0);

                if (x > 480 || x < 0)
                    continue;

                if (y > 770 || y < 0)
                    continue;

                Button marker = new Button()
                {
                    Style = FindResource("MapMarkerButton") as Style,
                    Width = marker_size,
                    Height = marker_size,
                    Content = kvp.Value,
                    Tag = kvp.Key
                };
              
                marker.Click += onMarkerClick;

                MarkerCanvas.Children.Add(marker);
                Canvas.SetLeft(marker, x);
                Canvas.SetTop(marker, y);
            }
        }


        private void onMarkerClick(object sender, RoutedEventArgs e)
        {
            Button clicked = (Button)sender;
            ((MainWindow)Application.Current.MainWindow).GetHistory().GoTo("/Pages/CommunityTasks.xaml?location=" + clicked.Tag);
        }
        private void SetCenterLocation(string center)
        {
            width = MainGrid.ActualWidth;
            height = MainGrid.ActualHeight;

            center_lat = communities.GetLocation(center).latitude;
            center_lon = communities.GetLocation(center).longitude;
        } 

        private void DrawMap()
        {
            var map = new StaticMapRequest();
            map.Center = new Google.Maps.Location(center_lat + "," + center_lon);
            try
            {
                map.Size = new MapSize((int)width, (int)height);
            }
            catch
            {
                width = 500;
                height = 770;
                map.Size = new MapSize((int)width, (int)height);
            }
            map.Zoom = current_zoom;
            map.Scale = 2;


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

        private double degreesToRadians(double degrees)
        {
            return (degrees * Math.PI) / 180.0;
        }

        private double ConvertRadiansToDegrees(double radians)
        {
            double degrees = (180.0 / Math.PI) * radians;
            return (degrees);
        }

        private DistanceAndAngle getDAFromPoints(double lat1, double lon1, double lat2, double lon2)
        {

            double dLon = degreesToRadians(lon2 - lon1);
            double dLat = degreesToRadians(lat2 - lat1);

            lat1 = degreesToRadians(lat1);
            lat2 = degreesToRadians(lat2);

            double cosLat1 = Math.Cos(lat1);
            double cosLat2 = Math.Cos(lat2);

            double y = Math.Sin(dLon) * cosLat2;
            double x = cosLat1 * Math.Sin(lat2) - Math.Sin(lat1)
                    * cosLat2 * Math.Cos(dLon);

            double brng = Math.Atan2(y, x);

            brng = ConvertRadiansToDegrees(brng);
            brng = (brng + 360.0) % 360.0;
            brng = 360.0 - brng; // count degrees counter-clockwise - remove to make clockwise
            brng = degreesToRadians(brng + 90);

            double earthRadiusKm = 6371.0;

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


        double x_start;
        double x_end;
        double y_start;
        double y_end;
        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow window = (MainWindow)Application.Current.MainWindow;
            Point cursor_point = Map.PointToScreen(Mouse.GetPosition(Map));


            x_start = cursor_point.X - Map.ActualWidth / 2.0;
            y_start = (cursor_point.Y - Map.ActualHeight / 2.0);

        }

        private void Page_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow window = (MainWindow)Application.Current.MainWindow;
            Point cursor_point = Map.PointToScreen(Mouse.GetPosition(Map));


            x_end = cursor_point.X - Map.ActualWidth / 2.0;
            y_end = (cursor_point.Y - Map.ActualHeight / 2.0);

            double move_factor = 0.00008;

            if ( x_end > x_start) // Went Upwatds, so we move downwards
            {
                center_lon -= move_factor * Math.Abs(x_end - x_start);
            } else if ( x_end < x_start) // Went down so we go up
            {
                center_lon += move_factor * Math.Abs(x_end - x_start);
            }

            if ( y_end > y_start)
            {
                center_lat += move_factor * Math.Abs(y_end - y_start);
            }else if (y_end < y_start)
            {
                center_lat -= move_factor * Math.Abs(y_end - y_start);
            }

            MakeMap();
        }
    }
}