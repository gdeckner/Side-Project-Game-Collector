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
        }
        [TestMethod]
        public void CheckifNameValidTest()
        {
            Assert.AreEqual(true, dao.CheckIfValid("spark1"));
        }
        [TestMethod]
        public void CreateLoginTest()
        {

            UserLogin test = dao.CreateLogin("testUser", "password");
            Assert.AreEqual(1, test.userId);
            Assert.AreEqual("testUser", test.userName);
        }
        [TestMethod]
        public void CheckLoginTest()
        {
           
            Assert.AreEqual(true, dao.CheckLogin("foxtrot", "password"));
            Assert.AreEqual(false, dao.CheckLogin("testUser", "Password"));
        }
        [TestMethod]
        public void ChangeLoginPasswordTest()
        {

            Assert.AreEqual(true, dao.ChangeLoginPassword("foxtrot", "password", "NewPassword"));
            
        }
        [TestMethod]
        public void FullLoginTest()
        {
            Assert.AreEqual(true,dao.CreateLogin("testUser", "password"));
            Assert.AreEqual(true,dao.CheckLogin("testUser", "password"));
            Assert.AreEqual(true, dao.ChangeLoginPassword("testUser", "password", "NewPassword"));
            Assert.AreEqual(true, dao.CheckLogin("testUser", "NewPassowrd"));
            
        }



    }

}
