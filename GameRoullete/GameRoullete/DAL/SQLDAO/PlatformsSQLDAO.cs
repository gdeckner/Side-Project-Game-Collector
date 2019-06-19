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
        private string connectionString;

        public PlatformsSQLDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public bool CheckPlatformID(int platformID)
        {
            throw new NotImplementedException();
        }

        public IList<Platforms> PullAllPlatforms()
        {
            throw new NotImplementedException();
        }

        public PlatformID PullSpecificPlatform(int platformID)
        {
            throw new NotImplementedException();
        }

        public PlatformID PushPlatform(int platformID, string name)
        {
            throw new NotImplementedException();
        }
    }
}
