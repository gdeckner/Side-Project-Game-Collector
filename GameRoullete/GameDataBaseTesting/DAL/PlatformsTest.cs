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
        }
        [TestMethod]
        public void CheckPlatformIDTest()
        {
            Platforms test = new Platforms
            {
                platform_Id = 6,
                platform_Name = "test"
            };
            dao.pulledPlatforms.Add(test);
            Assert.AreEqual(true, dao.CheckPlatformID(6));
            Assert.AreEqual(false, dao.CheckPlatformID(04324234));
        }
        [TestMethod]
        public void PullAllPlatformsTest()
        {
            IList < Platforms > test = dao.PullAllPlatforms();

            Assert.AreEqual(2, test.Count);
            Assert.AreEqual("360scope", test[0].platform_Name);
            Assert.AreEqual(6, test[1].platform_Id);
        }
        [TestMethod]
        public void PullSpecificPlatformTest()
        {
            Platforms test = dao.PullSpecificPlatform(3);
            Assert.AreEqual("360scope", test.platform_Name);
        }
        [TestMethod]
        public void PushPlatformTest()
        {
            Platforms test = dao.PushPlatform(56, "MarioPlayer");
            Assert.AreEqual(56, test.platform_Id);
            Assert.AreEqual("MarioPlayer", test.platform_Name);
        }
        [TestMethod]
        public void FullPlatformTest()
        {
            dao.PushPlatform(56, "MarioPlayer");
            Platforms test = dao.PullSpecificPlatform(56);
            IList<Platforms> testList = dao.PullAllPlatforms();

            Assert.AreEqual(5, testList.Count);
            Assert.AreEqual("MarioPlayer", testList[0].platform_Name);
            Assert.AreEqual(3, testList[2].platform_Id);
            Assert.AreEqual(true, dao.CheckPlatformID(56));
        }


    }
}
