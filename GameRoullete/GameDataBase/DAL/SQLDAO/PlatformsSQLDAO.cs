using System;
using Game_Collector.Models;
using Game_Collector.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

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
            bool result = false;
            foreach (Platforms x in pulledPlatforms)
            {
                if (x.platform_Id == platformID)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public IList<Platforms> PullAllPlatforms()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from Platforms";

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pulledPlatforms.Add(new Platforms
                    {
                        platform_Id = (int)reader["platform_Id"],
                        platform_Name = (string)reader["platform_Name"]

                    });
                }
            }
            return pulledPlatforms;
        }

        public Platforms PullSpecificPlatform(int platformID)
        {
            Platforms platform = new Platforms();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Platforms where platform_Id = @platformId";
                cmd.Parameters.AddWithValue("@platformId", platformID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    platform.platform_Id = platformID;
                    platform.platform_Name = (string)reader["platform_Name"];
                    pulledPlatforms.Add(platform);
                }
            }
            return platform;
        }

        public Platforms PushPlatform(int platformID, string name)
        {
            Platforms platform = new Platforms
            {
                platform_Id = platformID,
                platform_Name = name
            };
            pulledPlatforms.Add(platform);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "insert into Platforms (platform_Id,platform_Name) values(@platformId,@platformName)";
                cmd.Parameters.AddWithValue("@platformId", platformID);
                cmd.Parameters.AddWithValue("@platformName", name);

                cmd.ExecuteNonQuery();
            }


            return platform;
        }
    }
}
