using System;
using Game_Collector.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using 

namespace Game_Collector.DAL.Interfaces
{
   public interface IUserLoginDAO
    {
        //Checks if account info is valid
        bool CheckLogin(string userName, string password);

        //Checks if userName is being used
        bool CheckIfValid(string userName);
        //Checks if passwords match and is valid
        bool CheckPasswordValid(string password1, string password2);

        //Creates a new user
        UserLogin CreateLogin(string userName, string password);

        //Change User Password
        UserLogin ChangeLoginPassword(string userName, string oldpassword,string newPassword);

        
        
    }
}
