using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGDB;
using IGDB.Models;
using RestEase;

namespace GameRoullete
{


    public class PullFromDatabase
    {
        private string ApiKey
        {
            get
            {
                return "07b6d5257e21d7bbaa02fb68a9a13899";
            }
        }
        private string coverUrl
        { get
            {
                return @"https://images.igdb.com/igdb/image/upload/t_cover_big";
            }

        }
        private Dictionary<string, Game> StoreGameData;

        public PullFromDatabase()
        {

        }
        public async Task<Dictionary<string, Game>> PullSpecificGameAsync (string gameName)
        { //Will take the first result it pulls from IGDB database
            gameName = "search " + "\"" + gameName + "\"" + ";fields * ;";
            var igdb = IGDB.Client.Create(Environment.GetEnvironmentVariable(ApiKey)); //Takes api and creates client to run the queries
            igdb.ApiKey = ApiKey;
            Dictionary<string,Game> pullGame = new Dictionary<string, Game>(); //Store the data we pull from database

            var games = await igdb.QueryAsync<Game>(IGDB.Client.Endpoints.Games, query: gameName); //Uses the client to query to database then stores it in variable
            var game = games.First();
            string title = game.Name;
            pullGame.Add(title, game);
            StoreGameData.Add(title,game);
          
            return pullGame;
       
        }
        public Dictionary<string, List<string>> PullAllStoredGames(string gameName)
        {
            Dictionary<string, List<string>> pullAllStoredGames = new Dictionary<string, List<string>>();

            return pullAllStoredGames;

        }
        public Dictionary<string, List<string>> PullSequels(string gameName)
        {
            Dictionary<string, List<string>> pullSequels = new Dictionary<string, List<string>>();

            return pullSequels;

        }
        public Dictionary<string, List<string>> PullFranchise(string gameName)
        {
            Dictionary<string, List<string>> pullFranchise = new Dictionary<string, List<string>>();

            return pullFranchise;

        }
        public Dictionary<string, List<string>> PullTopRatedGames(string gameName)
        {
            Dictionary<string, List<string>> pullTopRatedGames = new Dictionary<string, List<string>>();

            return pullTopRatedGames;

        }
        public Dictionary<string, List<string>> PullTopRatedGames(string gameName,string platform)
        {
            Dictionary<string, List<string>> pullTopRatedGames = new Dictionary<string, List<string>>();

            return pullTopRatedGames;

        }
        public Dictionary<string, List<string>> PullTopRatedGames(string gameName,string platform,int amountReturned)
        {
            Dictionary<string, List<string>> pullTopRatedGames = new Dictionary<string, List<string>>();

            return pullTopRatedGames;

        }
        public Dictionary<string, List<string>> PullTopRatedGames(string gameName,int amountReturned)
        {
            Dictionary<string, List<string>> pullTopRatedGames = new Dictionary<string, List<string>>();

            return pullTopRatedGames;

        }

    }
   
}
