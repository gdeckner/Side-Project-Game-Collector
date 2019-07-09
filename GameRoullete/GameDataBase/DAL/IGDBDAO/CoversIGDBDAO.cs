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
    public class CoversIGDBDAO : ICoversDAO
    {

        private string apiKey; //Stores personal apiKey for IGDB client
        public CoversIGDBDAO(string getKey)
        {
            apiKey = getKey;
        }
        public bool CheckCoverValid(int coverId)
        {
            //created from interface, not needed to utilize IGDB
            throw new NotImplementedException();
        }

        public Covers PullCover(int coverId) 
        {
            //Creates IGDB client using our apiKey
            var igdb = IGDB.Client.Create(Environment.GetEnvironmentVariable(apiKey)); 
            igdb.ApiKey = apiKey;
            
            //Queries IGDB and pulls url based on cover ID that we pull from our game search method
            var coverImage = Task.Run(()=> igdb.QueryAsync<Cover>(IGDB.Client.Endpoints.Covers, query: $"fields *; where id = {coverId};" )).Result;
            Covers newCover = new Covers
            {
                Cover_ID = coverId,
                Cover_Url = @"https://images.igdb.com/igdb/image/upload/t_cover_big/" + $"{coverImage[0].ImageId}.jpg"
            };
            return newCover;
        }

        public void PushCover(int coverId, string url)
        {
            //created from interface, not needed to utilize IGDB
            throw new NotImplementedException();
        }
    }
}
