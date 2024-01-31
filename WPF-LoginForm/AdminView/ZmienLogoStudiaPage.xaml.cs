using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using WPF_LoginForm.Data;
using WPF_LoginForm.Models;

namespace WPF_LoginForm.AdminView
{
    /// <summary>
    /// Logika interakcji dla klasy ZmienLogoStudiaPage.xaml
    /// </summary>
    public partial class ZmienLogoStudiaPage : UserControl
    {

        Models.StudioInfo studioInfoModel;  // Declare at class level

        public ZmienLogoStudiaPage()
        {
            InitializeComponent();
            GetStudioInfo();
        }

        private void GetStudioInfo()
        {
            DataAccess dataAccess = new DataAccess();
            StudioInfo studioInfo = dataAccess.GetStudioInfo();

            if (studioInfo != null)
            {
                studioInfoModel = new Models.StudioInfo
                {
                    Id = studioInfo.Id,
                    NazwaStudia = studioInfo.NazwaStudia,
                    LogoStudia = studioInfo.LogoStudia
                };

                StudioLogoImage.Source = ConvertByteArrayToBitmapImage(studioInfoModel.LogoStudia);


            }
        }

        private BitmapImage ConvertByteArrayToBitmapImage(byte[] byteArray)
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

        private byte[] ConvertImageToByteArray(string imagePath)
        {
            // Your code to convert the image file to a byte array.
            // You can use ImageMagick, System.Drawing, or other libraries for this.
            // Example using System.Drawing:
            using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                BinaryReader br = new BinaryReader(fs);
                return br.ReadBytes((int)fs.Length);
            }
        }


        private void ChangeLogoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All Files (*.*)|*.*",
                Title = "Select a Logo Image",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = openFileDialog.FileName;

                // Now you can use the selectedImagePath to update the image or save it to your database.
                UpdateImage(selectedImagePath);
            }
        }


        private void UpdateImage(string imagePath)
        {
            // Your code to update the image in the database or wherever needed.
            // You might want to convert the image file to a byte array before saving to the database.
            byte[] newLogo = ConvertImageToByteArray(imagePath);
            DataAccess dataAccess = new DataAccess();

            StudioInfo studioInfo = dataAccess.UpdateStudioLogo(newLogo);

            // Update the StudioInfoModel and UI accordingly.
            if (studioInfo != null)
            {
                studioInfoModel = new Models.StudioInfo
                {
                    Id = studioInfo.Id,
                    NazwaStudia = studioInfo.NazwaStudia,
                    LogoStudia = studioInfo.LogoStudia
                };
                StudioLogoImage.Source = ConvertByteArrayToBitmapImage(studioInfoModel.LogoStudia);
                MessageBox.Show("Successfully changed the Logo of the studio");


            }
            else
            {
                MessageBox.Show("Failed to Update Studio Name");



            }
        }

    }
}
