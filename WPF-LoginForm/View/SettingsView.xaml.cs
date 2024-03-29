﻿using Microsoft.Win32;
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
using System.IO;
using WPFLocalizeExtension.Engine;
using System.Globalization;


namespace WPF_LoginForm.View
{
    /// <summary>
    /// Logika interakcji dla klasy SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {

        public SettingsView()
        {
            InitializeComponent();
        }
        private void RegulaminBtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\", "Regulamin.txt");

                if (File.Exists(filePath))
                {
                    // Wczytaj zawartość pliku
                    string regulaminContent = File.ReadAllText(filePath);

                    // Utwórz nowe okno, aby wyświetlić zawartość pliku
                    Window regulaminWindow = new Window
                    {
                        Title = "Regulamin",
                        Width = 500,
                        Height = 400,

                        Content = new TextBox
                        {
                            Text = regulaminContent,
                            IsReadOnly = true,
                            TextWrapping = TextWrapping.Wrap,
                            VerticalScrollBarVisibility = ScrollBarVisibility.Auto
                        }
                    };

                    // Pokaż nowe okno
                    regulaminWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Plik regulaminu nie istnieje.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Sprawdź, czy coś zostało wybrane
            if (e.AddedItems.Count > 0)
            {
                // Pobierz indeks wybranej pozycji w ComboBox
                int selectedIndex = (sender as ComboBox).SelectedIndex;

                // Ustaw nową kulturę na podstawie wybranego indeksu
                switch (selectedIndex)
                {
                    case 0: // English
                        LocalizeDictionary.Instance.Culture = new CultureInfo("en-US");
                        break;
                    case 1: // Polish
                        LocalizeDictionary.Instance.Culture = new CultureInfo("pl-PL");
                        break;
                        // Dodaj więcej przypadków dla innych języków, jeśli są obsługiwane
                }


            }
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            // Sprawdź, czy coś zostało wybrane
            if (e.AddedItems.Count > 0)
            {
                
                int selectedIndex = (sender as ComboBox).SelectedIndex;

                
                switch (selectedIndex)
                {
                    case 0:
                        AppTheme.ChangeTheme(new Uri("/Themes/Jasny.xaml", UriKind.Relative));
                        break;
                    case 1:
                        AppTheme.ChangeTheme(new Uri("/Themes/Ciemny.xaml", UriKind.Relative));
                        break;
                        
                }


            }
        }
    }
}