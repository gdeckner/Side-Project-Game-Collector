using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Collector.Models
{
    public class UserGameInfo
    {
        public int Entry_Id { get; set; }
        public int Game_Id { get; set; }
        public bool Game_isOwned { get; set; }
        public bool Game_onWish { get; set; }
        public double Game_Progress { get; set; }
        public string User_name { get; set; }
    }
}
