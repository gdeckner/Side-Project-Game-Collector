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
        bool CheckIfValid(string userName);

        //Returns all games associated with userName
        IList<UserGameInfo> PullUserGameInfo(string userName);

        //Adds a game associated with userName and returns success
        bool PushUserGameInfo(string userName);

        //Modifies the game associated with username with if it is owned or on the wishlist
        bool UpdateOwnedOrWishList(int gameId, bool isOwnedValue, bool isTrue);
    }
}
