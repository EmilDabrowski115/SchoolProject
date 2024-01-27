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
using System.IO;



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
                string filePath = @"C:\Users\Asus\Source\Repos\EmilDabrowski115\SchoolProject\WPF-LoginForm\Regulamin.txt";

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




    }
    }


