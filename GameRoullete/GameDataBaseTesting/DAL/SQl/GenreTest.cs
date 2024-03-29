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
    public class GenreTest : DatabaseTest
    {
        private GenresSQLDAO dao;
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            dao = new GenresSQLDAO(ConnectionString);
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"insert into Genres (genre_id,genre_name) values(10,'Code Fun'),(5,'Code Smash'),(8,'Fun Fun')";
                cmd.ExecuteNonQuery();
            }
        }
        [TestMethod]
        public void CheckGenreIDTest()
        {
            Assert.AreEqual(true, dao.CheckGenreID(10));
            Assert.AreEqual(false, dao.CheckGenreID(200));
        }
        [TestMethod]
        public void PullSpecificGenreTest()
        {
            Genres test = new Genres();
            test = dao.PullSpecificGenre(10);
            Assert.AreEqual("Code Fun", test.Genre_Name);
        }
        [TestMethod]
        public void PushGenreTest()
        {
            dao.PushGenre(23, "Turtles");
            Genres test = new Genres();
            test = dao.PullSpecificGenre(23);
            Assert.AreEqual("Turtles", test.Genre_Name);
        }
        [TestMethod]
        public void PullAllGenresTest()
        {
            IList<Genres> testList = dao.PullAllGenres();
            Assert.AreEqual(3, testList.Count);
        }
        [TestMethod]
        public void PullGenresByID()
        {
            int[] testArray = new int[] {10,5,8};
            IList<Genres> testList = dao.PullGenreList(testArray);
            Assert.AreEqual(3, testList.Count);
        }


}
}
