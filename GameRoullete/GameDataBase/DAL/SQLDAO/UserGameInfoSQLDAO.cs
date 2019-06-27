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
    public class UserGameInfoSQLDAO : IUserGameInfoDAO
    {
        private string connectionString;
        public UserGameInfoSQLDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public bool CheckIfValid(string userId)
        {
            bool result = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = connection.CreateCommand();
                connection.Open();
                cmd.CommandText = @"select userName from UserGameInfo where userName = @userID";
                cmd.Parameters.AddWithValue("@userID", userId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    result = true;
                }
            }

            return result;
        }

        public UserGameInfo PullSingleUserGameInfo(string userId, int gameId)
        {
            UserGameInfo pulledUserGame = new UserGameInfo();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = connection.CreateCommand();
                connection.Open();
                cmd.CommandText = @"select * from UserGameInfo where userName = @userID and game_id = @gameId";
                cmd.Parameters.AddWithValue("@userID", userId);
                cmd.Parameters.AddWithValue("@gameId", gameId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pulledUserGame.entry_Id = (int)reader["EntryId"];
                    pulledUserGame.game_Id = (int)reader["game_id"];
                    pulledUserGame.game_isOwned = (bool)reader["owned"];
                    pulledUserGame.game_onWish = (bool)reader["wishlist"];
                    pulledUserGame.game_Progress = (int)reader["progress"];
                    pulledUserGame.user_name = userId;
                }
            }
            return pulledUserGame;
        }

        public IList<UserGameInfo> PullUserGameInfo(string userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = connection.CreateCommand();
                connection.Open();
                cmd.CommandText = @"Select * from UserGameInfo where userName = @userID";
                cmd.Parameters.AddWithValue("@userID", userId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    UserGameInfo newGameInfo = new UserGameInfo
                    {
                        user_name = userId,
                        game_Id = (int)reader["game_Id"],
                        game_isOwned = (bool)reader["game_isOwned"],
                        game_onWish = (bool)reader["game_onWish"],
                        game_Progress = Convert.ToDouble(reader["game_Progress"]),
                        entry_Id = (int)reader["entry_Id"]
                    };
                }
            }
            return null;
        }

        public void PushUserGameInfo(string userId, int gameId, int progress, bool owned, bool wish)
        {
            int convertToBit;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = connection.CreateCommand();
                connection.Open();
                cmd.CommandText = @"insert into UserGameInfo (game_Id,userName,progress,wishlist,owned) 
                values (@game_Id,@userId,@progress,@wishlist,@owned)";
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@game_Id", gameId);
                convertToBit = (owned) ? 1 : 0;
                cmd.Parameters.AddWithValue("@owned", convertToBit);
                convertToBit = (wish) ? 1 : 0;
                cmd.Parameters.AddWithValue("@wishlist", convertToBit);
                cmd.Parameters.AddWithValue("@progress", progress);

                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateUserGame(int gameId, bool isOwnedValue, bool wish, string userId, int progress)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = connection.CreateCommand();
                connection.Open();
                cmd.CommandText = @"insert into UserGameInfo (game_Id,userName,owned,wishlist,progress) values (@game_Id,@userId,@gameOwned,@gameWish,@progress)";
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@game_Id", gameId);
                cmd.Parameters.AddWithValue("@gameOwned", isOwnedValue);
                cmd.Parameters.AddWithValue("@gameWish", wish);
                cmd.Parameters.AddWithValue("@progress", progress);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
