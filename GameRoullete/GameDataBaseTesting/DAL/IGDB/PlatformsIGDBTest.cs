using System;
using Game_Collector.DAL;
using Game_Collector.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameDataBase.DAL.IGDBDAO;
using System.Linq;
namespace GameDataBase.test.DAL.IGDB
{
    [TestClass]
    public class PlatformsIGDBDAOTest
    {
        private string apiKey = "07b6d5257e21d7bbaa02fb68a9a13899";
        private PlatformsIGDBDAO dao;
        [TestInitialize]
        public void Setup()
        {
            dao = new PlatformsIGDBDAO(apiKey);
        }
        [TestMethod]
        public void PullAllPlatformsTest()
        {
            IList<Platforms> test = dao.PullAllPlatforms();
            test = test.OrderBy(x => x.platform_Id).ToList();
            Assert.AreEqual(50, test.Count());

        }
        [TestMethod]
        public void PullSinglePlatformTest()
        {
            Platforms test = new Platforms();
            test = dao.PullSpecificPlatform(48);
            Assert.AreEqual("PlayStation 4", test.platform_Name);
        }
    }

}
