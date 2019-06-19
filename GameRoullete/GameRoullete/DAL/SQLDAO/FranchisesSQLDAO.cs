using System;
using Game_Collector.Models;
using Game_Collector.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Collector.DAL
{
    public class FranchisesSQLDAO : IFranchisesDAO
    {
        private string connectionString;

        public FranchisesSQLDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public bool CheckFranchiseID(int franchiseID)
        {
            throw new NotImplementedException();
        }

        public Franchises InsertFranchises(int franchiseID, string name)
        {
            throw new NotImplementedException();
        }

        public IList<Franchises> PullFranchise()
        {
            throw new NotImplementedException();
        }

        public Franchises PullSpecificFranchise(int franchiseID)
        {
            throw new NotImplementedException();
        }
    }
}
