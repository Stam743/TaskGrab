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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private MainWindow main;
        private History history;
        private QueryString query_string;
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
            loginTabButton.Background = new SolidColorBrush(Colors.SlateBlue);
            signUpTabButton.Background = new SolidColorBrush(Colors.BlueViolet);
            emailPhoneNumLabel.Visibility = Visibility.Visible;
            emailPhoneNumTextBox.Visibility = Visibility.Visible;
            passwordLabel.Visibility = Visibility.Visible;
            passwordTextBox.Visibility = Visibility.Visible;
            forgotButton.Visibility = Visibility.Visible;
            rememberMeCheckBox.Visibility = Visibility.Visible;
            loginButton.Visibility = Visibility.Visible;

            registerEmailPhoneNumLabel.Visibility = Visibility.Hidden;
            registerEmailPhoneNumTextBlock.Visibility = Visibility.Hidden;
            registerPasswordLabel.Visibility = Visibility.Hidden;
            registerPasswordTextBox.Visibility = Visibility.Hidden;
            confirmPasswordLabel.Visibility = Visibility.Hidden;
            confirmPasswordTextBox.Visibility = Visibility.Hidden;
            signUpButton.Visibility = Visibility.Hidden;
        }

        private void signUpTabButton_Click(object sender, RoutedEventArgs e)
        {
            signUpTabButton.Background = new SolidColorBrush(Colors.SlateBlue);
            loginTabButton.Background = new SolidColorBrush(Colors.BlueViolet);
            registerEmailPhoneNumLabel.Visibility = Visibility.Visible;
            registerEmailPhoneNumTextBlock.Visibility = Visibility.Visible;
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
    }
}
