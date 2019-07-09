using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Collector.Models
{
   public class GameRating
    {
        public int Game_Total_Rating { get; set; }
        public int Game_Total_Rating_Count { get; set; }
        public int   Game_popularity { get; set; }
        public int Game_Hype { get; set; }
        public int Rating_Id { get; set; }
    }
}
