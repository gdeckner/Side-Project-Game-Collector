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

       
        bool CheckCoverValid(int coverId);

        
        Covers PullCover(int coverId);

        
        void PushCover(int coverId,string url);

       
    }
}
