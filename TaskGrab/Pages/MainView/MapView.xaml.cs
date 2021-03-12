using Google.Maps;
using Google.Maps.StaticMaps;
using System;
using System.Collections.Generic;
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

namespace TaskGrab.Pages.MainView
{
    /// <summary>
    /// Interaction logic for MapView.xaml
    /// </summary>
    public partial class MapView : Page
    {
        string API_KEY = "AIzaSyDbQ_0Y3jYBzg1oxjbJzDfk4JwLgT2BHGY";
        public MapView()
        {
            GoogleSigned.AssignAllServices(new GoogleSigned(API_KEY));
            InitializeComponent();
            MakeMap();
        }

        private void MakeMap()
        {
            double width = MainGrid.ActualWidth;
            double height = MainGrid.ActualHeight;

            var map = new StaticMapRequest();
            map.Center = new Location("358 Hawkstone Dr NW, Calgary AB T3G3T7");
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


            MapMarkers markers = new MapMarkers
            {
                MarkerSize = Google.Maps.MarkerSizes.Small,
                Label = "5",
                IconUrl = "http://shorturl.at/amyRY"

            };
            markers.Locations.Add(new Location("147 Citadel Meadows Grdns NW, Calgary AB"));

            map.Markers.AddRange(map.Markers.Append(markers));

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
