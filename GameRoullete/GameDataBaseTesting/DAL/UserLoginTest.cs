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
            Assert.AreEqual(true, dao.CheckIfValid("testUser"));
            Assert.AreEqual(true, dao.CheckIfValid("testuser"));
            Assert.AreEqual(false, dao.CheckIfValid("ooga"));
        }
        [TestMethod]
        public void CreateLoginTest()
        {
            dao.CreateLogin()
        }
        [TestMethod]
        public void CheckLoginTest()
        {
          
        }
        [TestMethod]
        public void ChangeLoginPasswordTest()
        {

        }
        [TestMethod]
        public void FullLoginTest()
        {
           
            
        }



    }

}
