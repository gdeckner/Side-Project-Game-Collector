using System;
using Game_Collector.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;

namespace Game_Collector.DAL.Interfaces
{
   public interface IUserLoginDAO
    {
        //Checks if account info is valid
        bool CheckLogin(string userName, SecureString password);

        //Checks if userName is being used
        bool CheckIfValid(string userName);

        //Creates a new user
        bool CreateLogin(string userName, SecureString password);

        //Change User Password
        bool ChangeLoginPassword(SecureString password);
        
    }
}
