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
           
        }
        [TestMethod]
        public void CreateLoginTest()
        {
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
