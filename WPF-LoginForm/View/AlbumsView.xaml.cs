﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logika interakcji dla klasy AlbumsView.xaml
    /// </summary>
    public partial class AlbumsView : UserControl
    {
        public AlbumsView()
        {
            InitializeComponent();
            InicjalizujListeUtworow();
        }
        private void InicjalizujListeUtworow()
        {
            // Uzyskaj dostęp do listy utworów z klasy Utwor
            List<Utwor> listaUtworow = Utwor.ListaUtworow();

            // Ustaw źródło danych dla ItemsControl na listę utworów
            itemsControl.ItemsSource = listaUtworow;
        }


    }
}
   



       
        
    





