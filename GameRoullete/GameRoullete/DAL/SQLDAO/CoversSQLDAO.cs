using System;
using Game_Collector.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_Collector.DAL.Interfaces;

namespace Game_Collector.DAL
{
    public class CoversSQLDAO : ICoversDAO
    {
        public bool CheckCoverValid(int gameId)
        {
            throw new NotImplementedException();
        }

        public IList<Covers> InsertCover(int coverId, string url)
        {
            throw new NotImplementedException();
        }

        public IList<Covers> PullCover(int gameId)
        {
            throw new NotImplementedException();
        }

        bool ICoversDAO.InsertCover(int coverId, string url)
        {
            throw new NotImplementedException();
        }
    }
}
