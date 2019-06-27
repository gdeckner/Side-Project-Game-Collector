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
        bool CheckLogin(string userName, string password);

        //Checks if userName is being used
        bool CheckIfUserNameExists(string userName);

        //Creates a new user
        void CreateLogin(string userName, string password);

        //Change User Password
        void ChangeLoginPassword(string userName,string newPassword);

        
        
    }
}
