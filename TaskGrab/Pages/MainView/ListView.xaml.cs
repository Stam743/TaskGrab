using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskGrab.Controls;
using TaskGrab.Data;

namespace TaskGrab.Pages.MainView
{
    /// <summary>
    /// Interaction logic for ListView.xaml
    /// </summary>
    public partial class ListView : Page
    {
        double scrollBarHeight = 50;
        TaskGrabContext _context = new();
        public ListView()
        {
            
            InitializeComponent();
            
            List<TaskControl> tasks = new List<TaskControl>();
            
            foreach (Data.Task task in _context.Tasks.Take(20))
            {
                tasks.Add(new TaskControl(task));
                tasks.Last().MessageClick += OnMessageClick;
                tasks.Last().TaskClick += OnTaskClick;
            }
            TasksHolder.ItemsSource = tasks;

        }

        private void OnMessageClick(object sender, RoutedEventArgs e)
        {
            TaskControl task = (TaskControl)sender;
            ((MainWindow)Application.Current.MainWindow).GetHistory().GoTo("Pages/TaskView/Grabber.xaml?id=" + task.Id + "&view=chat");
        }

        private void OnTaskClick(object sender, RoutedEventArgs e)
        {
            TaskControl task = (TaskControl)sender; 
            ((MainWindow)Application.Current.MainWindow).GetHistory().GoTo("Pages/TaskView/Grabber.xaml?id=" + task.Id + "&view=info");
        }

        private void OnScroll(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer currentViewer = (ScrollViewer)sender;

            if (currentViewer.ScrollableHeight > currentViewer.ActualHeight)
            {
                ScrollBarCustom.Visibility = Visibility.Visible;
                double offset = currentViewer.VerticalOffset / currentViewer.ScrollableHeight;
                double y1 = offset * (currentViewer.ActualHeight - scrollBarHeight);
                double y2 = y1 + scrollBarHeight;

                ScrollBarCustom.Y1 = y1;
                ScrollBarCustom.Y2 = y2;
            } else
            {
                ScrollBarCustom.Visibility = Visibility.Hidden;
            }
        }
    }
}
