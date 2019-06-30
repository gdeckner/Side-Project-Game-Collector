using System;
using Game_Collector.DAL;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game_Collector.Security;
using System.Security;
using Game_Collector.Models;

namespace GameDataBase.test.DAL
{
    [TestClass]
    public class UserLoginTests : DatabaseTest
    {
        private UserLoginSqlDao dao;
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            PasswordHasher hash = new PasswordHasher();
            dao = new UserLoginSqlDao(ConnectionString,new PasswordHasher());
            string salt = Convert.ToBase64String(hash.GenerateRandomSalt());
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"insert into UserInfo (userName,password,salt) values ('testUser',@password,@salt)";
                cmd.Parameters.AddWithValue("@salt", "RrQlUO2CbmowsGDSpRhXZA==");
                cmd.Parameters.AddWithValue("@password", "RrQlUO2CbmowsGDSpRhXZPGjRy1BEXkN3fdCrNs4xUJjxNcs");
                cmd.ExecuteNonQuery();
            }
        }
        [TestMethod]
        public void CheckifNameValidTest()
        {
            Assert.AreEqual(true, dao.CheckIfUserNameExists("testUser"));
            Assert.AreEqual(true, dao.CheckIfUserNameExists("testuser"));
            Assert.AreEqual(false, dao.CheckIfUserNameExists("ooga"));
        }
        [TestMethod]
        public void CreateLoginTest()
        {
            dao.CreateLogin("newUser","password");
            Assert.AreEqual(true, dao.CheckLogin("newUser", "password"));
            Assert.AreEqual(false, dao.CheckLogin("newuser", "Password"));
        }
        [TestMethod]
        public void CheckLoginTest()
        {
            
            Assert.AreEqual(true, dao.CheckLogin("testUser","Password"));
            Assert.AreEqual(false, dao.CheckLogin("testuser", "password"));
            Assert.AreEqual(true, dao.CheckLogin("testuser", "Password"));

        }
        [TestMethod]
        public void ChangeLoginPasswordTest()
        {
            dao.ChangeLoginPassword("testuser", "NewPassword");
            Assert.AreEqual(true, dao.CheckLogin("testuser", "NewPassword"));
            Assert.AreEqual(false, dao.CheckLogin("testuser", "password"));
        }
    


    }

}
