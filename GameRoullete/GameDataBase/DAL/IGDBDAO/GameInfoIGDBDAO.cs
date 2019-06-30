using System;
using Game_Collector.DAL.Interfaces;
using System.Collections.Generic;
using System.Text;
using Game_Collector.Models;
using IGDB;
using IGDB.Models;
using System.Linq;
using System.Threading.Tasks;
using Game_Collector.DAL;

namespace GameDataBase.DAL.IGDBDAO
{
    public class GameInfoIGDBDAO : IGameInfoDAO
    {

        private string apiKey; //Stores personal apiKey for IGDB client
        private string connectionString; //Storesd our connection information to connect to SQL
        GameRatingSQLDAO daoPushRating; //Creating Rating class here due to it needed that raw return query of game search to minimize code
        public GameInfoIGDBDAO(string getKey)
        {
            apiKey = getKey;
        }
        public GameInfoIGDBDAO(string getKey, string getConnection)
        {
            //Setting values that get passed thru the constructor to set up our class
            apiKey = getKey;
            connectionString = getConnection;
            daoPushRating = new GameRatingSQLDAO(connectionString);
        }
        public int CheckGameInfo(string gameName)
        {
            //created from interface, not needed to utilize IGDB
            throw new NotImplementedException();
        }

        public GameInfo PullGameInfo(string gameName)
        {
            //created from interface, might add something here later to get only one result instead of a list
            throw new NotImplementedException();
        }

        public IList<GameInfo> PullMuliGameInfo(string gameName)
        {
            //Creates a new list of games
            IList<GameInfo> pulledGames = new List<GameInfo>();
            //Setting our query string up for IGDB
            gameName = "search " + "\"" + gameName + "\"" + ";fields * ;";
            //Creates IGDB client using our apiKey
            var igdb = IGDB.Client.Create(Environment.GetEnvironmentVariable(apiKey)); 
            igdb.ApiKey = apiKey;
            //Queries IGDB and pulls all the game info then creates a GameInfo class and sets the values then returns it all as a list of games
            var games = Task.Run(() => igdb.QueryAsync<Game>(IGDB.Client.Endpoints.Games, query: gameName)).Result; 
            for(int i = 0;i<games.Count();i++)
            {
                int? popularity = (int?)games[i].Popularity;
                if(popularity == null) { popularity = 0; }
                int? totalRating = (int?)games[i].TotalRating;
                if (totalRating == null) { totalRating = 0; }
                int? totalRatingCount = (int?)games[i].TotalRatingCount;
                if (totalRatingCount == null) { totalRatingCount = 0; }
                int? hype = (int?)games[i].Hypes;
                if (hype == null) { hype = 0; }

                //Ratings are only available from querying games on IGDB, due to this creating the Rating here is neccessary to reduce queries(limit is 10k a month for free)
                int newRatingId = daoPushRating.PushGameRating((int)hype, (int)popularity, (int)totalRating, (int)totalRatingCount);
                GameInfo newGame = new GameInfo
                {
                    coverID = (int)games[i].Cover.Id,
                    franchiseID = (int)games[i].Franchise.Id,
                    gameDescription = (string)games[i].Summary,
                    gameName = (string)games[i].Name,
                    game_ID = (int)games[i].Id,
                    genreID = games[i].Genres.Ids.Select(x => (int)x).ToArray(),
                    platformID = games[i].Platforms.Ids.Select(x => (int)x).ToArray(),
                    ratingId = newRatingId
                    

                };
                pulledGames.Add(newGame);

            }
            return pulledGames;
           
        }

        public void PushGameInfo(int gameId, string gameName, string gameDescription, int []genreID, int[] platformID, int franchiseId, int coverId, int ratingId)
        {
            throw new NotImplementedException();
        }
    }
}
