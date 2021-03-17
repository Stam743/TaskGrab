using Google.Maps;
using Google.Maps.Geocoding;
using Google.Maps.StaticMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using TaskGrab.Navigation;

namespace TaskGrab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window 
    {
        string API_KEY = "AIzaSyDbQ_0Y3jYBzg1oxjbJzDfk4JwLgT2BHGY";
        CommunityLocationContext _context = new CommunityLocationContext();
        private bool menu_open = false;
        public History history;
        public MainWindow()
        {
            InitializeComponent();
            history = new History(this.MainWindowView, "/Pages/MainView/MapView.xaml");
            this.DataContext = history;
            GoogleSigned.AssignAllServices(new GoogleSigned(API_KEY));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated();
        }
        private void BackBtnClick(object sender, RoutedEventArgs e)
        {
           history.GoBack();
        }
        private void BtnOpenModal_Click(object sender, RoutedEventArgs e)
        {
            history.GoTo((string)((Button)sender).Tag);
        }

        public History GetHistory()
        {
            return history;
        }


        private void MenuToggle(object sender, RoutedEventArgs e)
        {
            Button menuButton = (Button)sender;
            if (menu_open)
            {
                menuButton.LayoutTransform = new RotateTransform(0);
                this.MenuActions.Height = 0;
                menu_open = false;

            }
            else
            {
                menuButton.LayoutTransform = new RotateTransform(90);
                this.MenuActions.Height = double.NaN;
                menu_open = true;
            }
        }

        private void SwitchClicked(object sender, RoutedEventArgs e)
        {
            Button switchButton = (Button)sender;
            bool switch_on_map = Regex.Match(history.current.OriginalString,@".*/MainView/MapView.xaml").Success;
            if (switch_on_map)
            {
                switchButton.Template = (ControlTemplate)this.FindResource("SwitchButtonTemplateList");
                history.Replace("Pages/MainView/ListView.xaml");
            }
            else
            {
                switchButton.Template = (ControlTemplate)this.FindResource("SwitchButtonTemplateMap");
                history.Replace("Pages/MainView/MapView.xaml");
            }
        }
    }
}
