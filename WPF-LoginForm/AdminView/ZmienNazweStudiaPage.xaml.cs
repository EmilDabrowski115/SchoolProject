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
using WPF_LoginForm.Models;

namespace WPF_LoginForm.AdminView
{
    /// <summary>
    /// Logika interakcji dla klasy ZmienNazweStudiaPage.xaml
    /// </summary>
    public partial class ZmienNazweStudiaPage : UserControl
    {

        Models.StudioInfo studioInfoModel;  // Declare at class level

        public ZmienNazweStudiaPage()
        {
            InitializeComponent();
            GetStudioInfo();
        }

        private void GetStudioInfo()
        {
            DataAccess dataAccess = new DataAccess();
            StudioInfo studioInfo = dataAccess.GetStudioInfo();

            if (studioInfo != null)
            {
                studioInfoModel = new Models.StudioInfo
                {
                    Id = studioInfo.Id,
                    NazwaStudia = studioInfo.NazwaStudia,
                    LogoStudia = studioInfo.LogoStudia
                };
                StudioNameBlock.Text = studioInfoModel.NazwaStudia;

            }
        }

        private void OnUpdateStudioNameClick(object sender, RoutedEventArgs e)
        {
            string NewStudioName = NewStudioNameBox.Text.Trim(); // Adjusted to use txtUser
            DataAccess dataAccess = new DataAccess();

            if (NewStudioName == null)
            {
                MessageBox.Show("Please Enter a studio Name first");

            }

            StudioInfo studioInfo = dataAccess.UpdateStudioName(NewStudioName);

            if (studioInfo != null)
            {
                studioInfoModel = new Models.StudioInfo
                {
                    Id = studioInfo.Id,
                    NazwaStudia = studioInfo.NazwaStudia,
                    LogoStudia = studioInfo.LogoStudia
                };
                StudioNameBlock.Text = studioInfoModel.NazwaStudia;
                MessageBox.Show("Successfully changed the name of the studio");


            }
            else 
            {
                MessageBox.Show("Failed to Update Studio Name");
                


            }



        }

    }
}
