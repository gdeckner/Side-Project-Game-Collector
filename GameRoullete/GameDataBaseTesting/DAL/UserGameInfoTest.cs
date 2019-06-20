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
            Assert.AreEqual(true, dao.CheckIfValid("sparky1"));
            Assert.AreEqual(false, dao.CheckIfValid("mufasarocks"));
        }
        [TestMethod]
        public void PullUserGameInfoTest()
        {
            IList<UserGameInfo> test = dao.PullUserGameInfo("sparky1");
            Assert.AreEqual(2, test.Count);
            Assert.AreEqual(740, test[1].game_Id);
        }
        [TestMethod]
        public void PushUserGameInfoTest()
        {
            UserGameInfo test = dao.PushUserGameInfo("foxtrot", 9000);
            Assert.AreEqual(9000, test.game_Id);
            Assert.AreEqual("foxtrot", test.userLogin);
        }
        [TestMethod]
        public void UpdateOwnedorWishListTest()
        {
            UserGameInfo test = dao.UpdateOwnedOrWishList(9000, true, false, "foxtrot");
            Assert.AreEqual(true, test.game_isOwned);
            Assert.AreEqual(false, test.game_onWish);
            
        }
        [TestMethod]
        public void UserGameInfoAllTest()
        {
            dao.PushUserGameInfo("foxtrot", 9000);
            dao.PushUserGameInfo("foxtrot", 740);
            IList<UserGameInfo> test = dao.PullUserGameInfo("foxtrot");
            dao.UpdateOwnedOrWishList(9000, false, true, "foxtrot");
            test = dao.PullUserGameInfo("foxtrot");

            Assert.AreEqual(2, test.Count);
            Assert.AreEqual(740, test[1].game_Id);
            Assert.AreEqual(9000, test[0].game_Id);


        }


    }
}
