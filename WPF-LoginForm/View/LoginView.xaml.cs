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
using System.Windows.Shapes;
using System.Windows.Navigation;
using WPF_LoginForm.Data;

namespace WPF_LoginForm.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>

    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the registration view
            RegisterView registerView = new RegisterView();
            registerView.Show();

            // Close the current window (optional)
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUser.Text.Trim(); // Adjusted to use txtUser
            string password = txtPass.Password.Trim(); // Adjusted to use txtPass

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            DataAccess dataAccess = new DataAccess();

            // Call the AuthenticateUser method to attempt login
            if (dataAccess.AuthenticateUser(username, password))
            {
                // Login successful, you can navigate to the main view or perform other actions
                // For now, let's show a message and close the current window
                
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                // Navigate to the main view or perform other actions here
                // For now, let's just close the current window
                this.Close();
            }
            // Login failed - no need for an else block, as error messages are shown within the AuthenticateUser method
        }

       


        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            // Get the target URI from the Hyperlink
            string targetUri = e.Uri.OriginalString;

            // Create an instance of the target window and show it
            Window targetWindow = (Window)Activator.CreateInstance(Type.GetType(targetUri));
            targetWindow.Show();

            // Close the current window (optional)
            this.Close();
        }

        private int click_Count=0;
        private void ChangeLanguae_Click(object sender, RoutedEventArgs e)
        {
            click_Count++;
            if (click_Count % 2 == 1)
            {
                ChangeLanguae.Content = "Zmień Język";
                Username.Text = "Użytkownik";
                Password.Text = "Hasło";
                btnLogin.Content = "Zaloguj się";
                DontHave.Text = "Nie masz konta?";
                registerButton.Content = "Zarejestruj się";
            }
            else
            {
                ChangeLanguae.Content = "Change Languar";
                Username.Text = "Username";
                Password.Text = "Password";
                btnLogin.Content = "Log In";
                DontHave.Text = "Don't have an account?";
                registerButton.Content = "Register";
            }
        }
    }
}
