using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
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
using WPF_LoginForm.Viewmodel;

namespace WPF_LoginForm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowAdmin : Window
    {
        public MainWindowAdmin()
        {
            InitializeComponent();
        }
        private void CloseBtn(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void SearchGotFocus(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = "";
        }
        private void SearchLostFocus(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = "Szukaj: ";
        }
        private void MinimalizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void WindowReSize(object sender, RoutedEventArgs e)
        {
            click_Count++;
            if (click_Count % 2 == 1)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }
        private int click_Count = 0;
        private void LanguaeBtn_Click(object sender, RoutedEventArgs e)
        {
            click_Count++;
            if (click_Count % 2 == 1)
            {
                BrowceBtn.Content = "Przeglądaj";
                SettingsBtn.Content = "Ustwawienia";
                ActivityBtn.Content = "Aktywność";
                SongsBtn.Content = "Utwory";
                AlbumsBtn.Content = "Albumy";
                ArtistsBtn.Content = "Artyści";
                MainBlock.Text = "Główne:";
                YourMusicBlock.Text = "Twoja muzyka:";
                SearchBox.Text = "Szukaj:";
            }
            else
            {
                BrowceBtn.Content = "Browse";
                SettingsBtn.Content = "Settings";
                ActivityBtn.Content = "Activity";
                SongsBtn.Content = "Songs";
                AlbumsBtn.Content = "Albums";
                ArtistsBtn.Content = "Artists";
                MainBlock.Text = "Main:";
                YourMusicBlock.Text = "Your Music:";
                SearchBox.Text = "Search:";
            }
        }


        
        





        //Ważne Widoki NIE ZMIENIAC
        #region Widoki
        private void BrowceBtn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new BrowseViewModel();
        }

        private void SettingsBtn_click(object sender, RoutedEventArgs e)
        {
            DataContext = new SettingsViewModel();
        }

        private void ActivityBtn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ActivityViewModel();
        }
        private void SongsBtn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new SongsViewModel();
        }

        private void AlbumsBtn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new AlbumsViewModel();
        }

        private void Artists_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ArtistsViewModel();
        }


        #endregion

       
    }
}
