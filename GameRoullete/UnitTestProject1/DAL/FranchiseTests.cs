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
            bool result = dao.CheckFranchiseID(137);
            Assert.AreEqual(true, result);

            result = dao.CheckFranchiseID(123444);
            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void InsertFranchisesTest()
        {
            string result;
            string sql = @"Select franchise_Name from Franchises where franchise_Id = 666 ;";
            Franchises franchise = new Franchises
            {
                franchise_Id = 666,
                franchise_Name = "Testurl"

            };
            dao.InsertFranchises(franchise.franchise_Id, franchise.franchise_Name);
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                result = cmd.ExecuteScalar().ToString();

            }

           
            Assert.AreEqual("Testurl",result);

        }
        [TestMethod]
        public void PullAllFranchisesTest()
        {
            IList<Franchises> test = dao.PullFranchise();
            Assert.AreEqual(2, test.Count);
        }
        [TestMethod]
        public void PullSpecificFranchiseTest()
        {
            Franchises franchise = new Franchises();
            franchise = dao.PullSpecificFranchise(137);

            Assert.AreEqual("Halo", franchise.franchise_Name);
            Assert.AreEqual(137, franchise.franchise_Id);
        }

    }
}
