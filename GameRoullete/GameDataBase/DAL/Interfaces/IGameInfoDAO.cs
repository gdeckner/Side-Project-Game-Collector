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
        //Returns number of result based on name, if 0 indicates it was not found
        int CheckGameInfo(string gameName);

        //Pulls game from server based on closest Match
        GameInfo PullGameInfo(string gameName);

        //Pulls all games that start with the game name from a database
        IList<GameInfo> PullMuliGameInfo(string gameName);

        //Primarly used to a new game into the SQL server
        void PushGameInfo(int gameId, string gameName, string gameDescription, int [] genreID, int [] platformID,int franchiseId, int coverId,int ratingId);
    }
}
