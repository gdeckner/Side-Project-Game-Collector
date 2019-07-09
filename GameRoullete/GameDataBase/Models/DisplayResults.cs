using System;
using System.Collections.Generic;
using System.Text;
using Game_Collector.Models;
using GameDataBase.Models;

namespace GameDataBase.Models
{
    public class DisplayResults
    {
        public GameRating Rating { get; set; }
        public IList<Platforms> Platform { get; set; }
        public UserGameInfo UserGameInfo { get; set; }
        public  IList<Genres> Genre { get; set; }
        public Franchises Franchise { get; set; }
        public GameInfo GameInfo { get; set; }
    }
}
