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
using System.Data.SQLite;
using WPF_LoginForm.Models;
using WPF_LoginForm.Data;

namespace WPF_LoginForm.View
{

    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Window
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void MinimalizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
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
                Password.Text= "Haslo";
                btnRegister.Content="Zarejestuj sie";
                LogInButton.Content = "Zaloguj sie";
                DontHave.Text = "Masz już konto? ";
            }
            else
            {
                Username.Text = "Username";
                Password.Text = "Password";
                btnRegister.Content = "Register";
                LogInButton.Content = "Log In";
                DontHave.Text = "Already have an account? ";
            }
        }


        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Password.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            DataAccess dataAccess = new DataAccess();

            // Create a UserModel instance and set the properties
            UserModel user = new UserModel
            {
                Username = username,
                Password = password
            };

            // Call the RegisterUser method to attempt registration
            if (dataAccess.RegisterUser(user))
            {
                // Registration successful, you can navigate to the login view or perform other actions
                LoginView loginView = new LoginView();
                loginView.Show();
                this.Close();
            }
            // Registration failed - no need for an else block, as error messages are shown within the RegisterUser method
        }

        private void DoLogowania_click(object sender, RoutedEventArgs e)
        {
            //navigate to the login view
            LoginView loginView = new LoginView();
            loginView.Show();
            //close the current window
            this.Close();

        }
        

       
        
    }
}
/*
  private void DoLogowania_click(object sender, RoutedEventArgs e)
        {
            //navigate to the login view
            LoginView loginView = new LoginView();
            loginView.Show();
            //close the current window
            this.Close();

        }
        private void DoMaina_click(object sender, RoutedEventArgs e)
        {
            //navigate to the login view
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            //close the current window
            this.Close();

        }
 
 
 
 */