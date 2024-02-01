using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_LoginForm.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public bool IsLiked { get; set; }  // Nowe pole
    }


    public class UserSettings
    {
        public string Username { get; set; }
        public int Credits { get; set; }
        public string Purchased { get; set; }


        public void Save()
        {
            Properties.Settings.Default.Username = Username;
            Properties.Settings.Default.Purchased = Purchased;
            Properties.Settings.Default.Credits = Credits;
            Properties.Settings.Default.Save();
        }

        public void Load()
        {
            Username = Properties.Settings.Default.Username;
            Purchased = Properties.Settings.Default.Purchased;
            Credits = Properties.Settings.Default.Credits;

        }
    }

    public class StudioInfo
    { 
        public int Id { get; set; } 
        public string NazwaStudia { get; set; }
        public byte[] LogoStudia { get; set; }
    }

    


    public class MusicRecord
    {
        public int Id { get; set; }
        public string NazwaUtworu { get; set; }
        public string Album { get; set; }
        public string Wykonawca { get; set; }
        public byte[] ZDJ { get; set; }

    }
}
