using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    /// Logika interakcji dla klasy ArtistsView.xaml
    /// </summary>
    public partial class ArtistsView : UserControl
    {
        public ArtistsView()
        {
            InitializeComponent();



            ObservableCollection<ArtistsItemControl> items = new ObservableCollection<ArtistsItemControl>
            {
            new ArtistsItemControl { DataContext = "Element 1" },
                // Dodaj więcej elementów, jeśli chcesz
             };
            ArtistsList.ItemsSource = items;

        }
    }
}
