using System;
using Game_Collector.Models;
using Game_Collector.DAL.Interfaces;
using System.Data.SqlClient;

namespace Game_Collector.DAL
{
    public class UserLoginSqlDao : IUserLoginDAO
    {
        private string connectionString;
       
        public UserLoginSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public void ChangeLoginPassword(string username, string oldpassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfUserNameExists(string userName)
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

        public void CreateLogin(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
