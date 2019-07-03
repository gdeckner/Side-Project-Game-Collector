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
        
        //Primarly used to insert new franchise info into SQL database
        void PushFranchise(int franchiseID,string name);

        //Primarly used to check if data exists in the SQL database
        bool CheckFranchiseID(int franchiseID);

        //Pulls franchise based on its ID from a database
        Franchises PullSpecificFranchise(int franchiseID);

    
    }
}
