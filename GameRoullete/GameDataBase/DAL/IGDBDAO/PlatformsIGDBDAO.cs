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
    public class PlatformsIGDBDAO : IPlatformsDAO
    {//Stores personal apiKey for IGDB client
        private string apiKey;

        public PlatformsIGDBDAO(string igdbApiKey)
        {
            apiKey = igdbApiKey;

        }
        public bool CheckPlatformID(int platformID)
        {
            //created from interface, not needed to utilize IGDB
            throw new NotImplementedException();
        }

        public IList<Platforms> PullAllPlatforms()
        {
            //Creates IGDB client using our apiKey
            var igdb = IGDB.Client.Create(Environment.GetEnvironmentVariable(apiKey));
            igdb.ApiKey = apiKey;

            IList<Platforms> pulledPlatforms = new List<Platforms>();

            //Queries IGDB and pulls 50 platform Ids and names
            var getPlatforms = Task.Run(() => igdb.QueryAsync<Platform>(IGDB.Client.Endpoints.Platforms, query: "limit 50;fields *;")).Result;
            for (int i = 0; i < getPlatforms.Count(); i++)
            {
                Platforms newPlatform = new Platforms
                {
                    Platform_Id = (int)getPlatforms[i].Id,
                    Platform_Name = (string)getPlatforms[i].Name
                };
                pulledPlatforms.Add(newPlatform);
            }
            return pulledPlatforms;
        }
        public Platforms PullSpecificPlatform(int platformID)
        {
            //Creates IGDB client using our apiKey
            var igdb = IGDB.Client.Create(Environment.GetEnvironmentVariable(apiKey));
            igdb.ApiKey = apiKey;

            //Queries IGDB and pulls platform name based on platform ID that we pulled from our game search method
            var getPlatforms = Task.Run(() => igdb.QueryAsync<Platform>(IGDB.Client.Endpoints.Platforms, query: $"fields *; where id ={platformID};")).Result;
            Platforms pulledPlatform = new Platforms
            {
                Platform_Id = platformID,
                Platform_Name = getPlatforms[0].Name
            };


            return pulledPlatform;
        }

        public void PushPlatform(int platformID, string name)
        {
            //created from interface, not needed to utilize IGDB
            throw new NotImplementedException();
        }
    }
}
