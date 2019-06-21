using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Collector.Models
{
   public class GameRating
    {
        public double game_Total_Rating { get; set; }
        public double game_Total_Rating_Count { get; set; }
        public double   game_popularity { get; set; }
        public double game_Hype { get; set; }
        public int game_id { get; set; } //PK/FK
    }
}
