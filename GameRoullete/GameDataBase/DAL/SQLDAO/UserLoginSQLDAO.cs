using System;
using Game_Collector.Models;
using Game_Collector.DAL.Interfaces;
using System.Data.SqlClient;
using Game_Collector.Security;

namespace Game_Collector.DAL
{
    public class UserLoginSqlDao : IUserLoginDAO
    {
        private string connectionString;
        private readonly IPasswordHasher passHasher;
       
        public UserLoginSqlDao(string dbConnectionString,IPasswordHasher passwordHasher)
        {
            connectionString = dbConnectionString;
            passHasher = passwordHasher;
        }
        public void ChangeLoginPassword(string username, string newPassword)
        {
            //Changes the password for the user
            byte[] salt = passHasher.GenerateRandomSalt();
            string hashedPassword = passHasher.ComputeHash(newPassword, salt);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"update UserInfo
                set password = @hashedPassword,
                salt = @salt
                where userName = @userName";
                cmd.Parameters.AddWithValue("@userName", username);
                cmd.Parameters.AddWithValue("@salt", Convert.ToBase64String(salt));
                cmd.Parameters.AddWithValue("@hashedPassword", hashedPassword);
                cmd.ExecuteNonQuery();
            }
        }

        public bool CheckIfUserNameExists(string userName)
        {
            //Verifies if user name already exists, not case sensitive
            bool isValid = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"select userName from UserInfo
                where userName = @username";
                cmd.Parameters.AddWithValue("@username", userName.ToLower());
                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    isValid = true;
                }
            }
            return isValid;
        }

        public bool CheckLogin(string userName, string password)
        {
            //Verifies that login info is correct
            bool loginPassed = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"select password, salt from UserInfo
                where userName = @userName";
                cmd.Parameters.AddWithValue("@userName", userName);
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    string pulledPassword = (string)reader["password"];
                    string pulledSalt = (string)reader["salt"];
                    string hash = passHasher.ComputeHash(password, Convert.FromBase64String(pulledSalt));
                    loginPassed = hash.Equals(pulledPassword);
                }
            }
            return loginPassed;
        }

        public void CreateLogin(string userName, string password)
        {
            //Creates a new login
            byte[] salt = passHasher.GenerateRandomSalt();
            string hashedPassword = passHasher.ComputeHash(password, salt);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"insert into UserInfo(userName,password,salt)
                values(@userName,@hashedPassword,@salt)";
                cmd.Parameters.AddWithValue("@userName", userName);
                cmd.Parameters.AddWithValue("@hashedPassword", hashedPassword);
                cmd.Parameters.AddWithValue("@salt", Convert.ToBase64String(salt));
                cmd.ExecuteNonQuery();
            }
        }
    }
}
