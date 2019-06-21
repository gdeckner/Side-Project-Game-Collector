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
        public IList<GameRating> pulledGameRating = new List<GameRating>();

        public GameRatingSQLDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public bool CheckGameRatingID(int gameID)
        {
            bool result = false;
            foreach (GameRating x in pulledGameRating)
            {
                if (x.game_id == gameID)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public GameRating PullGameRating(int gameID)
        {
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from GameRating where game_Id = @gameId";
                cmd.Parameters.AddWithValue("@gameId", gameID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    GameRating gameRatingPull = new GameRating
                    {
                        game_id = gameID,
                        game_popularity = Convert.ToDouble(reader["game_Popularity"]),
                        game_Total_Rating = Convert.ToDouble(reader["game_Total_Rating"]),
                        game_Hype = Convert.ToDouble(reader["game_Popularity"]),
                        game_Total_Rating_Count = Convert.ToDouble(reader["game_Total_Rating_Count"])
                    };
                     pulledGameRating.Add(gameRatingPull);
                     return gameRatingPull; 
                }
            }
            return null;
            
        }

        public GameRating PushGameRating(int gameID, double gameHype, double gamePopularity, double gameRatingCount, double gameTotalRating)
        {
            GameRating pushedRating = new GameRating
            {
                game_id = gameID,
                game_Hype = gameHype,
                game_popularity = gamePopularity,
                game_Total_Rating = gameTotalRating,
                game_Total_Rating_Count = gameRatingCount
            };
            pulledGameRating.Add(pushedRating);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "insert into GameRating (game_Id,game_Popularity,game_Total_Rating,game_Total_Rating_Count,game_Hype) values (@gameId,@gamePopularity,@totalRating,@totalRatingCount,@gameHype)";
                cmd.Parameters.AddWithValue("@gameId", gameID);
                cmd.Parameters.AddWithValue("@gamePopularity", gamePopularity);
                cmd.Parameters.AddWithValue("@totalRating", gameTotalRating);
                cmd.Parameters.AddWithValue("@totalRatingCount", gameRatingCount);
                cmd.Parameters.AddWithValue("@gameHype", gameHype);

                cmd.ExecuteNonQuery();
            }


            return pushedRating;
        }
    }
}
