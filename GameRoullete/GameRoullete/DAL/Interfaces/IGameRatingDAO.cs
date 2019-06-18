using System;
using Game_Collector.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Collector.DAL.Interfaces
{
    public interface IGameRatingDAO
    {
        //Checks if gameID is valid
        bool CheckGameRatingID(int gameID);

        //Pulls game rating based on gameID
        IList<GameRating> PullGameRating(int gameID);

        //Inserts gameRating values into the DB and verifies if it worked
        bool PushGameRating(int gameID, int gameHype, double gamePopularity, int gameRatingCount, double gameTotalRating);
    }
}
