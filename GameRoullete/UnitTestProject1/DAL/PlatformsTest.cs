﻿using System;
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
        private CoversSQLDAO dao;
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            dao = new CoversSQLDAO(ConnectionString);
        }
        [TestMethod]
        public void CheckPlatformIDTest()
        {

        }
        [TestMethod]
        public void PullAllPlatformsTest()
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
