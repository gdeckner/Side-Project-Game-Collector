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
    public class GenresSQLDAO : IGenresDAO
    {
        private string connectionString;

        public GenresSQLDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public bool CheckGenreID(int genreID)
        {
            bool isValidGenre = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"select genre_id,genre_name from Genres
                 where genre_id = @genreId";
                cmd.Parameters.AddWithValue("@genreId", genreID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    isValidGenre = true;
                }
            }
            return isValidGenre;
        }

        public Genres PullSpecificGenre(int genreID)
        {
            Genres pulledGenre = new Genres();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"select * from Genres
                where genre_id = @genreId";
                cmd.Parameters.AddWithValue("@genreId", genreID);
                SqlDataReader reader = cmd.ExecuteReader();
                 while(reader.Read())
                {
                    pulledGenre.genre_iD = (int)reader["genre_id"];
                    pulledGenre.genre_Name = (string)reader["genre_name"];
                }
            }
            return pulledGenre;
        }

        public void PushGenre(int genreId, string genreName)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
                {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"insert into Genres (genre_id,genre_name)
                values(@genreId,@genreName)";
                cmd.Parameters.AddWithValue("@genreId", genreId);
                cmd.Parameters.AddWithValue("@genreName", genreName);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
