using System;
using Game_Collector.Models;
using Game_Collector.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Collector.DAL
{
    public class PlatformsSQLDAO : IPlatformsDAO
    {
        private string connectionString;
        public IList<Platforms> pulledPlatforms = new List<Platforms>();

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

        public Platforms PullSpecificPlatform(int platformID)
        {
            throw new NotImplementedException();
        }

        public Platforms PushPlatform(int platformID, string name)
        {
            throw new NotImplementedException();
        }
    }
}
