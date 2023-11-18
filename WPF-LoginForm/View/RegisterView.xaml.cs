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

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {

        }
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

       
        private int click_Count = 0;
        private void ChangeLanguae_Click(object sender, RoutedEventArgs e)
        {
            click_Count++;
            if (click_Count % 2 == 1)
            {
                Username.Text = "Użytkownik";
                Password.Text = "Hasło";
                btnRegister.Content = "Zarejestruj się";
                HaveAccount.Text = "Masz już konto? ";
                doLogowania.Content = "Zaloguj się";
                ChangeLanguae.Content = "Zmień język";
            }
            else
            {
                Username.Text = "Username";
                Password.Text = "Password";
                btnRegister.Content = "Register";
                HaveAccount.Text = "Have account already? ";
                doLogowania.Content = "Log in";
                ChangeLanguae.Content = "Change languae";
            }
           
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