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
    public class GameRatingSQLDAO : IGameRatingDAO
    {
        private string connectionString;
        public GameRatingSQLDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public GameRating PullGameRating(int rating_Id)
        {
            GameRating pulledGameRating = new GameRating();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"select * from Ratings 
                where rating_id = @ratingId";
                cmd.Parameters.AddWithValue("@ratingId", rating_Id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pulledGameRating.game_Hype = (int)reader["hype"];
                    pulledGameRating.game_popularity = (int)reader["popularity"];
                    pulledGameRating.rating_Id = rating_Id;
                    pulledGameRating.game_Total_Rating = (int)reader["rating"];
                    pulledGameRating.game_Total_Rating_Count = (int)reader["rating_count"];
                }

            }
            return pulledGameRating;
        }

        public int PushGameRating(int gameHype, int gamePopularity, int gameRatingCount, int gameTotalRating)
        {
            int ratingId;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @" insert into Ratings (hype,popularity,rating,rating_count)
                values(@hype,@popularity,@rating,@rating_count)
                select scope_identity()";
                cmd.Parameters.AddWithValue("@hype", gameHype);
                cmd.Parameters.AddWithValue("@popularity", gamePopularity);
                cmd.Parameters.AddWithValue("@rating", gameTotalRating);
                cmd.Parameters.AddWithValue("@rating_count", gameRatingCount);
                ratingId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return ratingId;
        }
    }
}
