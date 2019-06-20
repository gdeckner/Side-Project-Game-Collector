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
        public IList<GameInfo> pulledGame = new List<GameInfo>();

        public GameInfoSQLDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public bool CheckGameInfo(string gameName)
        {
            bool result = false;
            foreach(GameInfo x in pulledGame)
            {
                if (x.gameName == gameName)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public IList<GameInfo> PullAllGameInfoFromDB()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from GameInfo";
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    pulledGame.Add(new GameInfo
                    {
                        game_ID = (int)reader["game_Id"],
                        gameName = (string)reader["game_Name"],
                        gameDescription = (string)reader["game_Description"],
                        genreID = (int)reader["genre_Id"],
                        platformID = (int)reader["platform_Id"],
                        franchiseID = (int)reader["franchise_Id"],
                        coverID = (int)reader["cover_Id"]
      
                    });
                }
            }
            pulledGame.OrderBy(x => x.gameName);
            return pulledGame;
        }

        public IList<GameInfo> PullGameInfo(string gameName)
        {
           
            IList<GameInfo> gameReturn = new List<GameInfo>();
            for(int i = 0;i<pulledGame.Count;i++)
            {
                bool result = true;
                if(pulledGame[i].gameName.ToLower().Contains(gameName.ToLower()))
                {
                    foreach(GameInfo x in gameReturn)
                    {
                        if(x.game_ID == pulledGame[i].game_ID)
                        {
                            result = false;
                            break;
                        }
                    }
                    if(result)
                    {
                        gameReturn.Add(pulledGame[i]);
                    }
                }
            }
            return gameReturn;
        }

        public GameInfo PushGameInfo(int gameId, string gameName, string gameDescription, int genreID, int platformID,int franchiseId, int gameCover)
        {
            GameInfo game = new GameInfo
            {
                game_ID = gameId,
                gameName = gameName,
                gameDescription = gameDescription,
                genreID = genreID,
                platformID = platformID,
                franchiseID = franchiseId,
                coverID = gameCover

            };
            pulledGame.Add(game);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "insert into GameInfo (game_Id,game_Name,game_Description,genre_Id,platform_Id,franchise_Id,cover_Id) values(@one,@two,@three,@four,@five,@six,@seven)";
                cmd.Parameters.AddWithValue("@one",gameId);
                cmd.Parameters.AddWithValue("@two", gameName);
                cmd.Parameters.AddWithValue("@three",gameDescription);
                cmd.Parameters.AddWithValue("@four",genreID);
                cmd.Parameters.AddWithValue("@five",platformID);
                cmd.Parameters.AddWithValue("@six",franchiseId);
                cmd.Parameters.AddWithValue("@seven",gameCover);

                cmd.ExecuteNonQuery();
            }
            return game;
        }
    }
}
