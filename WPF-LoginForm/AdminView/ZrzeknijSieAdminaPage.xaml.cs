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
using WPF_LoginForm.Data;

namespace WPF_LoginForm.AdminView
{
    /// <summary>
    /// Logika interakcji dla klasy ZrzeknijSieAdminaPage.xaml
    /// </summary>
    public partial class ZrzeknijSieAdminaPage : UserControl
    {
        public ZrzeknijSieAdminaPage()
        {
            InitializeComponent();
        }

        private void RemoveAdminPrivileges(object sender, RoutedEventArgs e)
        {
            string username = txtUser1.Text.Trim(); // Adjusted to use txtUser

            DataAccess dataAccess = new DataAccess();

            Tuple<bool, bool> loginResult = dataAccess.RemoveAdmin(username);

            // Instead of directly checking for a boolean, use Item1 from the tuple
            if (loginResult.Item1)
            {
                if (loginResult.Item2)
                {
                    // Admin login successful
                    MessageBox.Show("Admin Removed successfuly!");

                }
                else
                {
                    // Regular user login successful
                    MessageBox.Show("This User doesnt have admin privelages");

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
