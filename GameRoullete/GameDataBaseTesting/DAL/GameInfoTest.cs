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
    public class GameInfoTest : DatabaseTest
    {
        private GameInfoSQLDAO dao;
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            dao = new GameInfoSQLDAO(ConnectionString);
        }
        [TestMethod]
        public void CheckGameNameExistTest()
        {
            GameInfo test = new GameInfo
            {
                game_ID = 5000,
                gameName = "Coder Tycoon",
                gameDescription = "Live the fantasy of code development",
                genreID = 3,
                platformID = 5,
                coverID = 3000,
                franchiseID = 137
            };
            dao.pulledGame.Add(test);
            Assert.AreEqual(true, dao.CheckGameInfo("Coder Tycoon"));
        }
        [TestMethod]
        public void PullAllGameTest()
        {
            IList<GameInfo> test = dao.PullAllGameInfoFromDB();
            Assert.AreEqual(2, test.Count);

        }
        [TestMethod]
        public void PullGameInfoTest()
        {
            GameInfo test = new GameInfo
            {
                game_ID = 5000,
                gameName = "Coder Tycoon",
                gameDescription = "Live the fantasy of code development",
                genreID = 3,
                platformID = 5,
                coverID = 3000,
                franchiseID = 137
            };
            dao.pulledGame.Add(test);
            Assert.AreEqual(5000, dao.pulledGame[0].game_ID);
        }
        [TestMethod]
        public void PushGameInfoTest()
        {
            int gameID = 5000;
            string name = "Coder Tycoon";
            string description = "Live the fantasy of code development";
            int genreID = 5;
            int platformID = 3;
            int coverId = 3000;
            int franchiseId = 137;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"insert into Covers (cover_Id,cover_Url) values(3000,'testU')";
                cmd.ExecuteNonQuery();
                
            }
                GameInfo test = dao.PushGameInfo(gameID, name, description, genreID, platformID, franchiseId, coverId);

            Assert.AreEqual(5000, test.game_ID);
            Assert.AreEqual("Coder Tycoon", test.gameName);
            Assert.AreEqual(5, test.genreID);
            Assert.AreEqual(3,test.platformID);
            Assert.AreEqual(3000, test.coverID);

            
        }
        [TestMethod]
        public void FullGameInfoTest()
        {
            int gameID = 5000;
            string name = "Coder Tycoon";
            string description = "Live the fantasy of code development";
            int genreID = 5;
            int platformID = 6;
            int coverId = 3000;
            int franchiseId = 137;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"insert into Covers (cover_Id,cover_Url) values(3000,'testU')";
                cmd.ExecuteNonQuery();

            }
           
            IList<GameInfo> test = dao.PullAllGameInfoFromDB();
            dao.PushGameInfo(gameID, name, description, genreID, platformID,franchiseId, coverId);
            Assert.AreEqual(3, test.Count);
            Assert.AreEqual(3000, test[2].coverID);
            Assert.AreEqual("Halo: Combat Evolved", test[0].gameName);

            test = dao.PullGameInfo("Halo");
            Assert.AreEqual(9000, test[1].game_ID);
            Assert.AreEqual(true, dao.CheckGameInfo("Coder Tycoon"));


        }

    }
}
