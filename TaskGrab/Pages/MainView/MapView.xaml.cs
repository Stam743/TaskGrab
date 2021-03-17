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
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// </summary>
    public partial class MapView : Page
    {

        string[] map_marker_icons = new string[] {
            "https://i.ibb.co/h9Z9zRJ/Icon1.png",
            "https://i.ibb.co/Z1yp95C/Icon2.png",
            "https://i.ibb.co/cg66zgM/Icon3.png",
            "https://i.ibb.co/QHJxR3b/Icon4.png",
            "https://i.ibb.co/3zyJjnC/Icon5.png",
            "https://i.ibb.co/m8ygwzP/Icon6.png",
            "https://i.ibb.co/ZJCKW3Z/Icon7.png",
            "https://i.ibb.co/ySzWbmp/Icon8.png",
            "https://i.ibb.co/M7RbK6Y/Icon9.png",
            "https://i.ibb.co/ckzGWR9/Icon10.png",
            "https://i.ibb.co/rbHrT6B/Icon10.png"
        };

        string[] map_marker_locations = new string[] {
            "Ranchlands, Calgary Nw",
            "Citadel, Calgary NW",
            "Arbour Lake, Calgary NW",
            "Dalhousie, Calgary NW",
            "Silver Springs, Calgary NW",
            "Simons Valley, Calgary NW"

        };

        Communities communities;

        //public string Icon1 = "https://i.ibb.co/h9Z9zRJ/Icon1.png";
        //public string Icon2 = "https://i.ibb.co/Z1yp95C/Icon2.png";
        //public string Icon3 = "https://i.ibb.co/cg66zgM/Icon3.png";
        //public string Icon4 = "https://i.ibb.co/QHJxR3b/Icon4.png";
        //public string Icon5 = "https://i.ibb.co/3zyJjnC/Icon5.png";
        //public string Icon6 = "https://i.ibb.co/m8ygwzP/Icon6.png";
        //public string Icon7 = "https://i.ibb.co/ZJCKW3Z/Icon7.png";
        //public string Icon8 = "https://i.ibb.co/ySzWbmp/Icon8.png";
        //public string Icon9 = "https://i.ibb.co/M7RbK6Y/Icon9.png";
        //public string Icon10 = "https://i.ibb.co/ckzGWR9/Icon10.png";
        //public string Icon10p = "https://i.ibb.co/rbHrT6B/Icon10.png";

        public MapView()
        {
            communities = new();
            InitializeComponent();
            MakeMap();
        }

        private void MakeMap()
        {
            double width = MainGrid.ActualWidth;
            double height = MainGrid.ActualHeight;

            

            var map = new StaticMapRequest();
            map.Center = new Google.Maps.Location("Hawkwood, Calgary AB");
            try
            {
                map.Size = new MapSize((int)width, (int)height);
            }
            catch
            {
                map.Size = new MapSize(480, 770);
            }
            map.Zoom = 13;
            map.Scale = 2;

            List<MapMarkers> markers = new();
            foreach (string location in map_marker_locations)
            {
                Ellipse marker = new Ellipse()
                {
                    Width = 50,
                    Height = 50,
                    
                };
                double center_point_x = communities.GetLocation("Hawkwood, Calgary AB").longitude;
                double center_point_y = communities.GetLocation("Hawkwood, Calgary AB").latitude;

                double scale = 144447.644200;
                double marker_x = center_point_x - communities.GetLocation(location).longitude;
                MarkerCanvas.Children.Add(marker);
                double slope = (480) / (256);
                double output = slope * (marker_x);
                Debug.WriteLine(output + "  oooolll  " + output * scale) ;
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
    }
}