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
    public class GenresIGDBDAO : IGenresDAO
    {
        private string apiKey;


        public GenresIGDBDAO(string igdbApiKey)
        {
            apiKey = igdbApiKey;

        }

        public bool CheckGenreID(int genreID)
        {
            //created from interface, not needed to utilize IGDB
            throw new NotImplementedException();
        }

        public IList<Genres> PullAllGenres()
        {
            //Creates IGDB client using our apiKey
            var igdb = IGDB.Client.Create(Environment.GetEnvironmentVariable(apiKey)); 
            igdb.ApiKey = apiKey;
            
            IList<Genres> pulledGenreList = new List<Genres>();
            //Queries IGDB and pulls 50 genre Ids and names
            var getGenre =  Task.Run(() => igdb.QueryAsync<Genre>(IGDB.Client.Endpoints.Genres, query: "limit 50;fields *;")).Result;
            for (int i = 0; i < getGenre.Count(); i++)
            {
                Genres newGenre = new Genres
                {
                    genre_iD = (int)getGenre[i].Id,
                    genre_Name = (string)getGenre[i].Name
                };
                pulledGenreList.Add(newGenre);

            }
            return pulledGenreList;
        }

        public Genres PullSpecificGenre(int genreID)
        {
            //Creates IGDB client using our apiKey
            var igdb = IGDB.Client.Create(Environment.GetEnvironmentVariable(apiKey)); 
            igdb.ApiKey = apiKey;
            //Queries IGDB and pulls genre name based on genre ID that we pulled from our game search method
            var getGenre = Task.Run(() => igdb.QueryAsync<Genre>(IGDB.Client.Endpoints.Genres, query: $"fields *;where id = {genreID};")).Result;
            Genres pulledGenre = new Genres
            {
                genre_iD = (int)getGenre.First().Id,
                genre_Name = (string)getGenre.First().Name

            };
            return pulledGenre;


        }

        public void PushGenre(int genreId, string genreName)
        {
            //created from interface, not needed to utilize IGDB
            throw new NotImplementedException();
        }
     
  
    }
}
