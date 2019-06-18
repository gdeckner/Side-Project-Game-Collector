using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Collector.Models
{
    public class UserGameInfo
    {
        public int entry_Id { get; set; }
        public int game_Id { get; set; }
        public bool game_isOwned { get; set; }
        public bool game_onWish { get; set; }
        public decimal game_Progress { get; set; }
        public string userLogin { get; set; }
    }
}
