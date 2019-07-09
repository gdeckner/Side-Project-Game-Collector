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
            //Checks if genre exists in SQL DB
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

        public IList<Genres> PullAllGenres()
        {
            //Pulls all the genres stored in SQL DB
            IList<Genres> pulledGenres = new List<Genres>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"select * from Genres";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Genres newGenre = new Genres
                    {
                        Genre_iD = (int)reader["genre_id"],
                        Genre_Name = (string)reader["genre_name"]
                    };
                    pulledGenres.Add(newGenre);
                }
            }
            return pulledGenres;
        }

        public Genres PullSpecificGenre(int genreID)
        {
            //Pulls genre name based on ID from SQL DB
            Genres pulledGenre = new Genres();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"select * from Genres
                where genre_id = @genreId";
                cmd.Parameters.AddWithValue("@genreId", genreID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pulledGenre.Genre_iD = (int)reader["genre_id"];
                    pulledGenre.Genre_Name = (string)reader["genre_name"];
                }
            }
            return pulledGenre;
        }

        public void PushGenre(int genreId, string genreName)
        {
            //Pushes a new genre id and name into SQL DB
            using (SqlConnection connection = new SqlConnection(connectionString))
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

        public IList<Genres> PullGenreList(int[] genreArray)
        {
            IList<Genres> pulledGenres = new List<Genres>();

            foreach (int x in genreArray)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();

                    cmd.CommandText = @"select * from Genres
                    where genre_id = @genreId";
                    cmd.Parameters.AddWithValue("@genreId", x);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Genres pulledGenre = new Genres
                        {
                            Genre_iD = (int)reader["genre_id"],
                            Genre_Name = (string)reader["genre_name"]
                        };
                        pulledGenres.Add(pulledGenre);
                    }

                }
            }


            return pulledGenres;
        }
    }
}
