using System;
using Game_Collector.Models;
using Game_Collector.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Collector.DAL
{
    public class GameRatingSQLDAO : IGameRatingDAO
    {
        private string connectionString;

        public GameRatingSQLDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public bool CheckGameRatingID(int gameID)
        {
            throw new NotImplementedException();
        }

        public GameRating PullGameRating(int gameID)
        {
            throw new NotImplementedException();
        }

        public GameRating PushGameRating(int gameID, int gameHype, double gamePopularity, int gameRatingCount, double gameTotalRating)
        {
            throw new NotImplementedException();
        }
    }
}
