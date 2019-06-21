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
    public class GameRatingTest : DatabaseTest
    {
        private GameRatingSQLDAO dao;
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            dao = new GameRatingSQLDAO(ConnectionString);
        }
        [TestMethod]
        public void CheckGameRatingTest()
        {
            GameRating pushedRating = new GameRating
            {
                game_id = 740,
                game_Hype = 120,
                game_popularity = 10.3,
                game_Total_Rating = 85.7,
                game_Total_Rating_Count = 328
            };
            dao.pulledGameRating.Add(pushedRating);
            Assert.AreEqual(true, dao.CheckGameRatingID(740));
            Assert.AreEqual(false,dao.CheckGameRatingID(9545));
        }
        [TestMethod]
        public void PullGameRatingTest()
        {
            GameRating test = dao.PullGameRating(740);

            Assert.AreEqual(740, test.game_id);
            Assert.AreEqual(10.3, test.game_popularity);
            Assert.AreEqual(85.7, test.game_Total_Rating);
            Assert.AreEqual(328, test.game_Total_Rating_Count);
        }
        [TestMethod]
        public void PushGameRatingTest()
        {
            GameRating test = dao.PushGameRating(9000, 0, 900, 395, 100);

            Assert.AreEqual(9000, test.game_id);
            Assert.AreEqual(0, test.game_Hype);
            Assert.AreEqual(100, test.game_Total_Rating);
            Assert.AreEqual(395, test.game_Total_Rating_Count);
        }
        [TestMethod]
        public void FullGameRatingTest()
        {
            dao.PushGameRating(9000, 0, 900, 395, 100);
            GameRating test = dao.PullGameRating(9000);

            Assert.AreEqual(9000, test.game_id);
            Assert.AreEqual(395, test.game_Total_Rating_Count);
            Assert.AreEqual(true, dao.CheckGameRatingID(9000));
        }
       

    }
}
