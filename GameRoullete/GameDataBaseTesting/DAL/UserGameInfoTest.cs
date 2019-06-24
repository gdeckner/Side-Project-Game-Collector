using System;
using Game_Collector.DAL;
using Game_Collector.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace GameDataBase.test.DAL
{
    [TestClass]
    public class UserGameInfoTest : DatabaseTest
    {
        private UserGameInfoSQLDAO dao;
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            dao = new UserGameInfoSQLDAO(ConnectionString);
        }
        [TestMethod]
        public void CheckifNameValidTest()
        {
            
        }
        [TestMethod]
        public void PullUserGameInfoTest()
        {
            
        }
        [TestMethod]
        public void PushUserGameInfoTest()
        {
        }
        [TestMethod]
        public void UpdateOwnedorWishListTest()
        {
           
            
        }
        [TestMethod]
        public void UserGameInfoAllTest()
        {
            

        }


    }
}
