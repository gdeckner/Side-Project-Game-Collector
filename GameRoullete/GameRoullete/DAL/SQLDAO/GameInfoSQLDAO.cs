using System;
using Game_Collector.Models;
using Game_Collector.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Collector.DAL
{
    public class GameInfoSQLDAO : IGameInfoDAO
    {
        private string connectionString;

        public GameInfoSQLDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public bool CheckGameInfo(string gameName)
        {
            throw new NotImplementedException();
        }

        public IList<GameInfo> MultiPullGameInfo(string gameName)
        {
            throw new NotImplementedException();
        }

        public GameInfo PullGameInfo(string gameName)
        {
            throw new NotImplementedException();
        }

        public GameInfo PushGameInfo(int gameId, string gameName, string gameDescription, int genreID, List<int> platformID, int gameCover)
        {
            throw new NotImplementedException();
        }
    }
}
