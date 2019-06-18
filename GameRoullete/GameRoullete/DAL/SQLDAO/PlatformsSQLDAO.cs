using System;
using Game_Collector.Models;
using Game_Collector.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Collector.DAL
{
    class PlatformsSQLDAO : IPlatformsDAO
    {
        public bool CheckPlatformID(int platformID)
        {
            throw new NotImplementedException();
        }

        public IList<Platforms> PullAllPlatforms()
        {
            throw new NotImplementedException();
        }

        public IList<Platforms> PullSpecificPlatform(int platformID)
        {
            throw new NotImplementedException();
        }

        public bool PushPlatform(int platformID, string name)
        {
            throw new NotImplementedException();
        }
    }
}
