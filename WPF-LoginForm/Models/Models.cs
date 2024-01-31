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

    public class UserSettings
    {
        public string Username { get; set; }

        public void Save()
        {
            Properties.Settings.Default.Username = Username;
            Properties.Settings.Default.Save();
        }

        public void Load()
        {
            Username = Properties.Settings.Default.Username;
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
