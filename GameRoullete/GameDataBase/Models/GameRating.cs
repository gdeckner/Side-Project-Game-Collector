using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Collector.Models
{
   public class GameRating
    {
        public int game_Total_Rating { get; set; }
        public int game_Total_Rating_Count { get; set; }
        public int   game_popularity { get; set; }
        public int game_Hype { get; set; }
        public int rating_Id { get; set; }
    }
}
