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

        //Pulls game from local and returns closest matches
        IList<GameInfo> PullGameInfo(string gameName);

        //Pulls all games from db
        IList<GameInfo> PullAllGameInfoFromDB();

        //Adds game info to DB and returns if it worked
        void PushGameInfo(int gameId, string gameName, string gameDescription, int genreID, int platformID,int franchiseId, int gameCover);
    }
}
