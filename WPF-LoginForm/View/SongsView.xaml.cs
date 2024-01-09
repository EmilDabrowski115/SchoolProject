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
using WPF_LoginForm;

namespace WPF_LoginForm.View
{
    /// <summary>
    /// Logika interakcji dla klasy SongsView.xaml
    /// </summary>
    public partial class SongsView : UserControl
    {
        public SongsView()
        {
            InitializeComponent();
            InicjalizujListeUtworow();
        }




        private void InicjalizujListeUtworow()
    {
        // Stwórz przykładowe utwory
        var utwory = new List<Utwor>
        {
            new Utwor {ID="1.", Tytul = "Piosenka 1", Wykonawca = "Artysta 1", Album="Album", Image="/Images/Album50Cent.jpg" },
            new Utwor {ID="2.", Tytul = "Piosenka 2", Wykonawca = "Artysta 2" , Album="Album", Image="/Images/Album50Cent.jpg"},
            // Dodaj inne utwory
        };
            // Ustaw źródło danych dla ItemsControl
            itemsControl.ItemsSource = utwory;

        }
}
}

