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
using Microsoft.EntityFrameworkCore;


namespace TaskGrab.Pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private MainWindow main;
        private History history;
        private QueryString query_string;

        TaskGrabContext user_info = new TaskGrabContext();
        DbSet<Data.UserInfo> userInfo => user_info.UserInformation;

        public Login()
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

        private void loginTabButton_Click(object sender, RoutedEventArgs e)
        {
            loginTabButton.Foreground = new SolidColorBrush(Colors.Yellow);
            signUpTabButton.Foreground = new SolidColorBrush(Colors.White);
            emailPhoneNumLabel.Visibility = Visibility.Visible;
            emailPhoneNumTextBox.Visibility = Visibility.Visible;
            passwordLabel.Visibility = Visibility.Visible;
            passwordTextBox.Visibility = Visibility.Visible;
            forgotButton.Visibility = Visibility.Visible;
            rememberMeCheckBox.Visibility = Visibility.Visible;
            loginButton.Visibility = Visibility.Visible;

            registerEmailPhoneNumLabel.Visibility = Visibility.Hidden;
            registerEmailPhoneNumTextBox.Visibility = Visibility.Hidden;
            registerPasswordLabel.Visibility = Visibility.Hidden;
            registerPasswordTextBox.Visibility = Visibility.Hidden;
            confirmPasswordLabel.Visibility = Visibility.Hidden;
            confirmPasswordTextBox.Visibility = Visibility.Hidden;
            signUpButton.Visibility = Visibility.Hidden;
        }

        private void signUpTabButton_Click(object sender, RoutedEventArgs e)
        {
            signUpTabButton.Foreground = new SolidColorBrush(Colors.Yellow);
            loginTabButton.Foreground = new SolidColorBrush(Colors.White);
            registerEmailPhoneNumLabel.Visibility = Visibility.Visible;
            registerEmailPhoneNumTextBox.Visibility = Visibility.Visible;
            registerPasswordLabel.Visibility = Visibility.Visible;
            registerPasswordTextBox.Visibility = Visibility.Visible;
            confirmPasswordLabel.Visibility = Visibility.Visible;
            confirmPasswordTextBox.Visibility = Visibility.Visible;
            signUpButton.Visibility = Visibility.Visible;

            emailPhoneNumLabel.Visibility = Visibility.Hidden;
            emailPhoneNumTextBox.Visibility = Visibility.Hidden;
            passwordLabel.Visibility = Visibility.Hidden;
            passwordTextBox.Visibility = Visibility.Hidden;
            forgotButton.Visibility = Visibility.Hidden;
            rememberMeCheckBox.Visibility = Visibility.Hidden;
            loginButton.Visibility = Visibility.Hidden;
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {

            history.GoTo("Pages/MainView/MapView.xaml");
        }

        private void signUpButton_Click(object sender, RoutedEventArgs e)
        {
            string password = "";
            if(registerPasswordTextBox.Text == confirmPasswordTextBox.Text)
            {
                password = registerPasswordTextBox.Text;
            }
            TaskGrab.Data.UserInfo newUser = new Data.UserInfo()
            {
                Email_or_Phone_num = registerEmailPhoneNumTextBox.Text,
                Password = password

            };

            user_info.UserInformation.Add(newUser);
            user_info.SaveChanges();

            history.GoTo("Pages/MainView/MapView.xaml");
        }
    }
}
