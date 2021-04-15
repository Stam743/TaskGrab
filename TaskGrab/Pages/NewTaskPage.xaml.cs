using Microsoft.EntityFrameworkCore;
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
using TaskGrab.Data;
using TaskGrab.Navigation;

namespace TaskGrab.Pages
{
    /// <summary>
    /// Interaction logic for NewTaskPage.xaml
    /// </summary>
    public partial class NewTaskPage : Page
    {
        private MainWindow main;
        private History history;
        private QueryString query_string;

        TaskGrabContext task_grab_context = new TaskGrabContext();
        DbSet<Data.Task> tasks => task_grab_context.Tasks;


        public NewTaskPage()
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


        private void setAmountButton_Click(object sender, RoutedEventArgs e)
        {
            setAmountButton.Foreground = new SolidColorBrush((Color) ColorConverter.ConvertFromString("#f3c206"));
            acceptOffersButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#defefe"));
            volunteerButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#defefe"));
            setAmountLabel.Visibility = Visibility.Visible;
            setAmountTextBox.Visibility = Visibility.Visible;
            acceptingOffersLabel.Visibility = Visibility.Hidden;
            volunteerWorkLabel.Visibility = Visibility.Hidden;
        }

        private void acceptOffersButton_Click(object sender, RoutedEventArgs e)
        {
            acceptOffersButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f3c206"));
            setAmountButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#defefe"));
            volunteerButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#defefe"));
            acceptingOffersLabel.Visibility = Visibility.Visible;
            setAmountLabel.Visibility = Visibility.Hidden;
            setAmountTextBox.Visibility = Visibility.Hidden;
            volunteerWorkLabel.Visibility = Visibility.Hidden;

        }

        private void volunteerButton_Click(object sender, RoutedEventArgs e)
        {
            volunteerButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f3c206"));
            volunteerWorkLabel.Visibility = Visibility.Visible;
            acceptOffersButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#defefe"));
            setAmountButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#defefe"));
            acceptingOffersLabel.Visibility = Visibility.Hidden;
            setAmountLabel.Visibility = Visibility.Hidden;
            setAmountTextBox.Visibility = Visibility.Hidden;
        }

        private void postTaskButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime currentTime = DateTime.Now;

            string paymentChoice = "";

            if (volunteerWorkLabel.Visibility == Visibility.Visible)
            {
                paymentChoice = "Volunteer Work";
            }
            else if (acceptingOffersLabel.Visibility == Visibility.Visible)
            {
                paymentChoice = "Accepting Offers";
            }
            else
            {
                paymentChoice = setAmountTextBox.Text;
            }


            if(locationTextBox.Text != "" && titleTextBox.Text != "" && descriptionTextBox.Text != "")
            {
                TaskGrab.Data.Task newTask = new Data.Task()
                {
                    title = titleTextBox.Text,
                    description = descriptionTextBox.Text,
                    posted = currentTime.ToString("yyyy-MM-dd,hh:mm:ss"),
                    poster = "Sharif",
                    payment = paymentChoice,
                    location = locationTextBox.Text
                };

                task_grab_context.Tasks.Add(newTask);
                task_grab_context.SaveChanges();
                errorLabel.Visibility = Visibility.Hidden;

                history.GoTo("Pages/Login.xaml");
            }
            else
            {
                errorLabel.Visibility = Visibility.Visible;
            }

            
        }

        private void locationButton_Click(object sender, RoutedEventArgs e)
        {
            locationTextBox.Text = "Foothills, Calgary AB";
        }
    }
}
