using System;
using System.Collections.Generic;
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

namespace TaskGrab.Pages.MainView
{
    /// <summary>
    /// Interaction logic for ListView.xaml
    /// </summary>
    public partial class ListView : Page
    {
        List<TaskControl> tasks = new List<TaskControl>();
        double scrollBarHeight = 50;
        public ListView()
        {
            InitializeComponent();
            for (int i = 0; i < 100; i++)
            {
                tasks.Add(new TaskControl()
                {
                    Title = "This is task number " + i,
                    Description = "This is a really long description that is sort of useless if you ask me lol now we need to add more to really make it a long ass description",
                    BackgroundColor = TaskControl.ODD,
                    Payment = i % 3 == 0 ? "Accepting Offers" : "$40",
                    Time = (i + 3) + " minutes ago",
                    Id = i
                }) ;
                tasks.Last().MessageClick += OnMessageClick;
                tasks.Last().TaskClick += OnTaskClick;
            }

            
            TasksHolder.ItemsSource = tasks;

        }

        private void OnMessageClick(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).GetHistory().GoTo("/Pages/MyTasks.xaml");
        }

        private void OnTaskClick(object sender, RoutedEventArgs e)
        {
            TaskControl task = (TaskControl)sender; 
            ((MainWindow)Application.Current.MainWindow).GetHistory().GoTo("/Pages/Task/TaskInfo.xaml?id=" + task.Id);
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
