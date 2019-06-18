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
        public bool CheckGameInfo(string gameName)
        {
            throw new NotImplementedException();
        }

        public IList<GameInfo> MultiPullGameInfo(string gameName)
        {
            throw new NotImplementedException();
        }

        public IList<GameInfo> PullGameInfo(string gameName)
        {
            throw new NotImplementedException();
        }

        public bool PushGameInfo(int gameId, string gameName, string gameDescription, int genreID, List<int> platformID, int gameCover)
        {
            throw new NotImplementedException();
        }
    }
}
