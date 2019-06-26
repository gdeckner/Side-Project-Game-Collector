using System;
using Game_Collector.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Collector.DAL.Interfaces
{
    public interface IUserGameInfoDAO
    {
        //Check if UserName exists
        bool CheckIfValid(string userId);

        //Returns all games associated with userName
        IList<UserGameInfo> PullUserGameInfo(string userId);

        UserGameInfo PullSingleUserGameInfo(string userId, int gameId);

        //Adds a game associated with userName
        void PushUserGameInfo(string userId,int gameId,int progress,bool owned,bool wish);

        //Modifies the game associated with username with if it is owned or on the wishlist
        void UpdateUserGame(int gameId, bool isOwnedValue, bool isTrue,string userId,int progress);


    }
}
