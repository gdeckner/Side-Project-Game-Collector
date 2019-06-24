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
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"insert into Franchises (franchise_id,franchise_name) values(101,'Coder Legacy')";
                cmd.ExecuteNonQuery();
            }
        }
        [TestMethod]
        public void CheckFranchiseIDTest()
        {
            Assert.AreEqual(true, dao.CheckFranchiseID(101));
            Assert.AreEqual(false, dao.CheckFranchiseID(123213123));
        }
        [TestMethod]
        public void PushFranchisesTest()
        {
            Franchises test = new Franchises();
            dao.PushFranchise(340, "A series of unfortunate calls");
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"select * from Franchises where franchise_Id = 340";
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    test.franchise_Id = (int)reader["franchise_id"];
                    test.franchise_Name = (string)reader["franchise_name"];
                }
            }
            Assert.AreEqual(340, test.franchise_Id);
            Assert.AreEqual("A series of unfortunate calls", test.franchise_Name);

        }
        [TestMethod]
        public void PullFranchisesTest()
        {
            Franchises test = new Franchises();
            test = dao.PullSpecificFranchise(101);
            Assert.AreEqual(101, test.franchise_Id);
            Assert.AreEqual("Coder Legacy", test.franchise_Name);
        }
      
        [TestMethod]
        public void FullFranchiseTest()
        {
            dao.PushFranchise(420, "CyberRocker");
            Franchises test = new Franchises();
            test = dao.PullSpecificFranchise(420);
            Assert.AreEqual(true, dao.CheckFranchiseID(420));
            Assert.AreEqual(420, test.franchise_Id);
            Assert.AreEqual("CyberRocker", test.franchise_Name);


        }

    }
}
