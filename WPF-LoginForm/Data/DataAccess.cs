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








    }
}
