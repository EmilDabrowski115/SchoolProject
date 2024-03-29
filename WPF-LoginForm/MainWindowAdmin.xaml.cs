﻿using System;
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
using static WPF_LoginForm.Data.DataAccess;

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
            DataContext = new BrowseView();

            Models.UserSettings userSettings = new Models.UserSettings();
            userSettings.Load();

            // Update the TextBlock with the username
            HelloTextBlock.Text = $"Hello: {userSettings.Username}";
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
                BrowseBtn.Content = "Przeglądaj";
                SettingsBtn.Content = "Ustwawienia";
                ActivityBtn.Content = "Sklep";
                SongsBtn.Content = "Utwory";
                AlbumsBtn.Content = "Albumy";
                ArtistsBtn.Content = "Artyści";
                MainBlock.Text = "Główne:";
                YourMusicBlock.Text = "Twoja muzyka:";
                SearchBox.Text = "Szukaj:";
            }
            else
            {
                BrowseBtn.Content = "Browse";
                SettingsBtn.Content = "Settings";
                ActivityBtn.Content = "Shop";
                SongsBtn.Content = "Songs";
                AlbumsBtn.Content = "Albums";
                ArtistsBtn.Content = "Artists";
                MainBlock.Text = "Main:";
                YourMusicBlock.Text = "Your Music:";
                SearchBox.Text = "Search:";
            }
        }


        private void JansyThemeClick(object sender, RoutedEventArgs e)
        {
            AppTheme.ChangeTheme(new Uri("/Themes/Jasny.xaml", UriKind.Relative));
        }
        private void CiemnyThemeClick(object sender, RoutedEventArgs e)
        {
            AppTheme.ChangeTheme(new Uri("/Themes/Ciemny.xaml", UriKind.Relative));
        }









        //Ważne Widoki NIE ZMIENIAC
        #region Widoki
        private void BrowseBtn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new BrowseView();
        }

        private void SettingsBtn_click(object sender, RoutedEventArgs e)
        {
            DataContext = new SettingsView();
        }

        private void ActivityBtn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ActivityModel();
        }
        private void SongsBtn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new SongsView();
        }

        private void AlbumsBtn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new AlbumsView();
        }

        private void Artists_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ArtistsView();
        }
        private void AdminBtn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new AdminSettingsView();
        }



        #endregion





    }
}
