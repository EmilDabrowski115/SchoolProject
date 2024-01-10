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
        new Utwor {ID="3.", Tytul = "Bohemian Rhapsody", Wykonawca = "Queen", Album="A Night at the Opera", Image="/Images/Album50Cent.jpg", Lubiany=false},
        new Utwor {ID="4.", Tytul = "Like a Rolling Stone", Wykonawca = "Bob Dylan", Album="Highway 61 Revisited", Image="/Images/Album50Cent.jpg", Lubiany=false},
        new Utwor {ID="5.", Tytul = "Imagine", Wykonawca = "John Lennon", Album="Imagine", Image="/Images/Album50Cent.jpg", Lubiany=false},
        new Utwor {ID="6.", Tytul = "Thriller", Wykonawca = "Michael Jackson", Album="Thriller", Image="/Images/Album50Cent.jpg", Lubiany=false},
        new Utwor {ID="7.", Tytul = "Hotel California", Wykonawca = "Eagles", Album="Hotel California", Image="/Images/Album50Cent.jpg", Lubiany=false},
        new Utwor {ID="8.", Tytul = "Stairway to Heaven", Wykonawca = "Led Zeppelin", Album="Led Zeppelin IV", Image="/Images/Album50Cent.jpg", Lubiany=false},
        new Utwor {ID="9.", Tytul = "Billie Jean", Wykonawca = "Michael Jackson", Album="Thriller", Image="/Images/Album50Cent.jpg", Lubiany=false},
        new Utwor {ID="10.", Tytul = "Boogie Wonderland", Wykonawca = "Earth, Wind & Fire", Album="I Am", Image="/Images/Album50Cent.jpg", Lubiany=false},
        new Utwor {ID="11.", Tytul = "Rocket Man", Wykonawca = "Elton John", Album="Honky Château", Image="/Images/Album50Cent.jpg", Lubiany=false},
        new Utwor {ID="12.", Tytul = "Sweet Child o' Mine", Wykonawca = "Guns N' Roses", Album="Appetite for Destruction", Image="/Images/Album50Cent.jpg", Lubiany=false},
        new Utwor {ID="13.", Tytul = "Purple Haze", Wykonawca = "Jimi Hendrix", Album="Are You Experienced", Image="/Images/Album50Cent.jpg", Lubiany=false},
        new Utwor {ID="14.", Tytul = "Superstition", Wykonawca = "Stevie Wonder", Album="Talking Book", Image="/Images/Album50Cent.jpg", Lubiany=false},
        new Utwor {ID="15.", Tytul = "The Sound of Silence", Wykonawca = "Simon & Garfunkel", Album="Sounds of Silence", Image="/Images/Album50Cent.jpg", Lubiany=false},
        new Utwor {ID="16.", Tytul = "Imagine", Wykonawca = "John Lennon", Album="Imagine", Image="/Images/Album50Cent.jpg", Lubiany=false},
        new Utwor {ID="17.", Tytul = "Black", Wykonawca = "Pearl Jam", Album="Ten", Image="/Images/Album50Cent.jpg", Lubiany=false},
        new Utwor {ID="18.", Tytul = "Sweet Caroline", Wykonawca = "Neil Diamond", Album="Sweet Caroline", Image="/Images/Album50Cent.jpg", Lubiany=false},
        new Utwor {ID="19.", Tytul = "Wish You Were Here", Wykonawca = "Pink Floyd", Album="Wish You Were Here", Image="/Images/Album50Cent.jpg", Lubiany=false},
        new Utwor {ID="20.", Tytul = "What's Going On", Wykonawca = "Marvin Gaye", Album="What's Going On", Image="/Images/Album50Cent.jpg", Lubiany=false},
            };
        }

    




    }
}
