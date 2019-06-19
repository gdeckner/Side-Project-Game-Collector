﻿using System;
using Game_Collector.Models;
using Game_Collector.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Collector.DAL
{
    public class UserGameInfoSQLDAO : IUserGameInfoDAO
    {
        private string connectionString;

        public UserGameInfoSQLDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public bool CheckIfValid(string userName)
        {
            throw new NotImplementedException();
        }

        public IList<UserGameInfo> PullUserGameInfo(string userName)
        {
            throw new NotImplementedException();
        }

        public UserGameInfo PushUserGameInfo(string userName)
        {
            throw new NotImplementedException();
        }

        public UserGameInfo UpdateOwnedOrWishList(int gameId, bool isOwnedValue, bool isTrue)
        {
            throw new NotImplementedException();
        }
    }
}
