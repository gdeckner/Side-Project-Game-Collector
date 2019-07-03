using System;
using Game_Collector.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Collector.DAL.Interfaces
{
   public interface ICoversDAO
    {

       //Primarly used for verifying if cover exists in the SQL database
        bool CheckCoverValid(int coverId);

        //Pulls cover information from data source
        Covers PullCover(int coverId);

        //Primarly used for inserting new cover data into SQL database
        void PushCover(int coverId,string url);

       
    }
}
