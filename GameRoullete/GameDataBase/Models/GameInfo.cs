using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Collector.Models
{
    public class GameInfo
    {
        public int game_ID { get; set; } //PK
        public string gameName { get; set; }
        public string gameDescription { get; set; }
        public int genreID { get; set; }
        public int franchiseID { get; set; }
        public int coverID { get; set; }
        public int platformID { get; set; }
        public int ratingId { get; set; }
    }

}
