using System;
using Game_Collector.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Collector.DAL.Interfaces
{
    public interface IPlatformsDAO
    {
        //Checks if platform exists
        bool CheckPlatformID(int platformID);

        //Pulls Specific platform Name by the ID
        Platforms PullSpecificPlatform(int platformID);

     
        //Adds Platform ID and name to table 
        void PushPlatform(int platformID,string name);
    }
}

