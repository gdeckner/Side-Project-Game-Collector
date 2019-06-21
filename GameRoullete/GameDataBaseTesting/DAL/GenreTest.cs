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
    public class GenreTest : DatabaseTest
    {
        private GenresSQLDAO dao;
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            dao = new GenresSQLDAO(ConnectionString);
        }
        [TestMethod]
        public void CheckGenreIDTest()
        {
            Genres test = new Genres
            {
                genre_iD = 5
            };
            dao.pulledGenres.Add(test);
            Assert.AreEqual(true, dao.CheckGenreID(5));
            Assert.AreEqual(false, dao.CheckGenreID(92893));
        }
        [TestMethod]
        public void PullAllGenresTest()
        {
            IList<Genres> test = dao.PullAllGenres();
            Assert.AreEqual(2, test.Count);
            Assert.AreEqual(5, test[0].genre_iD);
            Assert.AreEqual(28, test[1].genre_iD);
        }
        [TestMethod]
        public void PullSpecificGenreTest()
        {
            Genres test = new Genres();
            test = dao.PullSpecificGenre(5);
            Assert.AreEqual("Shooter", test.genre_Name);
        }
        [TestMethod]
        public void PushGenresTest()
        {
            Genres test = dao.PushGenre(987, "Spooky");
            Assert.AreEqual(987, test.genre_iD);
            Assert.AreEqual("Spooky", test.genre_Name);

        }
        [TestMethod]
        public void FullPushGenreTest()
        {
            dao.PushGenre(987, "Spooky");
            IList <Genres> test = dao.PullAllGenres();

            Assert.AreEqual("Spooky", test[3].genre_Name);
            Assert.AreEqual(987, test[3].genre_iD);
            Assert.AreEqual(4, test.Count);
            Assert.AreEqual(true, dao.CheckGenreID(987));
        }


    }
}
