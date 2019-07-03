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
       
        //Pulls game rating from SQL Server
        GameRating PullGameRating(int rating_Id);

        //Inserts gameRating values into the DB
        int PushGameRating(int gameHype, int gamePopularity, int gameRatingCount, int gameTotalRating);
    }
        
}
