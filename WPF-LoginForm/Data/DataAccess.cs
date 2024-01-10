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

                        if (username == "Admin" && password == "Admin")
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





    }
}
