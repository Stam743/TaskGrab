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
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        private MainWindow main;
        private History history;
        private QueryString query_string;
        public Profile()
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

        private void editNameButton_Click(object sender, RoutedEventArgs e)
        {
            displayNameLabel.Visibility = Visibility.Hidden;
            nameTextBox.Visibility = Visibility.Visible;
            editNameButton.Visibility = Visibility.Hidden;
            doneEditNameButton.Visibility = Visibility.Visible;
            

        }

        private void doneEditNameButton_Click(object sender, RoutedEventArgs e)
        {
            displayNameLabel.Content = nameTextBox.Text;
            displayNameLabel.Visibility = Visibility.Visible;
            nameTextBox.Visibility = Visibility.Hidden;
            editNameButton.Visibility = Visibility.Visible;
            doneEditNameButton.Visibility = Visibility.Hidden;
        }

        private void editEmailButton_Click(object sender, RoutedEventArgs e)
        {
            displayEmailLabel.Visibility = Visibility.Hidden;
            emailTextBox.Visibility = Visibility.Visible;
            editEmailButton.Visibility = Visibility.Hidden;
            doneEditEmailButton.Visibility = Visibility.Visible;
        }

        private void doneEditEmailButton_Click(object sender, RoutedEventArgs e)
        {
            displayEmailLabel.Content = emailTextBox.Text;
            displayEmailLabel.Visibility = Visibility.Visible;
            emailTextBox.Visibility = Visibility.Hidden;
            editEmailButton.Visibility = Visibility.Visible;
            doneEditEmailButton.Visibility = Visibility.Hidden;
        }

        private void signOutButton_Click(object sender, RoutedEventArgs e)
        {
            history.GoTo("Pages/Login.xaml");
        }

        private void changePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            passwordLabel.Visibility = Visibility.Hidden;
            changePasswordButton.Visibility = Visibility.Hidden;
            oldPasswordLabel.Visibility = Visibility.Visible;
            oldPasswordTextBox.Visibility = Visibility.Visible;
            newPasswordLabel.Visibility = Visibility.Visible;
            newPasswordTextBox.Visibility = Visibility.Visible;
            newPasswordLabel2.Visibility = Visibility.Visible;
            newPasswordTextBox2.Visibility = Visibility.Visible;
            savePasswordButton.Visibility = Visibility.Visible;
        }

        private void savePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            passwordLabel.Visibility = Visibility.Visible;
            changePasswordButton.Visibility = Visibility.Visible;
            oldPasswordLabel.Visibility = Visibility.Hidden;
            oldPasswordTextBox.Visibility = Visibility.Hidden;
            newPasswordLabel.Visibility = Visibility.Hidden;
            newPasswordTextBox.Visibility = Visibility.Hidden;
            newPasswordLabel2.Visibility = Visibility.Hidden;
            newPasswordTextBox2.Visibility = Visibility.Hidden;
            savePasswordButton.Visibility = Visibility.Hidden;
        }
    }
}
