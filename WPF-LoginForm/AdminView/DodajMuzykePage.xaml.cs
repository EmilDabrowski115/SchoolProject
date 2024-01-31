using Microsoft.Win32;
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
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using WPF_LoginForm.Data;
using static WPF_LoginForm.Data.DataAccess;

namespace WPF_LoginForm.AdminView
{
    /// <summary>
    /// Logika interakcji dla klasy DodajMuzykePage.xaml
    /// </summary>
    public partial class DodajMuzykePage : UserControl
    {
        private byte[] zdjecieBytes;


        public DodajMuzykePage()
        {
            InitializeComponent();
        }

        private void WybierzZdjecie_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Pliki obrazów (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|Wszystkie pliki (*.*)|*.*",
                Title = "Wybierz obraz"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string sciezkaDoPliku = openFileDialog.FileName;

                // Konwertuj obraz na bajty
                zdjecieBytes = File.ReadAllBytes(sciezkaDoPliku);
            }
        }

        private void DodajButton_Click(object sender, RoutedEventArgs e)
        {
            // Sprawdź, czy wszystkie pola są uzupełnione
            if (string.IsNullOrEmpty(Utwor.Text) ||
                string.IsNullOrEmpty(Album.Text) ||
                string.IsNullOrEmpty(Wykonawca.Text) ||
                zdjecieBytes == null)
            {
                MessageBox.Show("Proszę uzupełnić wszystkie pola, włącznie z wyborem zdjęcia.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Wszystkie pola są uzupełnione, możemy kontynuować dodawanie utworu
            DodajUtwor();
        }

        private void DodajUtwor()
        {
            // Pobierz dane z pól tekstowych
            string nazwaUtworu = Utwor.Text;
            string album = Album.Text;
            string wykonawca = Wykonawca.Text;

            // Pobierz ścieżkę do wybranego pliku zdj
            byte[] zdjBytes = null; // Tutaj musisz przekształcić ścieżkę pliku zdj na byte[], ale wymaga to obsługi w XAML.

            // Utwórz obiekt MusicRecord
            MusicRecord musicRecord = new MusicRecord
            {
                NazwaUtworu = nazwaUtworu,
                Album = album,
                Wykonawca = wykonawca,
                ZDJ = zdjecieBytes
            };

            DataAccess dataAccess = new DataAccess();

            // Dodaj utwór do bazy danych
            bool success = dataAccess.AddMusicRecord(musicRecord);

            if (success)
            {
                MessageBox.Show("Dodano Utwor do bazy danych pomyslnie");

                // Tutaj możesz wykonać dodatkowe operacje po pomyślnym dodaniu utworu do bazy danych
            }
            else 
            {
                MessageBox.Show("taki utwor juz istnieje");

            }
        }

    }
}
