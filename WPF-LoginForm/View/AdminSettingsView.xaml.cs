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
using WPF_LoginForm.AdminView;



namespace WPF_LoginForm.View
{
    /// <summary>
    /// Logika interakcji dla klasy AdminSettingsView.xaml
    /// </summary>
    public partial class AdminSettingsView : UserControl
    {
        public AdminSettingsView()
        {
            InitializeComponent();
            AdminSettingsFrame.Content = new DodajMuzykePage();
        }
        private void DodajMuzyke_Click(object sender, RoutedEventArgs e)
        {
            AdminSettingsFrame.Content = new DodajMuzykePage();
        }
    

        private void ZmienMuzyke_Click(object sender, RoutedEventArgs e)
        {
            AdminSettingsFrame.Content = new ZmienMuzykePage();
        }

        private void UsunMuzyke_Click(object sender, RoutedEventArgs e)
        {
            AdminSettingsFrame.Content = new UsunMuzykePage();
        }

        private void AwansujNaAdmina_Click(object sender, RoutedEventArgs e)
        {
            AdminSettingsFrame.Content = new AwansujNaAdminaPage();
        }

        private void ZrzeknijSieAdmina_Click(object sender, RoutedEventArgs e)
        {
            AdminSettingsFrame.Content = new ZrzeknijSieAdminaPage();

        }
        private void ZmienNazweStudia_Click(object sender, RoutedEventArgs e)
        {
            AdminSettingsFrame.Content = new ZmienNazweStudiaPage();

        }
        private void ZmienLogoAplikacji_Click(object sender, RoutedEventArgs e)
        {
            AdminSettingsFrame.Content = new ZmienLogoStudiaPage();

        }


    }
}