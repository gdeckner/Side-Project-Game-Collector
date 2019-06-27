using System;
using Game_Collector.DAL;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            dao = new UserLoginSqlDao(ConnectionString);
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"insert into UserInfo (userName,password,salt) values ('testuser','password','salt')";
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
            Assert.AreEqual(true, dao.CheckLogin("testUser", "password"));
            Assert.AreEqual(true, dao.CheckLogin("testuser", "password"));
            Assert.AreEqual(false, dao.CheckLogin("testuser", "Password"));

        }
        [TestMethod]
        public void ChangeLoginPasswordTest()
        {
            dao.ChangeLoginPassword("testuser", "password", "NewPassword");
            Assert.AreEqual(true, dao.CheckLogin("testuser", "NewPassword"));
            Assert.AreEqual(false, dao.CheckLogin("testuser", "password"));
        }
        [TestMethod]
        public void CheckPasswordChange_Or_CheckPasswordCreation()
        {
            Assert.AreEqual(true, dao.CheckPasswordValid("Test.Password!", "Test.Password!"));
            Assert.AreEqual(false, dao.CheckPasswordValid("Test.passwprd!", "Test.Password!"));
            Assert.AreEqual(false, dao.CheckPasswordValid("TestPassword12!", "testPassword12!"));

        }



    }

}
