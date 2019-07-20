using System;
using Game_Collector.DAL.Interfaces;
using System.Collections.Generic;
using System.Text;
using Game_Collector.Models;
using IGDB;
using IGDB.Models;
using System.Linq;
using System.Threading.Tasks;

namespace GameDataBase.DAL.IGDBDAO
{
    public class FranchisesIGDBDAO : IFranchisesDAO
    {
        private string apiKey; //Stores personal apiKey for IGDB client
        public FranchisesIGDBDAO(string getKey)
        {
            apiKey = getKey;
        }

        public bool CheckFranchiseID(int franchiseID)
        {
            //created from interface, not needed to utilize IGDB
            throw new NotImplementedException();
        }

        public Franchises PullSpecificFranchise(int franchiseID)
        {
            //Creates IGDB client using our apiKey
            var igdb = IGDB.Client.Create(Environment.GetEnvironmentVariable(apiKey));
            igdb.ApiKey = apiKey;

            //Queries IGDB and pulls url based on franchise ID that we pull from our game search method
            var franchiseInfo = Task.Run(()=> igdb.QueryAsync<Franchise>(IGDB.Client.Endpoints.Franchies, query: $"fields *;where id = {franchiseID};")).Result;
            Franchises newFranchise = new Franchises
            {
                Franchise_Id = franchiseID,
                Franchise_Name = franchiseInfo[0].Name
            };
            return newFranchise;
        }

        public void PushFranchise(int franchiseID, string name)
        {
            //created from interface, not needed to utilize IGDB
            throw new NotImplementedException();
        }
    }
}
