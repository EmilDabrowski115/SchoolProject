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


namespace WPF_LoginForm.Data
{
    public class DataAccess
    {
        private static string connectionString = $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")}\\BazaDanych.sqlite;Version=3;";

        public bool RegisterUser(UserModel user)
        {
            try
            {
                MessageBox.Show($"Current Directory: {AppDomain.CurrentDomain.BaseDirectory}");

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

                    // Insert new user
                    string insertQuery = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";
                    int rowsAffected = connection.Execute(insertQuery, new { Username = user.Username, Password = user.Password });

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

                    // Check if the username and password match
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                    int matchingUsersCount = connection.ExecuteScalar<int>(checkQuery, new { Username = username, Password = password });

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











    }
}
