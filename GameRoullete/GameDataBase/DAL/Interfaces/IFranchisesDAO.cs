using System;
using Game_Collector.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Collector.DAL.Interfaces
{
    public interface IFranchisesDAO
    {
        
        //Inserts Franchise ID and String into DB
        void PushFranchise(int franchiseID,string name);

        //Checks if FranchiseID exists
        bool CheckFranchiseID(int franchiseID);

        //Pulls franchise name based on ID
        Franchises PullSpecificFranchise(int franchiseID);

    
    }
}
