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
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class Index : Page
    {
        private bool switch_on_map = true;
        public Index()
        {
            InitializeComponent();
        }

        private void SwitchClicked(object sender, RoutedEventArgs e)
        {
            Button switchButton = (Button)sender;
            if (switch_on_map)
            {
                switchButton.Template = (ControlTemplate)this.FindResource("SwitchButtonTemplateList");
                Uri uri = new("ListView.xaml", UriKind.Relative);
                MainViewFrame.Source = uri;
                switch_on_map = false;
            }
            else
            {
                switchButton.Template = (ControlTemplate)this.FindResource("SwitchButtonTemplateMap");
                Uri uri = new("MapView.xaml", UriKind.Relative);
                MainViewFrame.Source = uri;
                switch_on_map = true;
            }
        }

       
    }
}
