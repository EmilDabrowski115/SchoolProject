using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static WPF_LoginForm.Data.DataAccess;
using WPF_LoginForm.Data;
using System.IO;
using System.Windows.Media.Imaging;

namespace WPF_LoginForm
{
    public class Utwor
    {
        public int ID { get; set; }
        public string Tytul { get; set; }
        public string Wykonawca { get; set; }
        public string Album { get; set; }
        public BitmapImage Image { get; set; }
        public bool Lubiany { get; set; }

        // Dodaj inne właściwości utworu, jeśli są potrzebne


        public static List<Utwor> ListaUtworow()
        {
            DataAccess dataAccess = new DataAccess();
            List<Models.MusicRecord> musicRecords = dataAccess.GetAllMusicRecords();

            // Convert MusicRecord objects to Utwor objects
            List<Utwor> utwory = musicRecords.Select(record => new Utwor
            {
                ID = record.Id,
                Tytul = record.NazwaUtworu,
                Wykonawca = record.Wykonawca,
                Album = record.Album,
                Image = ConvertByteArrayToBitmapImage(record.ZDJ),  // Convert byte array to BitmapImage
                Lubiany = false
            }).ToList();

            return utwory;
        }

        private static BitmapImage ConvertByteArrayToBitmapImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return null;

            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                stream.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = stream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }

            return bitmapImage;
        }







    }
}
