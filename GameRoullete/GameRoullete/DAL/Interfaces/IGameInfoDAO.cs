using System;
using System.Collections.Generic;
using Game_Collector.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Collector.DAL.Interfaces
{
   public interface IGameInfoDAO
    {
        //Checks if game exists
        bool CheckGameInfo(string gameName);

        //Pulls specific game info based on closest match
        GameInfo PullGameInfo(string gameName);

        //Pulls all games that start with name
        IList<GameInfo> MultiPullGameInfo(string gameName);

        //Adds game info to DB and returns if it worked
        GameInfo PushGameInfo(int gameId, string gameName, string gameDescription, int genreID, List<int> platformID, int gameCover);
    }
}
