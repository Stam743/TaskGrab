using Microsoft.QueryStringDotNET;
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
using TaskGrab.Navigation;

namespace TaskGrab.Pages
{
    /// <summary>
    /// Interaction logic for GrabberChatView.xaml
    /// </summary>
    public partial class TaskInfo : Page
    {
        private MainWindow main;
        private History history;
        private QueryString query_string;
        public TaskInfo()
        {
            InitializeComponent();
            main = (MainWindow)Application.Current.MainWindow;
            history = main.GetHistory();

            string request_url = main.GetHistory().current.OriginalString;
            int query_start = request_url.IndexOf('?');

            if (query_start < 0)
                return;

            query_string = QueryString.Parse(request_url.Substring(query_start + 1));
        }

        private void bookmarkButton_Click(object sender, RoutedEventArgs e)
        {
            bookmarkButton.Visibility = Visibility.Hidden;
            bookmarkedButton.Visibility = Visibility.Visible;
        }

        private void taskInfoButton_Click(object sender, RoutedEventArgs e)
        {
            history.GoTo("Pages/GrabberChatView.xaml");
        }

        private void bookmarkedButton_Click(object sender, RoutedEventArgs e)
        {
            bookmarkedButton.Visibility = Visibility.Hidden;
            bookmarkButton.Visibility = Visibility.Visible;
        }
    }
}
