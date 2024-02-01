using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Dapper;
using System.Windows;
using WPF_LoginForm.Models;
using System.IO;
using System.Security.Cryptography;
using System.Security.AccessControl;
using Newtonsoft.Json;

namespace WPF_LoginForm.Data
{
    public class DataAccess
    {
        private static string connectionString = $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")}\\BazaDanych.sqlite;Version=3;";


        public event Action<string> UserAuthenticated;

        private void OnUserAuthenticated(string username)
        {
            UserAuthenticated?.Invoke(username);
        }


        public bool DeduceCredits(string username, int creditsToDeduce)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Check if the username exists
                    string checkQuery = "SELECT * FROM User_Data WHERE Username = @Username";
                    var userData = connection.QueryFirstOrDefault<UserSettings>(checkQuery, new { Username = username });

                    if (userData != null)
                    {
                        // User found, update credits
                        userData.Credits -= creditsToDeduce;

                        // Update the User_Data table with the new credits value
                        string updateQuery = "UPDATE User_Data SET Credits = @Credits WHERE Username = @Username";
                        int rowsAffected = connection.Execute(updateQuery, new { Credits = userData.Credits, Username = username });

                        if (rowsAffected > 0)
                        {
                            // Update successful
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Failed to update credits in the database.");
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("User not found.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }


        public UserSettings RetrieveUserData(string username)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Check if the username exists
                    string checkQuery = "SELECT * FROM User_Data WHERE Username = @Username";
                    var userData = connection.QueryFirstOrDefault<UserSettings>(checkQuery, new { Username = username });

                    if (userData != null)
                    {
                        // User found, return the data
                        return userData;
                    }
                    else
                    {
                        MessageBox.Show("User not found.");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
        }

        public void UpdateUserPurchasedSongs(string username, string newPurchasedSongsJson)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Check if the user already exists
                    string checkQuery = "SELECT * FROM User_Data WHERE Username = @Username";
                    var userData = connection.QueryFirstOrDefault<UserSettings>(checkQuery, new { Username = username });

                    if (userData != null)
                    {
                        // Check if the existing purchased songs is null
                        if (userData.Purchased == null)
                        {
                            // Directly update the Purchased column with the new JSON
                            string updateQuery = "UPDATE User_Data SET Purchased = @Purchased WHERE Username = @Username";
                            int rowsAffected = connection.Execute(updateQuery, new { Username = username, Purchased = newPurchasedSongsJson });

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Purchased songs updated successfully!");
                            }
                            else
                            {
                                MessageBox.Show("Failed to update purchased songs. Please try again.");
                            }
                        }
                        else
                        {
                            // User found, merge the new and existing purchased songs
                            List<Utwor> existingPurchasedSongs = JsonConvert.DeserializeObject<List<Utwor>>(userData.Purchased);
                            List<Utwor> newPurchasedSongs = JsonConvert.DeserializeObject<List<Utwor>>(newPurchasedSongsJson);

                            List<Utwor> mergedPurchasedSongs = existingPurchasedSongs.Concat(newPurchasedSongs).ToList();

                            // Serialize the merged purchased songs
                            string jsonMergedPurchasedSongs = JsonConvert.SerializeObject(mergedPurchasedSongs);

                            // Update the User_Data table with the merged purchased songs
                            string updateQuery = "UPDATE User_Data SET Purchased = @Purchased WHERE Username = @Username";
                            int rowsAffected = connection.Execute(updateQuery, new { Username = username, Purchased = jsonMergedPurchasedSongs });

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Purchased songs updated successfully!");
                            }
                            else
                            {
                                MessageBox.Show("Failed to update purchased songs. Please try again.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("User not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }





        public bool RegisterUser(UserModel user)
        {
            try
            {

                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Check if the username already exists
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                    int existingUsersCount = connection.ExecuteScalar<int>(checkQuery, new { Username = user.Username });

                    if (existingUsersCount > 0)
                    {
                        MessageBox.Show("Username already exists. Please choose a different one.");
                        return false;
                    }

                    string userpassword = user.Password;
                    string HashedPassword = DoubleSHA1Hash(userpassword);
                    // Insert new user
                    string insertQuery = "INSERT INTO Users (Username, Password, IsAdmin) VALUES (@Username, @Password, 0)";
                    int rowsAffected = connection.Execute(insertQuery, new { Username = user.Username, Password = HashedPassword });

                    string insertQuery2 = "INSERT INTO User_Data (Username, Credits) VALUES (@Username, 100)";
                    int rowsAffected2 = connection.Execute(insertQuery2, new { Username = user.Username});

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Registration successful!");
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Registration failed. Please try again.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }

        public Tuple<bool, bool> AuthenticateUser(string username, string password)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string hashedpassword = DoubleSHA1Hash(password);
                    // Check if the username and password match
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                    int matchingUsersCount = connection.ExecuteScalar<int>(checkQuery, new { Username = username, Password = hashedpassword });

                    if (matchingUsersCount > 0)
                    {
                        // Check if the user is an admin based on the IsAdmin column
                        string isAdminQuery = "SELECT IsAdmin FROM Users WHERE Username = @Username";
                        int isAdmin = connection.ExecuteScalar<int>(isAdminQuery, new { Username = username });

                        if (isAdmin == 1)
                        {

                            return new Tuple<bool, bool>(true, true);
                        }
                        else 
                        {

                            return new Tuple<bool, bool>(true, false);

                        }


                    }
                    else
                    {
                        // Return a tuple with two bools indicating login failure
                        return new Tuple<bool, bool>(false, false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

                // Return a tuple with two bools indicating login failure
                return new Tuple<bool, bool>(false, false);
            }
        }



        public Tuple<bool, bool> AddAdmin(string username)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Check if the username and password match
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                    int matchingUsersCount = connection.ExecuteScalar<int>(checkQuery, new { Username = username });

                    if (matchingUsersCount > 0)
                    {
                        // Check if the user is an admin based on the IsAdmin column
                        string isAdminQuery = "SELECT IsAdmin FROM Users WHERE Username = @Username";
                        int isAdmin = connection.ExecuteScalar<int>(isAdminQuery, new { Username = username });

                        if (isAdmin == 1)
                        {
                            return new Tuple<bool, bool>(true, false);
                        }
                        else
                        {
                            // Update IsAdmin to 1 for the specified username
                            string updateQuery = "UPDATE Users SET IsAdmin = 1 WHERE Username = @Username";
                            connection.Execute(updateQuery, new { Username = username });

                            // Return a tuple indicating successful admin addition
                            return new Tuple<bool, bool>(true, true);
                        }


                    }
                    else
                    {
                        // Return a tuple with two bools indicating login failure
                        return new Tuple<bool, bool>(false, false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

                // Return a tuple with two bools indicating login failure
                return new Tuple<bool, bool>(false, false);
            }
        }

        static string DoubleSHA1Hash(string input)
        {
            // First SHA-1 hash
            byte[] firstHashBytes = SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
            string firstHash = BitConverter.ToString(firstHashBytes).Replace("-", "").ToLower();

            // Second SHA-1 hash
            byte[] secondHashBytes = SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(firstHash));
            string secondHash = BitConverter.ToString(secondHashBytes).Replace("-", "").ToLower();

            return secondHash;
        }


        public Tuple<bool, bool> RemoveAdmin(string username)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Check if the username and password match
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                    int matchingUsersCount = connection.ExecuteScalar<int>(checkQuery, new { Username = username });

                    if (matchingUsersCount > 0)
                    {
                        // Check if the user is an admin based on the IsAdmin column
                        string isAdminQuery = "SELECT IsAdmin FROM Users WHERE Username = @Username";
                        int isAdmin = connection.ExecuteScalar<int>(isAdminQuery, new { Username = username });

                        if (isAdmin == 1)
                        {
                            string updateQuery = "UPDATE Users SET IsAdmin = 0 WHERE Username = @Username";
                            connection.Execute(updateQuery, new { Username = username });
                            return new Tuple<bool, bool>(true, true);
                        }
                        else
                        {
                            // Return a tuple indicating successful admin addition
                            return new Tuple<bool, bool>(true, false);
                        }


                    }
                    else
                    {
                        // Return a tuple with two bools indicating login failure
                        return new Tuple<bool, bool>(false, false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

                // Return a tuple with two bools indicating login failure
                return new Tuple<bool, bool>(false, false);
            }
        }

        public StudioInfo GetStudioInfo()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Retrieve studio information from the database
                    string query = "SELECT * FROM StudioInfo WHERE id=1";
                    StudioInfo studioInfo = connection.QueryFirstOrDefault<StudioInfo>(query);

                    return studioInfo;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
        }

        public StudioInfo UpdateStudioName(string newStudioName)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Update studio name in the database
                    string updateQuery = "UPDATE StudioInfo SET NazwaStudia = @NewStudioName WHERE id=1";
                    int rowsAffected = connection.Execute(updateQuery, new { NewStudioName = newStudioName });

                    // Check if the update was successful
                    if (rowsAffected > 0)
                    {
                        // Retrieve updated studio information from the database
                        string selectQuery = "SELECT * FROM StudioInfo WHERE id=1";
                        StudioInfo updatedStudioInfo = connection.QueryFirstOrDefault<StudioInfo>(selectQuery);
                        return updatedStudioInfo;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return null;
        }


        public StudioInfo UpdateStudioLogo(byte[] newLogo)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Update studio logo in the database
                    string updateQuery = "UPDATE StudioInfo SET LogoStudia = @NewLogo WHERE id=1";
                    int rowsAffected = connection.Execute(updateQuery, new { NewLogo = newLogo });

                    // Check if the update was successful
                    if (rowsAffected > 0)
                    {
                        // Retrieve updated studio information from the database
                        string selectQuery = "SELECT * FROM StudioInfo WHERE id=1";
                        StudioInfo updatedStudioInfo = connection.QueryFirstOrDefault<StudioInfo>(selectQuery);
                        return updatedStudioInfo;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return null;
        }




        public bool AddMusicRecord(MusicRecord musicRecord)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Sprawdź, czy utwór o danej nazwie, albumie, wykonawcy i zdj już istnieje w bazie danych
                    string checkQuery = "SELECT COUNT(*) FROM BazaUtworow WHERE NazwaUtworu = @NazwaUtworu AND Album = @Album AND Wykonawca = @Wykonawca AND ZDJ = @ZDJ";
                                        

                    int existingRecordsCount = connection.ExecuteScalar<int>(checkQuery, musicRecord);

                    if (existingRecordsCount > 0)
                    {
                        // Utwór o podanych danych już istnieje w bazie danych
                        return false;
                    }

                    // Dodaj nowy utwór do bazy danych
                    string insertQuery = "INSERT INTO BazaUtworow (NazwaUtworu, Album, Wykonawca, ZDJ) " +
                                         "VALUES (@NazwaUtworu, @Album, @Wykonawca, @ZDJ)";

                    int rowsAffected = connection.Execute(insertQuery, musicRecord);

                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd: " + ex.Message);
                return false;
            }
        }

        public bool RemoveMusicRecord(string nazwaUtworu)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Sprawdź, czy utwór o danej nazwie istnieje w bazie danych
                    string checkQuery = "SELECT COUNT(*) FROM BazaUtworow WHERE NazwaUtworu = @NazwaUtworu";

                    int existingRecordsCount = connection.ExecuteScalar<int>(checkQuery, new { NazwaUtworu = nazwaUtworu });

                    if (existingRecordsCount == 0)
                    {
                        // Utwór o podanej nazwie nie istnieje w bazie danych
                        return false;
                    }

                    // Usuń utwór o podanej nazwie z bazy danych
                    string deleteQuery = "DELETE FROM BazaUtworow WHERE NazwaUtworu = @NazwaUtworu";

                    int rowsAffected = connection.Execute(deleteQuery, new { NazwaUtworu = nazwaUtworu });

                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd: " + ex.Message);
                return false;
            }
        }

        public List<MusicRecord> GetAllMusicRecords()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Pobierz wszystkie utwory z bazy danych
                    string query = "SELECT * FROM BazaUtworow";
                    List<MusicRecord> musicRecords = connection.Query<MusicRecord>(query).ToList();

                    return musicRecords;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd: " + ex.Message);
                return null;
            }
        }




        








    }
}
