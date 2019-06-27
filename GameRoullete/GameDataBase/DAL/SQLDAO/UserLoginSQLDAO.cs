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
        public UserLogin ChangeLoginPassword(string username, string oldpassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfValid(string userName)
        {
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
            throw new NotImplementedException();
        }

        public bool CheckPasswordValid(string password1, string password2)
        {
            throw new NotImplementedException();
        }

        public UserLogin CreateLogin(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
