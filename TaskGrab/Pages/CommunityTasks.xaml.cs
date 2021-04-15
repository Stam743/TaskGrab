using Microsoft.QueryStringDotNET;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using TaskGrab.Controls;
using TaskGrab.Data;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace TaskGrab.Pages
{
    /// <summary>
    /// Interaction logic for CommunityTasks.xaml
    /// </summary>
    public partial class CommunityTasks : Page, INotifyPropertyChanged
    {
        private MainWindow main;
        private History history;
        private QueryString query_string;
        private string location = "";
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public string Location
        {
            set { this.location = value; }
            get { return this.location; }
        }
        double scrollBarHeight = 50;
        TaskGrabContext _context = new();
        public CommunityTasks()
        {
            InitializeComponent();
            main = (MainWindow)Application.Current.MainWindow;
            history = main.GetHistory();

            string request_url = main.GetHistory().current.OriginalString;
            int query_start = request_url.IndexOf('?');

            if (query_start < 0)
                return;

            query_string = QueryString.Parse(request_url.Substring(query_start + 1));

            List<TaskControl> tasks = new List<TaskControl>();

            foreach (Data.Task task in _context.Tasks.Take(20))
            {
                tasks.Add(new TaskControl(task));
                tasks.Last().MessageClick += OnMessageClick;
                tasks.Last().TaskClick += OnTaskClick;
            }
            TasksHolder.ItemsSource = tasks;
            string loc = "";
            bool success = query_string.TryGetValue("location", out loc);
            if (success)
            {
                Location = loc;
            }
        }

        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void OnMessageClick(object sender, RoutedEventArgs e)
        {
            TaskControl task = (TaskControl)sender;
            ((MainWindow)Application.Current.MainWindow).GetHistory().GoTo("Pages/GrabberChatView.xaml?id=" + task.Id + "&view=chat");
        }

        private void OnTaskClick(object sender, RoutedEventArgs e)
        {
            TaskControl task = (TaskControl)sender;
            ((MainWindow)Application.Current.MainWindow).GetHistory().GoTo("Pages/TaskInfo.xaml?id=" + task.Id + "&view=info");
        }

        private void OnScroll(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer currentViewer = (ScrollViewer)sender;

            if (currentViewer.ScrollableHeight > currentViewer.ActualHeight)
            {
                ScrollBarCustom.Visibility = Visibility.Visible;
                double offset = currentViewer.VerticalOffset / currentViewer.ScrollableHeight;
                double y1 = offset * (currentViewer.ActualHeight - scrollBarHeight) + 70;
                double y2 = y1 + scrollBarHeight;

                ScrollBarCustom.Y1 = y1;
                ScrollBarCustom.Y2 = y2;
            }
            else
            {
                ScrollBarCustom.Visibility = Visibility.Hidden;
            }
        }
    }
}
