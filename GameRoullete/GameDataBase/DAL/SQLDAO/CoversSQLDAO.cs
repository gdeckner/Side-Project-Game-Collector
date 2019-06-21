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
        public IList<Covers> pulledCovers = new List<Covers>();
        private string connectionString;
        

        public CoversSQLDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public bool CheckCoverValid(int coverId)
        {
            bool result = false;
            foreach(Covers x in pulledCovers)
            {
                if (x.cover_ID == coverId)
                {
                    result = true;
                    break;
                }
            }
                
            return result;
        }

        public void PushCover(int coverId, string url)
        {
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "insert into Covers (cover_Id,cover_Url) values(@coverID,@coverUrl)";
                cmd.Parameters.AddWithValue("@coverID", coverId);
                cmd.Parameters.AddWithValue("@coverUrl", url);

                cmd.ExecuteNonQuery();
            }


                
        }

        public Covers PullCover(int coverId)
        {
            Covers cover = new Covers();
            foreach(Covers x in pulledCovers)
            {
                if(x.cover_ID == coverId)
                {
                    cover.cover_ID = coverId;
                    cover.cover_Url = x.cover_Url;

                }
            }

            return cover;
            
        }

        public IList<Covers> PullAllCovers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select cover_Url from Covers where cover_Id = @coverId";
                cmd.Parameters.AddWithValue("@coverId", coverId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cover.cover_ID = coverId;
                    cover.cover_Url = (string)reader["cover_Url"];
                    pulledCovers.Add(cover);
                }
            }
        }
    }
}
