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
        public int rtingID = 0;
        private GameRatingSQLDAO dao;
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            dao = new GameRatingSQLDAO(ConnectionString);
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"insert into Ratings (popularity,hype,rating,rating_count) values (100,200,5,7)
                select scope_identity()";
                rtingID = Convert.ToInt32(cmd.ExecuteScalar());

            }
        }
        [TestMethod]
        public void PullGameRatingTest()
        {
            GameRating test = new GameRating();
            test = dao.PullGameRating(rtingID);
            Assert.AreEqual(100, test.Game_popularity);
            Assert.AreEqual(200, test.Game_Hype);
            Assert.AreEqual(5, test.Game_Total_Rating);
            Assert.AreEqual(7, test.Game_Total_Rating_Count);
        }
        [TestMethod]
        public void PushGameRatingTest()
        {
            int newRatingId;
            GameRating test = new GameRating();
           newRatingId = dao.PushGameRating(9000, 51, 23, 55);
            test = dao.PullGameRating(newRatingId);
            Assert.AreEqual(rtingID + 1, test.Rating_Id);
            Assert.AreEqual(51, test.Game_popularity);

        }


    }



}

