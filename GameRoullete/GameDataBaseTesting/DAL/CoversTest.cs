using Game_Collector.DAL;
using Game_Collector.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameDataBase.test.DAL
{
    [TestClass]
    public class CoversTest : DatabaseTest
    {
        private CoversSQLDAO dao;
        
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            dao = new CoversSQLDAO(ConnectionString);
            
        }
        [TestMethod]
        public void CheckCoverValidTest()
        {
            Covers test = new Covers
            {
                cover_ID = 55403
            };
            dao.pulledCovers.Add(test);
            Assert.AreEqual(true, dao.CheckCoverValid(55403));
            Assert.AreEqual(false, dao.CheckCoverValid(123444));
           


        }
        [TestMethod]
        public void PullCoverTest()
        {
            Covers cover = new Covers();
            string url = "https://images.igdb.com/igdb/image/upload/t_cover_big/bcotwv6rapvdglcahxi3.jpg";
            cover = dao.PullCover(55403);

            Assert.AreEqual(55403, cover.cover_ID);
            Assert.AreEqual(url, cover.cover_Url);

        }
        [TestMethod]
        public void PushCoverTest()
        {
            Covers test = dao.PushCover(99, "CoderwarsImage");
            Assert.AreEqual(99, test.cover_ID);
            Assert.AreEqual("CoderwarsImage", test.cover_Url);

        }
        [TestMethod]
        public void FullCoverTest()
        {
            dao.PushCover(99, "SuperCoderWars");
           Covers test=  dao.PullCover(99);

            Assert.AreEqual(99, test.cover_ID);
            Assert.AreEqual("SuperCoderWars", test.cover_Url);
            Assert.AreEqual(true, dao.CheckCoverValid(99));


        }
    }
}
