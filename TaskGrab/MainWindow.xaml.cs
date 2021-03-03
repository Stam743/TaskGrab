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
        public MainWindow()
        {
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

        
    }
}
