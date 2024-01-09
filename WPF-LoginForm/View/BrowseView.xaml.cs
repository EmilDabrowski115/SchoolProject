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
using WPF_LoginForm.Viewmodel;


namespace WPF_LoginForm.View
{
    /// <summary>
    /// Logika interakcji dla klasy BrowseView.xaml
    /// </summary>
    public partial class BrowseView : UserControl
    {
        public BrowseView()
        {
            InitializeComponent();
            contentControl.Content = new SongsView();
        }
        private void zmianaWybierania(object sender, SelectionChangedEventArgs e)
        {
            if (wybieranie != null && contentControl != null && wybieranie.SelectedItem != null && wybieranie.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedOption = selectedItem.Content.ToString();

                // W zależności od wybranej opcji ustaw odpowiedni widok
                switch (selectedOption)
                {
                    case "Artists":
                        contentControl.Content = new ArtistsView(); // Załóżmy, że masz widok dla artystów o nazwie ArtistsView
                        break;
                    case "Albums":
                        contentControl.Content = new AlbumsView(); // Załóżmy, że masz widok dla albumów o nazwie AlbumsView
                        break;
                    case "Songs":
                        contentControl.Content = new SongsView(); // Załóżmy, że masz widok dla utworów o nazwie SongsView
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
