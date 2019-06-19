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
    public class CoversTest : DatabaseTest
    {
        private CoversSQLDAO dao;
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            dao = new CoversSQLDAO(ConnectionString);
        }
        [TestMethod]
        public void CheckCoverValidTest()
        {
            bool result = dao.CheckCoverValid(55403);
            Assert.AreEqual(true, result);

            result = dao.CheckCoverValid(123444);
            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void PullCoverTest()
        {
            Covers cover = new Covers();
            string url = "https://images.igdb.com/igdb/image/upload/t_cover_big/bcotwv6rapvdglcahxi3.jpg";
            cover = dao.PullCover(740);

            Assert.AreEqual(55403, cover.cover_ID);
            Assert.AreEqual(url,cover.cover_Url);

        }
        [TestMethod]
        public void PushCoverTest()
        {
            string result;
            string sql = @"Select cover_Url From Covers Where cover_ID = 666;";

            Covers cover = new Covers
            {
                cover_ID = 666,
                cover_Url = "Testurl"

            };
            dao.PushCover(cover.cover_ID, cover.cover_Url);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                result = cmd.ExecuteScalar().ToString();
                
            }
           
            Assert.AreEqual("Testurl", result);

        }

    }
}
