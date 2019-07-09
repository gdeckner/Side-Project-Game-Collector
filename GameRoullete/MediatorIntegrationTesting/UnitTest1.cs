using Game_Collector.DAL;
using Game_Collector.Models;
using GameDataBase;
using GameDataBase.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;

namespace MediatorIntegrationTesting
{
    [TestClass]
    public class UnitTest1

    {
        private IConfigurationRoot config;
        private TransactionScope transaction;
        GameInfoSQLDAO daoGameInfo;
        CoversSQLDAO daoCovers;
        GameRatingSQLDAO daoRatings;
        FranchisesSQLDAO daoFranchises;
        GameRatingSQLDAO daoGameRatings;
        protected IConfigurationRoot Config
        {
            get
            {
                if (config == null)
                {
                    var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");

                    config = builder.Build();
                }
                return config;
            }
        }
        protected string ConnectionString
        {
            get
            {
                return Config.GetConnectionString("Test");
            }
        }
        DataBaseMediator mediator = new DataBaseMediator();
        public int rtingID;
        [TestInitialize]

        public virtual void Setup()
        {

            daoCovers = new CoversSQLDAO(ConnectionString);
            daoGameInfo = new GameInfoSQLDAO(ConnectionString);
            daoRatings = new GameRatingSQLDAO(ConnectionString);
            daoFranchises = new FranchisesSQLDAO(ConnectionString);
            daoGameRatings = new GameRatingSQLDAO(ConnectionString);

            transaction = new TransactionScope();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"delete from UserGameInfo
                                    delete from UserInfo
                                    delete from Games
                                    delete from Ratings
                                    delete from Covers
                                    delete from Platforms
                                    delete from Genres
                                    delete from Franchises";

                conn.Open();
                cmd.ExecuteNonQuery();

            }
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
                cmd.CommandText = @"insert into Games(game_id, game_name, rating_id, platform_id_array, cover_id, genre_id_array, franchise_id, game_description)
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
        public void GameSearchTest()
        {
            IList<GameInfo> test = new List<GameInfo>();
            test = mediator.GetGames("Halo", false);
            bool result = (test.Count != 0) ? true : false;
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void CoverAddToSQLTest()
        {
            IList<GameInfo> testList = new List<GameInfo>();
            testList = mediator.GetGames("Halo", false);
            mediator.PushCoversIntoSQL(testList);
            Covers test = new Covers();
            test = daoCovers.PullCover(55403);
            Assert.AreEqual(@"https://images.igdb.com/igdb/image/upload/t_cover_big/bcotwv6rapvdglcahxi3.jpg", test.Cover_Url);
        }
        [TestMethod]
        public void FranchiseAddToSQLTest()
        {
            IList<GameInfo> testList = new List<GameInfo>();
            testList = mediator.GetGames("Halo", false);
            mediator.PushFranchiseIntoSQL(testList);
            Franchises test = new Franchises();
            test = daoFranchises.PullSpecificFranchise(137);
            Assert.AreEqual(137, test.Franchise_Id);
            Assert.AreEqual("Halo", test.Franchise_Name);
        }
        [TestMethod]
        public void RatingsAddToSQLTest() //Testing is limited due to the values always changing
        {
            IList<GameInfo> testList = new List<GameInfo>();
            testList = mediator.GetGames("Halo", false);
            GameRating testRating = new GameRating();
            testRating = daoGameRatings.PullGameRating(testList[0].ratingId);
            Assert.AreEqual(0, testRating.Game_Hype);



        }

        [TestMethod]
        public void GameSearchAddToSQL()
        {
            IList<GameInfo> test = new List<GameInfo>();
            test = mediator.GetGames("Halo", false);
            int result = daoGameInfo.CheckGameInfo("Halo");

            Assert.AreEqual(10, result);
        }
        [TestMethod]
        public void LoginFunctionsTest()
        {
            Assert.AreEqual("Account was succesfully created!", mediator.CreateLogin("testNewUser", @"testPassword1!", @"testPassword1!"));
            Assert.AreEqual("Passwords either do not match or do not fit the criteria, Passwords must be 8 in length, contain one number,one uppercase" +
                        "and one special character", mediator.CreateLogin("testUserAgain", "w", "w"));
            Assert.AreEqual("Username already exists, please try a different one", mediator.CreateLogin("testNewUser", "wewe", "wewe"));

            Assert.AreEqual(true, mediator.Login("testNewUser", "testPassword1!"));
            Assert.AreEqual(true, mediator.Login("testNewUser", "testPassword1!"));
            Assert.AreEqual(false, mediator.Login("testNewUser", "testpassword1!"));
            Assert.AreEqual(false, mediator.ChangePassword("testNewUser", "testPassword1!", "newTestPassword", "newTestPassword"));
            Assert.AreEqual(true, mediator.ChangePassword("testNewUser", "testPassword1!", "newTestPassword1!", "newTestPassword1!"));
            Assert.AreEqual(true, mediator.Login("testNewUser", "newTestPassword1!"));


        }
        [TestMethod]
        public void DisplayUserGameInfoTest()
        {
            IList<DisplayResults> testUser = new List<DisplayResults>();
            testUser = mediator.GetUserList("testuser");
            Assert.AreEqual(1, testUser.Count);
            Assert.AreEqual(200, testUser[0].GameInfo.game_ID);
        }
        [TestCleanup]
        public void Cleanup()
        {
            transaction.Dispose();
        }
    }
}
