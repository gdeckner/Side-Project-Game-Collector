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
        public PlatformsSQLDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public bool CheckPlatformID(int platformID)
        {
            //Checks if platform exists in SQL DB
            bool isValidPlatform = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"select * from platforms where platform_id = @platId";
                cmd.Parameters.AddWithValue("@platId", platformID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    isValidPlatform = true;
                }
            }
            return isValidPlatform;
        }

        public IList<Platforms> PullAllPlatforms()
        {
            //Pulls all platforms from SQL DB
            IList<Platforms> pulledPlatforms = new List<Platforms>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"select * from Platforms";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Platforms newPlatform = new Platforms
                    {
                        Platform_Id= (int)reader["platform_id"],
                        Platform_Name = (string)reader["platform_name"]
                    };
                    pulledPlatforms.Add(newPlatform);
                }
            }
            return pulledPlatforms;
        }

        public Platforms PullSpecificPlatform(int platformID)
        {
            //Pulls specific platform name based on platform ID from SQL DB
            Platforms pulledPlatform = new Platforms();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"select * from platforms where platform_id = @platId";
                cmd.Parameters.AddWithValue("@platId", platformID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pulledPlatform.Platform_Id = (int)reader["platform_id"];
                    pulledPlatform.Platform_Name = (string)reader["platform_name"];
                }
            }
            return pulledPlatform;
        }

        public void PushPlatform(int platformID, string name)
        {
            //pushs new platform id and name into SQL DB
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"insert into Platforms (platform_id,platform_name)
                values(@platId,@platName)";
                cmd.Parameters.AddWithValue("@platId", platformID);
                cmd.Parameters.AddWithValue("@platName", name);
                cmd.ExecuteNonQuery();
            }
        }
        public IList<Platforms> PullPlatformList(int[] platformArray)
        {
            IList<Platforms> pulledPlatforms = new List<Platforms>();

            foreach (int x in platformArray)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();

                    cmd.CommandText = @"select * from Platforms
                    where platform_id = @platformId";
                    cmd.Parameters.AddWithValue("@platformId", x);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Platforms pulledPlatform = new Platforms
                        {
                            Platform_Id = (int)reader["platform_id"],
                           Platform_Name = (string)reader["platform_name"]
                        };
                        pulledPlatforms.Add(pulledPlatform);
                    }

                }
            }

            return pulledPlatforms;
        }
    }
}
