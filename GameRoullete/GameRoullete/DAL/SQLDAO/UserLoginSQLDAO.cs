using System;
using Game_Collector.Models;
using Game_Collector.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;

namespace Game_Collector.DAL
{
    public class UserLoginSQLDAO : IUserLoginDAO
    {
        public bool ChangeLoginPassword(SecureString password)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfValid(string userName)
        {
            throw new NotImplementedException();
        }

        public bool CheckLogin(string userName, SecureString password)
        {
            throw new NotImplementedException();
        }

        public bool CreateLogin(string userName, SecureString password)
        {
            throw new NotImplementedException();
        }
    }
}
