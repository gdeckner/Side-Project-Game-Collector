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
        PlatformID PullSpecificPlatform(int platformID);

        //Returns entire Platform ID and Name table
        IList<Platforms> PullAllPlatforms();

        //Adds Platform ID and name to table and returns if it worked
        PlatformID PushPlatform(int platformID,string name);
    }
}

