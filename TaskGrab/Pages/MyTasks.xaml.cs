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
    /// Interaction logic for MyTasks.xaml
    /// </summary>
    public partial class MyTasks : Page
    {
        private MainWindow main;
        private History history;
        private QueryString query_string;
        public MyTasks()
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

        private void postedButton_Click(object sender, RoutedEventArgs e)
        {
            postedButton.Foreground = new SolidColorBrush(Colors.Yellow);
            savedButton.Foreground = new SolidColorBrush(Colors.DarkBlue);
        }

        private void savedButton_Click(object sender, RoutedEventArgs e)
        {
            savedButton.Foreground = new SolidColorBrush(Colors.Yellow);
            postedButton.Foreground = new SolidColorBrush(Colors.DarkBlue);
        }

        private void signOutButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
