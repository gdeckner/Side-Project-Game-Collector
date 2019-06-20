using System;
using Game_Collector.Models;
using Game_Collector.DAL.Interfaces;


namespace Game_Collector.DAL
{
    public class UserLoginSqlDao : IUserLoginDAO
    {
        private string connectionString;
       
        public UserLoginSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public UserLogin ChangeLoginPassword(string username, string oldpassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfValid(string userName)
        {
            throw new NotImplementedException();
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
