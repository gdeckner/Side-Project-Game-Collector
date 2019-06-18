using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGDB;
using IGDB.Models;
using RestEase;

namespace Game_Collector
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
        {
            get
            {
                return @"https://images.igdb.com/igdb/image/upload/t_cover_big";
            }

        }
        private Dictionary<string, string> PlatformData = new Dictionary<string, string>();
        private Dictionary<string, Game> StoreGameData = new Dictionary<string, Game>();
        private Dictionary<string, string> GenreIdData = new Dictionary<string, string>();

        //To DO
        // Handle bad inputs
        public async Task PullSpecificGameAsync(string gameName)
        { //Will take the first result it pulls from IGDB database
            gameName = "search " + "\"" + gameName + "\"" + ";fields * ;";
            var igdb = IGDB.Client.Create(Environment.GetEnvironmentVariable(ApiKey)); //Takes api and creates client to run the queries
            igdb.ApiKey = ApiKey;

            var games = await igdb.QueryAsync<Game>(IGDB.Client.Endpoints.Games, query: gameName); //Uses the client to query t database then stores it in variable
            var game = games.First();
            string title = game.Name;
            if (!StoreGameData.Keys.Contains(title))
            {
                StoreGameData.Add(title, game);
            }



        }
        public Dictionary<string, List<string>> PullAllStoredGames(string gameName)
        {
            Dictionary<string, List<string>> pullAllStoredGames = new Dictionary<string, List<string>>();

            return pullAllStoredGames;

        }

        public async Task PullFranchiseAsync(string franchiseID)
        {

            List<string> listID = new List<string>();
            franchiseID = @"fields *; where name ~ " + '"' + franchiseID + '"' + ';';
            var igdb = IGDB.Client.Create(Environment.GetEnvironmentVariable(ApiKey));
            igdb.ApiKey = ApiKey;

            var franchiseGameID = await igdb.QueryAsync<Franchise>(IGDB.Client.Endpoints.Franchies, query: franchiseID);

            for (int i = 0; i < franchiseGameID.First().Games.Ids.Count(); i++)
            {
                var games = await igdb.QueryAsync<Game>(IGDB.Client.Endpoints.Games, query: "fields *;limit 50;where id = " + franchiseGameID.First().Games.Ids[i] + ";");
                if (!StoreGameData.Keys.Contains(games[0].Name) && games[0].VersionTitle is null && !games[0].Name.Contains("duplicate"))
                {
                    StoreGameData.Add(games[0].Name, games[0]);
                }

            }


        }

        public async Task PullTopRatedGamesAsync(string[] platformName)
        {

            var igdb = IGDB.Client.Create(Environment.GetEnvironmentVariable(ApiKey));
            igdb.ApiKey = ApiKey;
            string output = "";
            string[] id = new string[platformName.Length];

            for (int i = 0; i < platformName.Length; i++)
            {
                id[i] = PlatformData[platformName[i].ToLower()];
            }
            output = String.Join(",", id);
            output = @"fields *;limit 50; where platforms = [" + output + "];sort total_rating desc;where total_rating != null;where total_rating_count >= 500;";
            var topGames = await igdb.QueryAsync<Game>(IGDB.Client.Endpoints.Games, query: output);

            for (int i = 0; i < topGames.Count(); i++)
            {

                if (!StoreGameData.Keys.Contains(topGames[i].Name))
                {
                    StoreGameData.Add(topGames[i].Name, topGames[i]);
                }

            }

        }
        //public async Task PullTopRatedGamesAsync(string[] platformName, string releaseYear)
        //{
        //    var igdb = IGDB.Client.Create(Environment.GetEnvironmentVariable(ApiKey));
        //    igdb.ApiKey = ApiKey;
        //    string output = "";
        //    string[] id = new string[platformName.Length];

        //    for (int i = 0; i < platformName.Length; i++)
        //    {
        //        id[i] = PlatformData[platformName[i].ToLower()];
        //    }
        //    output = String.Join(",", id);
        //    output = @"fields *;limit 50; where platforms = [" + output + "];sort total_rating desc;where total_rating != null;where total_rating_count >= 500;where first_release_date.Year >= " + releaseYear + ";";

        //    var topGames = await igdb.QueryAsync<Game>(IGDB.Client.Endpoints.Games, query: output);

        //    for (int i = 0; i < topGames.Count(); i++)
        //    {

        //        if (!StoreGameData.Keys.Contains(topGames[i].Name))
        //        {
        //            StoreGameData.Add(topGames[i].Name, topGames[i]);
        //        }

        //    }

        //}
        public void GetPlatformID()
        {
            string filepath = @"C:\Users\georgd\source\repos\Personal Side Projects\personal-side-projects\GameRoullete\";
            string file = "PlatformDictionary.txt";
            string output = "";
            string[] lineDivider;

            using (StreamReader sr = new StreamReader(filepath + file))
            {
                output = sr.ReadToEnd();
            }
            output = output.Replace('\r', ' ').Replace('\n', '\t');
            lineDivider = output.Split('\t');

            for (int i = 0; i < lineDivider.Length - 1; i += 2)
            {
                PlatformData.Add(lineDivider[i + 1].ToLower().Remove(lineDivider[i + 1].Length - 1), lineDivider[i]);
            }
        }
        public async Task PullGenreIdAsync()
        {
            var igdb = IGDB.Client.Create(Environment.GetEnvironmentVariable(ApiKey));
            igdb.ApiKey = ApiKey;

            var getGenre = await igdb.QueryAsync<Genre>(IGDB.Client.Endpoints.Genres, query: "limit 50;fields *;");
            for (int i = 0; i < getGenre.Count(); i++)
            {

                GenreIdData.Add(getGenre[i].Id.ToString(), getGenre[i].Name);

            }


        }
    }

}
