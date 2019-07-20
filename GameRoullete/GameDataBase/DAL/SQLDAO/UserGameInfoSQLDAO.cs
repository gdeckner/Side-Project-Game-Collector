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
            //Checks if username exists
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
            //Pulls a single game associated with user and the specific game ID
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
                    pulledUserGame.Entry_Id = (int)reader["EntryId"];
                    pulledUserGame.Game_Id = (int)reader["game_id"];
                    pulledUserGame.Game_isOwned = (bool)reader["owned"];
                    pulledUserGame.Game_onWish = (bool)reader["wishlist"];
                    pulledUserGame.Game_Progress = (int)reader["progress"];
                    pulledUserGame.User_name = userId;
                }
            }
            return pulledUserGame;
        }

        public IList<UserGameInfo> PullUserGameInfo(string userId)
        {
            //Pulls all games associated with user
            IList<UserGameInfo> userGameInfos = new List<UserGameInfo>();
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
                        User_name = userId,
                        Game_Id = (int)reader["game_Id"],
                        Game_isOwned = (bool)reader["owned"],
                        Game_onWish = (bool)reader["wishlist"],
                        Game_Progress = Convert.ToDouble(reader["progress"]),
                        Entry_Id = (int)reader["EntryId"]
                    };
                    userGameInfos.Add(newGameInfo);
                }
            }
            return userGameInfos;
        }

        public void PushUserGameInfo(string userId, int gameId, int progress, bool owned, bool wish)
        {
            //Adds a new game to be associated with user
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
            //Modifies game associated with user
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
