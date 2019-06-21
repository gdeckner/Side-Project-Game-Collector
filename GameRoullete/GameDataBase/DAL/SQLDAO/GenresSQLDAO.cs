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
        public IList<Genres> pulledGenres = new List<Genres>();

        public GenresSQLDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public bool CheckGenreID(int genreID)
        {
            bool result = false;
            foreach (Genres x in pulledGenres)
            {
                if (x.genre_iD == genreID)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public IList<Genres> PullAllGenres()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from Genres";

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pulledGenres.Add(new Genres
                    {
                        genre_iD = (int)reader["genre_Id"],
                        genre_Name = (string)reader["genre_Name"]

                    });
                }
            }
            return pulledGenres;

        }

        public Genres PullSpecificGenre(int genreID)
        {
            Genres genre = new Genres();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Genres where genre_Id = @genreId";
                cmd.Parameters.AddWithValue("@genreId", genreID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    genre.genre_iD = genreID;
                    genre.genre_Name = (string)reader["genre_Name"];
                    pulledGenres.Add(genre);
                }
            }
            return genre;
        }

        public Genres PushGenre(int genreId, string genreName)
        {
            Genres genre = new Genres
            {
                genre_iD = genreId,
                genre_Name = genreName
            };
            pulledGenres.Add(genre);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "insert into Genres (genre_Id,genre_Name) values(@genreId,@genreName)";
                cmd.Parameters.AddWithValue("@genreId", genreId);
                cmd.Parameters.AddWithValue("@genreName", genreName);

                cmd.ExecuteNonQuery();
            }


            return genre;
        }
    }
}
