using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF_LoginForm
{
    public class Utwor
    {
        public string ID { get; set; }
        public string Tytul { get; set; }
        public string Wykonawca { get; set; }
        public string Album { get; set; }
        public string Image { get; set; }
        public bool Lubiany { get; set; }

        // Dodaj inne właściwości utworu, jeśli są potrzebne


        public static List<Utwor> ListaUtworow()
        {
            return new List<Utwor>
            {
                new Utwor {ID="1.", Tytul = "Mamma Mia", Wykonawca = "Abba", Album="Jakis", Image="/Images/Album50Cent.jpg", Lubiany=false},
                new Utwor {ID="2.", Tytul = "Piosenka 2", Wykonawca = "Artysta 2" , Album="Album", Image="/Images/Album50Cent.jpg", Lubiany=false },
                // Dodaj inne utwory
            };
        }

    




    }
}
