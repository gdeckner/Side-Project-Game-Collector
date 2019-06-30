using System;
using Game_Collector.DAL;
using Game_Collector.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameDataBase.DAL.IGDBDAO;
using System.Linq;

namespace GameDataBase.test.DAL
{
   [TestClass]
   public class GenresIGDBTest
    {
        private GenresIGDBDAO dao;
        [TestInitialize]
        public  void Setup()
        {
            
            dao = new GenresIGDBDAO("07b6d5257e21d7bbaa02fb68a9a13899");
        }
        [TestMethod]
        public void PullAllGenresTest()
        {
            IList<Genres> test = dao.PullAllGenres();
            test = test.OrderBy(x => x.genre_iD).ToList();
            Assert.AreEqual(2, test[0].genre_iD);
            Assert.AreEqual("Point-and-click", test[0].genre_Name);
            
        }
        [TestMethod]
        public void PullSingleGenreTest()
        {
            Genres test = new Genres();
            test = dao.PullSpecificGenre(2);
            Assert.AreEqual("Point-and-click", test.genre_Name);
        }
    }
}
