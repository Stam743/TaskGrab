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
using TaskGrab.Data;
using TaskGrab.Controls;


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
        TaskGrabContext posted = new();
        string account = "Sharif";
        public MyTasks()
        {
            InitializeComponent();
            List<TaskControl> tasks = new List<TaskControl>();

            foreach (Data.Task task in posted.Tasks.Take(20))
            {
                if (task.poster == account)
                {
                    tasks.Add(new TaskControl(task));
                    tasks.Last().MessageClick += OnMessageClick;
                    tasks.Last().TaskClick += OnTaskClick;
                }

            }
            TasksHolder.ItemsSource = tasks;

            main = (MainWindow)Application.Current.MainWindow;
            history = main.GetHistory();

            string request_url = main.GetHistory().current.OriginalString;
            int query_start = request_url.IndexOf('?');

            if (query_start < 0)
                return;

            query_string = QueryString.Parse(request_url.Substring(query_start + 1));
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

        private void postedButton_Click(object sender, RoutedEventArgs e)
        {
            postedButton.Foreground = new SolidColorBrush(Colors.Yellow);
            savedButton.Foreground = new SolidColorBrush(Colors.DarkBlue);
            account = "Sharif";
            List<TaskControl> tasks = new List<TaskControl>();

            foreach (Data.Task task in posted.Tasks.Take(20))
            {
                if (task.poster == account)
                {
                    tasks.Add(new TaskControl(task));
                    tasks.Last().MessageClick += OnMessageClick;
                    tasks.Last().TaskClick += OnTaskClick;
                }

            }
            TasksHolder.ItemsSource = tasks;
        }

        private void savedButton_Click(object sender, RoutedEventArgs e)
        {
            savedButton.Foreground = new SolidColorBrush(Colors.Yellow);
            postedButton.Foreground = new SolidColorBrush(Colors.DarkBlue);
            account = "Dotty";
            List<TaskControl> tasks = new List<TaskControl>();

            foreach (Data.Task task in posted.Tasks.Take(20))
            {
                if (task.poster == account)
                {
                    tasks.Add(new TaskControl(task));
                    tasks.Last().MessageClick += OnMessageClick;
                    tasks.Last().TaskClick += OnTaskClick;
                }

            }
            TasksHolder.ItemsSource = tasks;
        }

        private void signOutButton_Click(object sender, RoutedEventArgs e)
        {
            history.GoTo("Pages/Login.xaml");
        }

        private void profileButton_Click(object sender, RoutedEventArgs e)
        {
            history.GoTo("Pages/Profile.xaml");
        }
    }
}
