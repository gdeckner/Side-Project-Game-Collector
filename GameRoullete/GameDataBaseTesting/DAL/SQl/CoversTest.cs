using Game_Collector.DAL;
using Game_Collector.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace GameDataBase.test.DAL
{
    [TestClass]
    public class CoversTest : DatabaseTest
    {
        private CoversSQLDAO dao;
        
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            dao = new CoversSQLDAO(ConnectionString);
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"insert into Covers (cover_id,cover_url) values(123,'testurl.com')";
                cmd.ExecuteNonQuery();
            }
            
        }
        [TestMethod]
        public void CheckCoverValidTest()
        {
            Assert.AreEqual(true, dao.CheckCoverValid(123));
            Assert.AreEqual(false, dao.CheckCoverValid(123213123));

        }
        [TestMethod]
        public void PullCoverTest()
        {
            Covers test = new Covers();
            test = dao.PullCover(123);
            Assert.AreEqual(123, test.Cover_ID);
            Assert.AreEqual("testurl.com", test.Cover_Url);

        }
        [TestMethod]
        public void PushCoverTest()
        {
            Covers test = new Covers();
            dao.PushCover(666, "newurltest.com");
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"Select * from Covers where cover_Id = 666";
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    test.Cover_ID = (int)reader["cover_id"];
                    test.Cover_Url = (string)reader["cover_url"];
                }
            }
            Assert.AreEqual(666, test.Cover_ID);
            Assert.AreEqual("newurltest.com", test.Cover_Url);
               

        }
        [TestMethod]
        public void FullCoverTest()
        {
            dao.PushCover(666, "newurltest.com");
            Covers test = new Covers();
            test = dao.PullCover(666);
            Assert.AreEqual(true, dao.CheckCoverValid(666));
            Assert.AreEqual(666, test.Cover_ID);
            Assert.AreEqual("newurltest.com", test.Cover_Url);
            
        }
    }
}
