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
using static WPF_LoginForm.Data.DataAccess;
using WPF_LoginForm.Models;

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

        private void GetStudioInfo(object sender, RoutedEventArgs e)
        {
            DataAccess dataAccess = new DataAccess();
            StudioInfo studioInfo = dataAccess.GetStudioInfo();

            if (studioInfo != null)
            {
                Models.StudioInfo studioInfoModel = new Models.StudioInfo(); // Instantiate StudioInfo model

                // Copy data from DataAccess.StudioInfo to Models.StudioInfo
                studioInfoModel.Id = studioInfo.Id;
                studioInfoModel.Nazwa = studioInfo.Nazwa;
                studioInfoModel.Logo = studioInfo.Logo;

                // Now you can use the studioInfoModel as needed
                // For example, you can pass it to another method or update the UI
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the registration view
            RegisterView registerView = new RegisterView();
            registerView.Show();

            // Close the current window (optional)
            this.Close();
        }


        private void MinimalizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void WindowReSize(object sender, RoutedEventArgs e)
        {
            click_Count++;
            if (click_Count % 2 == 1)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private int click_Count = 0;
        private void LanguaeBtn_Click(object sender, RoutedEventArgs e)
        {
            click_Count++;
            if (click_Count % 2 == 1)
            {
                Username.Text = "Nazwa uzytkownika";
                Password.Text = "Haslo";
                btnLogin.Content = "Zaloguj sie";
                registerButton.Content = "Zarejestuj sie";
                DontHave.Text = "Nie masz konta? ";
            }
            else
            {
                Username.Text = "Username";
                Password.Text = "Password";
                btnLogin.Content = "Log In";
                registerButton.Content = "Register";
                DontHave.Text = "Dont have an account? ";
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Simulate a click on the login button
                btnLogin_Click(sender, e);
            }
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


            Tuple<bool, bool> loginResult = dataAccess.AuthenticateUser(username, password);

            // Instead of directly checking for a boolean, use Item1 from the tuple
            if (loginResult.Item1)
            {
                if (loginResult.Item2)
                {
                    // Admin login successful
                    MessageBox.Show("Admin login successful!");

                    Models.UserSettings userSettings = new Models.UserSettings { Username = username };
                    userSettings.Save();
                    MainWindowAdmin mainWindow = new MainWindowAdmin();
                    mainWindow.Show();
                }
                else
                {
                    // Regular user login successful
                    MessageBox.Show("User login successful!");

                    Models.UserSettings userSettings = new Models.UserSettings { Username = username };
                    userSettings.Save();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                }

                // Continue with your navigation or other actions
                
                // Navigate to the main view or perform other actions here
                // For now, let's just close the current window
                this.Close();
            }
            else
            {
                MessageBox.Show("Login failed. Please check your credentials.");
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

    
    }
}
