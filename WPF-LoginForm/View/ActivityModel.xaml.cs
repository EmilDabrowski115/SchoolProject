using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_LoginForm.Data;
using WPF_LoginForm.Models;
using Newtonsoft.Json;


namespace WPF_LoginForm.View
{
    /// <summary>
    /// Logika interakcji dla klasy ActivityModel.xaml
    /// </summary>
    public partial class ActivityModel : UserControl
    {
        public ActivityModel()
        {
            InitializeComponent();
            // Load and display liked songs when the store page is loaded
            DisplayLikedSongs();
            DisplayCredits();

        }

        private void DisplayCredits()
        {
            UserSettings userSettings = new UserSettings();
            userSettings.Load(); // Load the settings

            // Assuming txtCredits is the TextBox to display the credits
            CreditsText.Text = $"Credits: {userSettings.Credits}";
        }


        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the list of liked songs from the manager
            List<Utwor> likedSongs = LikedSongsManager.GetLikedSongs();

            // Calculate the total cost based on the number of liked songs (for demonstration purposes, assuming each song costs 10 credits)
            int totalCost = likedSongs.Count * 10;

            // Check if the user has enough credits
            if (CheckSufficientCredits(totalCost))
            {
                // Deduct the credits from the user (for demonstration purposes, deducting the total cost)
                DeductCredits(totalCost);

                // Add the liked songs to the user's purchased list in the database
                AddToPurchasedList(likedSongs);

                // Remove the liked songs from the manager
                LikedSongsManager.ClearLikedSongs();

                DisplayLikedSongs();
                DisplayCredits();
                // Display a success message or perform any other necessary actions
                MessageBox.Show($"Purchase successful! Deducted {totalCost} credits.");
            }
            else
            {
                // Display an error message or perform any other necessary actions
                MessageBox.Show("Insufficient credits. Please add more credits.");
            }
        }

        private bool CheckSufficientCredits(int totalCost)
        {
            UserSettings userSettings = new UserSettings();
            userSettings.Load(); // Load the settings

            int UserCredits = userSettings.Credits;

            // Check if the user has enough credits
            return UserCredits >= totalCost;
        }

        private void DeductCredits(int totalCost)
        {
            UserSettings userSettings = new UserSettings();
            userSettings.Load(); // Load the settings

            string Username = userSettings.Username;
            int Credits = userSettings.Credits;

            DataAccess dataAccess = new DataAccess();
            bool check = dataAccess.DeduceCredits(Username, totalCost);

            if (check)
            {
                int newcredVal = Credits - totalCost;
                userSettings.Credits = newcredVal;
                userSettings.Save();

            }





        }

        private void AddToPurchasedList(List<Utwor> purchasedSongs)
        {
            try
            {

                UserSettings userSettings = new UserSettings();
                userSettings.Load(); // Load the settings

                string Username = userSettings.Username;
                // Convert the list to a JSON string
                string jsonPurchasedSongs = JsonConvert.SerializeObject(purchasedSongs);

                DataAccess dataAccess = new DataAccess();

                dataAccess.UpdateUserPurchasedSongs(Username, jsonPurchasedSongs);
                // For demonstration purposes, you might want to update the user's purchased songs in the database
                // In a real application, you would perform database operations here
                // For example:
                // UpdateUserPurchasedSongs(username, jsonPurchasedSongs);

                // Display a success message or perform other actions
                MessageBox.Show("Songs added to the purchased list successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void DisplayLikedSongs()
        {
            // Get the list of liked songs from the manager
            List<Utwor> likedSongs = LikedSongsManager.GetLikedSongs();

            likedSongsListBox.ItemsSource = likedSongs;


            // Assuming you have a control to display the liked songs (e.g., ListBox or DataGrid)
            // You need to bind the liked songs to the control's ItemsSource property for display
            // For example, if you have a ListBox named likedSongsListBox:

            // likedSongsListBox.ItemsSource = likedSongs;

            // Now you can display the liked songs in your store page
        }
    }
}
