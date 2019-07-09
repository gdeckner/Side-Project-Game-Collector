using System;
using Game_Collector.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_Collector.DAL.Interfaces;

namespace Game_Collector.DAL
{
    public class CoversSQLDAO : ICoversDAO 
    {
        
        private string connectionString;
        

        public CoversSQLDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public bool CheckCoverValid(int coverId)
        {
            //Creates SQL connection and connects to our DB, and returns if it exists or not
            bool isValidCover = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText= @"select cover_url,cover_id from covers
                 where cover_id = @coverId";
                cmd.Parameters.AddWithValue("@coverId", coverId);
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    isValidCover = true;
                }
            }
            return isValidCover;
        }

        public Covers PullCover(int coverId)
        {
            //Creates SQL connection and connects to our DB, and returns the cover url based on the cover id
            Covers pulledCover = new Covers();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"select cover_url,cover_id from covers
                where cover_id = @coverId";
                cmd.Parameters.AddWithValue("@coverId", coverId);
                SqlDataReader reader = cmd.ExecuteReader();
               while(reader.Read())
                {
                    pulledCover.Cover_ID = (int)reader["cover_id"];
                    pulledCover.Cover_Url = (string)reader["cover_url"];
                }
                
            }
            return pulledCover;
        }

        public void PushCover(int coverId, string url)
        {
            //Creates SQL connection and updates our cover table based on the values being pushed
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"insert into covers (cover_id,cover_url) values(@coverId,@coverurl)";
                cmd.Parameters.AddWithValue("@coverId", coverId);
                cmd.Parameters.AddWithValue("@coverurl", url);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
