using System;
using System.Windows.Media;
using System.Windows.Controls;
using WPF_LoginForm.Models;
using System.Collections.Generic;
using System.Windows;

namespace WPF_LoginForm
{
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

                    // Add the liked song to the manager
                    LikedSongsManager.AddLikedSong(utwor);
                }
                else
                {
                    polubienie.Background = Brushes.White;
                    polubienie.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#cc3434"));

                    // Remove the unliked song from the manager
                    LikedSongsManager.RemoveLikedSong(utwor);
                }
            }
        }
    }

    public static class LikedSongsManager
    {
        private static List<Utwor> likedSongs = new List<Utwor>();

        public static List<Utwor> GetLikedSongs()
        {
            return likedSongs;
        }

        public static void AddLikedSong(Utwor song)
        {
            if (!likedSongs.Contains(song))
            {
                likedSongs.Add(song);
            }
        }

        public static void RemoveLikedSong(Utwor song)
        {
            likedSongs.Remove(song);
        }

        public static void ClearLikedSongs()
        {
            likedSongs.Clear();
        }
    }
}
