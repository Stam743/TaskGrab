using Google.Maps;
using Google.Maps.Geocoding;
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

namespace TaskGrab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string API_KEY = "AIzaSyDbQ_0Y3jYBzg1oxjbJzDfk4JwLgT2BHGY";
        public MainWindow()
        {
            GoogleSigned.AssignAllServices(new GoogleSigned(API_KEY));
            InitializeComponent();

            
        }

        private void BtnOpenModal_Click(object sender, RoutedEventArgs e)
        {
            var page = ((Button)sender).Tag;
            Uri uri = new((string)page,UriKind.Relative);
            PopupFrame.Source = uri;
            ModalPopup.IsOpen = true;
        }

        private void SetOverlayShow(object sender, EventArgs e)
        {
            Overlay.Visibility = Visibility.Visible;
        }

        private void SetOverlayHide(object sender, EventArgs e)
        {
            Overlay.Visibility = Visibility.Hidden;
        }

        private void MakeMap(object sender, EventArgs e)
        {
            double width = MainGrid.ActualWidth;
            double height = MainGrid.ActualHeight;

            var map = new StaticMapRequest();
            map.Center = new Location("358 Hawkstone Dr NW, Calgary AB T3G3T7");
            map.Size = new MapSize((int) width, (int) height);
            map.Zoom = 14;
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
