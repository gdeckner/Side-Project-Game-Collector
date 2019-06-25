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
        public int rtingID;
        private UserGameInfoSQLDAO dao;
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            dao = new UserGameInfoSQLDAO(ConnectionString);
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
                cmd.CommandText = @"insert into Games(game_id, game_name, rating_id, platform_id, cover_id, genre_id, franchise_id, game_description)
                values(200, 'CW 01', @ratingId, 0, 0, 0, 0, 'Coder wars the start!'),(300,'Faja',@ratingId,0,0,0,0,'Mr Taco presents')";
                cmd.Parameters.AddWithValue("@ratingId", rtingID);
                cmd.ExecuteNonQuery();
                cmd.CommandText = @"insert into UserInfo (userName,password,salt) values ('testUser','password','salt')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = @"insert into UserGameInfo (userName,game_id,progress,owned,wishlist)
                 values ('testUser',200,0,1,0)";
                cmd.ExecuteNonQuery();
            }
        }
        [TestMethod]
        public void CheckifNameValidTest()
        {
            Assert.AreEqual(true, dao.CheckIfValid("testUser"));
            Assert.AreEqual(false, dao.CheckIfValid("uuga"));
        }
        [TestMethod]
        public void PullUserSingleGameInfoTest()
        {
            UserGameInfo test = new UserGameInfo();
            test = dao.PullSingleUserGameInfo("testUser",200);
            Assert.AreEqual(false, test.game_onWish);
            Assert.AreEqual(200, test.game_Id);
            Assert.AreEqual(true, test.game_isOwned);
        }
        [TestMethod]
        public void PushUserGameInfoTest()
        {
            dao.PushUserGameInfo("testUser",300);
            UserGameInfo test = new UserGameInfo();
            test = dao.PullSingleUserGameInfo("testUser", 300);
            Assert.AreEqual(300, test.entry_Id);
            Assert.AreEqual(null, test.game_Progress);
        }
        [TestMethod]
        public void UpdateOwnedorWishListTest()
        {
           
            
        }
        [TestMethod]
        public void UserGameInfoAllTest()
        {

            IList<UserGameInfo> test = new List<UserGameInfo>();
            test = dao.PullUserGameInfo("testUrl");
        }


    }
}
