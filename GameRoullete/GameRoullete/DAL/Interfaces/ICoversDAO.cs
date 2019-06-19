using System;
using Game_Collector.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Collector.DAL.Interfaces
{
   public interface ICoversDAO
    {
        //Checks if gameID and that values exist valid
        bool CheckCoverValid(int gameId);

        //Returns Cover ID and Cover URl based on GameID
        Covers PullCover(int gameId);

        //Adds Cover ID and url based on gameID and indicates success
        Covers PushCover(int coverId,string url);

       

    }
}
