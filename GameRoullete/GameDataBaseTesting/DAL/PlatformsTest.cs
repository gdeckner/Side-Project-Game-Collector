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
            
            Assert.AreEqual(true, dao.CheckPlatformID(6));
            Assert.AreEqual(false, dao.CheckPlatformID(04324234));
        }
        [TestMethod]
        public void CheckPlatformExists()
        {
            
        }
        [TestMethod]
        public void PullSpecificPlatformTest()
        {

        }
        [TestMethod]
        public void PushPlatformTest()
        {

        }



    }
}
