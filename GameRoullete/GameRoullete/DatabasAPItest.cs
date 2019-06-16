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

    public class DatabasAPItest
    {

        private string ApiKey
        {
            get
            {
                return "07b6d5257e21d7bbaa02fb68a9a13899";
            }
        }
        public async Task TTREAsync()
        {
            //https://images.igdb.com/igdb/image/upload/t_cover_big //
            var igdb = IGDB.Client.Create(Environment.GetEnvironmentVariable(ApiKey));
            igdb.ApiKey = ApiKey;
            Console.WriteLine(igdb.ApiKey);
            Console.ReadLine();
            string cover = "55403";
            string input = "Halo";
            input = "search " + "\"" + input + "\"";
         
           
            var gammes = await igdb.QueryAsync<Game>(IGDB.Client.Endpoints.Games, query: "fields *;where franchise = 137;");
            var covertest = await igdb.QueryAsync<Game>(IGDB.Client.Endpoints.Covers, query: "fields *;where id = 55403;"); 
            string othertest = covertest.First().Url.Substring(43);
            var game = gammes.First();
            Dictionary<string, Game> test = new Dictionary<string, Game>();
            test.Add("Halo", game);
            othertest = @"https://images.igdb.com/igdb/image/upload/t_cover_big" + othertest;
            var coverSmall = IGDB.ImageHelper.GetImageUrl(imageId: covertest.First().Url, size: ImageSize.CoverBig, retina: false);
            ////var games = await igdb.QueryAsync<Game>(IGDB.Client.Endpoints.Games, query: testt);

            //var game = games.First();
            //Console.WriteLine(game.Id);
            //Console.WriteLine(game.Name);
            //Console.WriteLine(game.Cover.Value.Url);


            Console.ReadLine();

            ////game.Id; // 4
            //game.Name; // Thief
        }

    }
}
