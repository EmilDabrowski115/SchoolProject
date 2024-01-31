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
using WPF_LoginForm.View;

namespace WPF_LoginForm
{
    /// <summary>
    /// Logika interakcji dla klasy SongsItemControl.xaml
    /// </summary>
    public partial class SongsItemControl : UserControl
    {
        public SongsItemControl()
        {
            InitializeComponent();
            
        }

      

        private void polubienieClick(object sender, RoutedEventArgs e)
        {
            // Pobierz DataContext (czyli obiekt Utwor przypisany do tej kontrolki)
            Utwor utwor = (Utwor)DataContext;

            if (utwor != null)
            {
                // Zmień wartość właściwości Lubiany na przeciwną
                utwor.Lubiany = !utwor.Lubiany;

                // Aktualizuj wygląd przycisku w zależności od wartości Lubiany
                if (utwor.Lubiany)
                {
                    polubienie.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#cc3434"));
                    polubienie.Foreground = Brushes.White;
                }
                else
                {
                    polubienie.Background = Brushes.White;
                    polubienie.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#cc3434"));
                }
            }
        }
    }

}






