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
using static WPF_LoginForm.Data.DataAccess;
using WPF_LoginForm.Data;

namespace WPF_LoginForm.AdminView
{
    /// <summary>
    /// Logika interakcji dla klasy UsunMuzykePage.xaml
    /// </summary>
    public partial class UsunMuzykePage : UserControl
    {
        public UsunMuzykePage()
        {
            InitializeComponent();
        }

        private void UsunUtwor(object sender, RoutedEventArgs e)
        {
            // Pobierz dane z pól tekstowych
            string nazwaUtworu = Utwor.Text;
            


            

            DataAccess dataAccess = new DataAccess();

            // Dodaj utwór do bazy danych
            bool success = dataAccess.RemoveMusicRecord(nazwaUtworu);

            if (success)
            {
                MessageBox.Show("Usunieto Utwor pomyslnie");

                // Tutaj możesz wykonać dodatkowe operacje po pomyślnym dodaniu utworu do bazy danych
            }
            else
            {
                MessageBox.Show("taki utwor nie istnieje");

            }
        }
    }
}
