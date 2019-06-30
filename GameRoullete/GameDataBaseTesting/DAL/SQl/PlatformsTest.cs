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
    public class PlatformsTest : DatabaseTest
    {
        private PlatformsSQLDAO dao;
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            dao = new PlatformsSQLDAO(ConnectionString);
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"insert into Platforms (platform_id,platform_name) values(23,'CODE DS'),
                (20,'CODE NDS'),(1,'boom')";
                cmd.ExecuteNonQuery();
            }
        }
        
        [TestMethod]
        public void CheckPlatformExists()
        {
            Assert.AreEqual(true, dao.CheckPlatformID(23));
            Assert.AreEqual(false, dao.CheckPlatformID(99));
        }
        [TestMethod]
        public void PullSpecificPlatformTest()
        {
            Platforms test = new Platforms();
            test = dao.PullSpecificPlatform(23);
            Assert.AreEqual(23, test.platform_Id);
            Assert.AreEqual("CODE DS", test.platform_Name);
        }
        [TestMethod]
        public void PushPlatformTest()
        {
            dao.PushPlatform(42, "Lifebox 2");
            Platforms test = new Platforms();
            test = dao.PullSpecificPlatform(42);
            Assert.AreEqual(42, test.platform_Id);
            Assert.AreEqual("Lifebox 2", test.platform_Name);
        }
        [TestMethod]
        public void PullAllPlatformsTest()
        {
            IList<Platforms> test = new List<Platforms>();
            test = dao.PullAllPlatforms();
            Assert.AreEqual(3, test.Count);
            Assert.AreEqual(1, test[0].platform_Id);
        }



    }
}
