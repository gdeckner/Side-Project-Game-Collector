using System;
using Game_Collector.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Collector.DAL.Interfaces
{
    public interface IGenresDAO
    {
        //Checks if Genre ID exists
        bool CheckGenreID(int genreID);

        //Pulls Specific Genre Name by the ID
        Genres PullSpecificGenre(int genreID);

        //Returns entire Genre ID and Name table
        IList<Genres> PullAllGenres();

        //Adds new Genres to table
        void PushGenre(int genreId,string genreName);


    }
}
