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
        bool CheckIfValid(int userId);

        //Returns all games associated with userName
        IList<UserGameInfo> PullUserGameInfo(int userId);

        //Adds a game associated with userName and returns success
        void PushUserGameInfo(int userId,int gameId);

        //Modifies the game associated with username with if it is owned or on the wishlist
        void UpdateOwnedOrWishList(int gameId, bool isOwnedValue, bool isTrue,int userId);


    }
}
