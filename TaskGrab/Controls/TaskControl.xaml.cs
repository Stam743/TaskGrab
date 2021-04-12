using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using TaskGrab.Navigation;
using Microsoft.QueryStringDotNET;


namespace TaskGrab.Controls
{
    /// <summary>
    /// Interaction logic for TaskControl.xaml
    /// </summary>
    public partial class TaskControl : UserControl
    {
        private MainWindow main;
        private History history;
        private QueryString query_string;
        public TaskControl()
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

        private int id = 0;
        [Description("The id of the task"), Category("Data")]
        public int Id
        {
            get => id;
            set => id = value;
        }

        [Description("The title of the task"), Category("Data")]
        public string Title
        {
            get => TaskTitleText.Text;
            set => TaskTitleText.Text = value;
        }
        
        [Description("The time the task was posted"), Category("Data")]
        public string Time
        {
            get => TaskTimeText.Text;
            set => TaskTimeText.Text = value;
        }

        [Description("The description of the task"), Category("Data")]
        public string Description
        {
            get => DescriptionText.Text;
            set
            {
                try
                {
                    DescriptionText.Text = value.Substring(0,80)+"...";
                }
                catch (ArgumentOutOfRangeException)
                {
                    DescriptionText.Text = value;
                }
            }
        }

        [Description("The description of the task"), Category("Data")]
        public string Payment
        {
            get =>PaymentText.Text;
            set
            {
                PaymentText.Text = value;
                if (! Regex.Match(value, @"\$\d+").Success)
                {
                    PaymentText.FontSize = 10;
                    PaymentText.Padding = new Thickness(8);
                } else
                {
                    PaymentText.FontSize = 18;
                    PaymentText.Padding = new Thickness(10);
                }
            }
        }

        public event RoutedEventHandler MessageClick;

        void onMessageClick(object sender, RoutedEventArgs e)
        {
            if (this.MessageClick != null)
            {
                this.MessageClick(this, e);
            }
            e.Handled = true;
           
        }

        public event RoutedEventHandler TaskClick;

        void onTaskClick(object sender, RoutedEventArgs e)
        {
            if (this.TaskClick != null)
            {
                this.TaskClick(this, e);
            }
        }

        public TaskControl(Data.Task task)
        {
            InitializeComponent();
            this.Id = task.ID;
            this.Time = string.Format(" {0:%m} minutes ago"
            , (DateTime.Now - DateTime.Parse(task.posted)));
            this.Title = task.title;
            this.Description = task.description;
            this.Payment = task.payment;
        }
    }
}
