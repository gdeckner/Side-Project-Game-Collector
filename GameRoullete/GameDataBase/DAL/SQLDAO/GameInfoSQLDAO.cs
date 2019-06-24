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
    public class GameInfoSQLDAO : IGameInfoDAO
    {
        private string connectionString;
        public GameInfoSQLDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public int CheckGameInfo(string gameName)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"select game_name from Games
                where game_name Like @gameName";
                cmd.Parameters.AddWithValue("@gameName", gameName +"%");
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    result += 1;
                }
            }
            return result;
        }

        public GameInfo PullGameInfo(string gameName)
        {
            GameInfo pulledGame = new GameInfo();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"select top(1) * from Games
                where game_name Like @gameName
                order by game_name";
                cmd.Parameters.AddWithValue("@gameName", gameName + "%");
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    
                    pulledGame.franchiseID = (int)reader["franchise_id"];
                    pulledGame.gameDescription = (string)reader["game_description"];
                    pulledGame.gameName = (string)reader["game_name"];
                    pulledGame.game_ID = (int)reader["game_id"];
                    pulledGame.genreID = (int)reader["genre_id"];
                    pulledGame.platformID = (int)reader["platform_id"];
                    pulledGame.ratingId = (int)reader["rating_id"];
                    pulledGame.coverID = (int)reader["cover_id"];

                }
            }
            return pulledGame;
        }

        public void PushGameInfo(int gameId, string gameName, string gameDescription, int genreID, int platformID, int franchiseId ,int coverId,int ratingId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"insert into Games (game_id,game_name,rating_id,platform_id,cover_id,genre_id,franchise_id,game_description) values(@gameId,@gameName,@ratingId,@platformId,@coverId,@genreId,@franchiseId,@game_description)";
                cmd.Parameters.AddWithValue("@gameId", gameId);
                cmd.Parameters.AddWithValue("@coverId", coverId);
                cmd.Parameters.AddWithValue("@gameName", gameName);
                cmd.Parameters.AddWithValue("@game_description", gameDescription);
                cmd.Parameters.AddWithValue("@genreId", genreID);
                cmd.Parameters.AddWithValue("@platformId", platformID);
                cmd.Parameters.AddWithValue("@franchiseId", franchiseId);
                cmd.Parameters.AddWithValue("@ratingId", ratingId);
                cmd.ExecuteNonQuery();
            }
        }

        public List<GameInfo> PullMuliGameInfo(string gameName)
        {
            List<GameInfo> multiPulledGames = new List<GameInfo>();
            GameInfo pulledGame = new GameInfo();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"select * from Games
                where game_name Like @gameName
                order by game_name";
                cmd.Parameters.AddWithValue("@gameName", gameName +"%");
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pulledGame.coverID = (int)reader["cover_id"];
                    pulledGame.franchiseID = (int)reader["franchise_id"];
                    pulledGame.gameDescription = (string)reader["game_description"];
                    pulledGame.gameName = (string)reader["game_name"];
                    pulledGame.game_ID = (int)reader["game_id"];
                    pulledGame.genreID = (int)reader["genre_id"];
                    pulledGame.platformID = (int)reader["platform_id"];
                    pulledGame.ratingId = (int)reader["rating_id"];
                    multiPulledGames.Add(pulledGame);

                }
            }
            return multiPulledGames;
        }
    }
}
