using System;
using Game_Collector.DAL;
using Game_Collector.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace GameDataBase.test.DAL
{
    [TestClass]
    public class GameInfoTest : DatabaseTest
    {
        public int rtingID = 0;
        private GameInfoSQLDAO dao;
        [TestInitialize]
        public override void Setup()
        {
            
            base.Setup();
            dao = new GameInfoSQLDAO(ConnectionString);
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"
                insert into Franchises (franchise_id,franchise_name) values(0,'No Franchise')
                insert into Covers (cover_id,cover_url) values(0,'testurl.com')
                insert into Genres (genre_id,genre_name) values(0,'Test Genre')
                insert into Platforms (platform_id,platform_name) values (0,'PizzaPiGameRunner')
                insert into Ratings (popularity,hype,rating,rating_count) values (100,200,5,7)
                select scope_identity()";
                
                rtingID = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.CommandText = @"insert into Games(game_id, game_name, rating_id, platform_id_array, cover_id, genre_id_array, franchise_id, game_description) values(200, 'CW 01', @ratingId, 0, 0, 0, 0, 'Coder wars the start!')";
                cmd.Parameters.AddWithValue("@ratingId", rtingID);
                cmd.ExecuteNonQuery();
            }
        }
        [TestMethod]
        public void CheckGameNameExistTest()
        {
            Assert.AreEqual(1, dao.CheckGameInfo("CW 01"));
            Assert.AreEqual(0, dao.CheckGameInfo("Turtle Wars"));
        }
   
        [TestMethod]
        public void PullGameInfoTest()
        {
            GameInfo test = new GameInfo();
            test = dao.PullGameInfo("CW 01");
            Assert.AreEqual(200, test.game_ID);
            Assert.AreEqual("CW 01", test.gameName);
            Assert.AreEqual(0, test.genreID[0]);
        }
        [TestMethod]
        public void PushGameInfoTest()
        {
            int[] testArray = new int[1] { 0 };
            int[] testOtherArray = new int[1] { 0 };
            GameInfo test = new GameInfo();
            dao.PushGameInfo(999, "MechaCoder", "Enter the weapon that will change Coder Wars forever", testArray, testOtherArray, 0,0,rtingID);
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"select * from Games where game_id = 999";
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    test.game_ID = (int)reader["game_id"];
                    test.franchiseID = (int)reader["franchise_id"];
                    test.gameDescription = (string)reader["game_description"];
                    test.gameName = (string)reader["game_name"];
                    test.genreID = reader["genre_id_array"].ToString().Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                    test.genreID = reader["platform_id_array"].ToString().Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                    test.ratingId = (int)reader["rating_id"];
                }
            }
            Assert.AreEqual(999, test.game_ID);
            Assert.AreEqual(rtingID, test.ratingId);
            Assert.AreEqual("MechaCoder", test.gameName);
            
        }
        [TestMethod]
        public void PullGamebyIDTest()
        {
            GameInfo test = new GameInfo();
            test = dao.PullGameByID(200);
            Assert.AreEqual("CW 01", test.gameName);
        }
        [TestMethod]
        public void FullGameInfoTest()
        {
            int[] testArray = new int[1] { 0 };
            int[] testOtherArray = new int[1] { 0 };
            dao.PushGameInfo(999, "CW Mecha", "Enter the weapon that will change Coder Wars forever", testArray, testOtherArray, 0, 0,rtingID);
            GameInfo test = new GameInfo();
            IList<GameInfo> testList = new List<GameInfo>();
            testList = dao.PullMuliGameInfo("CW");
            test = dao.PullGameInfo("CW Mecha");

            Assert.AreEqual(2, testList.Count);
            Assert.AreEqual(2, dao.CheckGameInfo("CW"));
            Assert.AreEqual(999, testList[1].game_ID);
        }

    }
}
