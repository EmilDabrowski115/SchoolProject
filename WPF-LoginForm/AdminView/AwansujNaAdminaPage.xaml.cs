using System;
using System.Windows;
using System.Windows.Controls;
using WPF_LoginForm.Data;


namespace WPF_LoginForm.AdminView
{
    /// <summary>
    /// Logika interakcji dla klasy AwansujNaAdminaPage.xaml
    /// </summary>
    public partial class AwansujNaAdminaPage : UserControl
    {
        public AwansujNaAdminaPage()
        {
            InitializeComponent();
        }

        private void AddAdminPrivileges(object sender, RoutedEventArgs e)
        {
            string username = txtUser.Text.Trim(); // Adjusted to use txtUser

            DataAccess dataAccess = new DataAccess();

            Tuple<bool, bool> loginResult = dataAccess.AddAdmin(username);

            // Instead of directly checking for a boolean, use Item1 from the tuple
            if (loginResult.Item1)
            {
                if (loginResult.Item2)
                {
                    // Admin login successful
                    MessageBox.Show("Admin Added successfuly!");
                    
                }
                else
                {
                    // Regular user login successful
                    MessageBox.Show("This User already has Admin privelages");
                    
                }

                // Continue with your navigation or other actions

                // Navigate to the main view or perform other actions here
                // For now, let's just close the current window
                
            }
            else
            {
                MessageBox.Show("No Such User Exists");
            }
            // Login failed - no need for an else block, as error messages are shown within the AuthenticateUser method
        }


    }
}
