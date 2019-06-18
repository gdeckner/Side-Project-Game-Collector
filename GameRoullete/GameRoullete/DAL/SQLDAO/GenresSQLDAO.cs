using System;
using Game_Collector.Models;
using Game_Collector.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Collector.DAL
{
    public class GenresSQLDAO : IGenresDAO
    {
        public bool CheckGenreID(int genreID)
        {
            throw new NotImplementedException();
        }

        public IList<Genres> PullAllGenres()
        {
            throw new NotImplementedException();
        }

        public IList<Genres> PullSpecificGenre(int genreID)
        {
            throw new NotImplementedException();
        }

        public bool PushAllGenres()
        {
            throw new NotImplementedException();
        }
    }
}
