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

        public Platforms PullSpecificPlatform(int platformID)
        {
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
                    pulledPlatform.platform_Id = (int)reader["platform_id"];
                    pulledPlatform.platform_Name = (string)reader["platform_name"];
                }
            }
            return pulledPlatform;
        }

        public void PushPlatform(int platformID, string name)
        {
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
    }
}
