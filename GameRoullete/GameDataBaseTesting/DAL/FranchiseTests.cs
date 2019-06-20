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
    public class FranchiseTests : DatabaseTest
    {
        private FranchisesSQLDAO dao;
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            dao = new FranchisesSQLDAO(ConnectionString);
        }
        [TestMethod]
        public void CheckFranchiseIDTest()
        {
            Franchises franchise = new Franchises
            {
                franchise_Id = 137
            };
            dao.pulledFranchises.Add(franchise);
            Assert.AreEqual(true, dao.CheckFranchiseID(137));
            Assert.AreEqual(false, dao.CheckFranchiseID(123444));
        }
        [TestMethod]
        public void InsertFranchisesTest()
        {
            Franchises test = dao.InsertFranchises(349, "Coder Wars");
            Assert.AreEqual(349, test.franchise_Id);
            Assert.AreEqual("Coder Wars", test.franchise_Name);
   
        }
        [TestMethod]
        public void PullAllFranchisesTest()
        {
            IList<Franchises> test = dao.PullFranchise();
            Assert.AreEqual(2, test.Count);
        }
      
        [TestMethod]
        public void FullFranchiseTest()
        {
            dao.InsertFranchises(349, "Code Wars");
            IList<Franchises> test = dao.PullFranchise();

            Assert.AreEqual(4, test.Count);
            Assert.AreEqual("Code Wars", test[0].franchise_Name);
            Assert.AreEqual(137, test[1].franchise_Id);
            Assert.AreEqual(true, dao.CheckFranchiseID(349));
        }

    }
}
