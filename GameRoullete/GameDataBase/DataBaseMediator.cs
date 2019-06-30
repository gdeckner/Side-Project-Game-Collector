using Game_Collector.DAL;
using Game_Collector.Models;
using Game_Collector.Security;
using GameDataBase.DAL.IGDBDAO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameDataBase
{
    public class DataBaseMediator
    {
        public string ReturnMessage { get; set; }
        protected IConfigurationRoot Config
        {
            get
            {
                if (config == null)
                {
                    var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");

                    config = builder.Build();
                }
                return config;
            }
        }
        protected string ConnectionString
        {
            get
            {
                return Config.GetConnectionString("Test");
            }
        }
        private IConfigurationRoot config;
        private const string UserApiKey = "07b6d5257e21d7bbaa02fb68a9a13899";
        private CoversSQLDAO coverSQLDAO;
        private FranchisesSQLDAO franchiseSQLDAO;
        private GameInfoSQLDAO gameInfoSQLDAO;
        private GameRatingSQLDAO gameRatingSQLDAO;
        private GenresSQLDAO genresSQLDAO;
        private PlatformsSQLDAO platformsSQLDAO;
        private UserGameInfoSQLDAO userGameInfoSQLDAO;
        private UserLoginSqlDao userLoginSQLDAO;
        private GameInfoIGDBDAO gameInfoIGDBDAO;
        private GenresIGDBDAO genresIGDBDAO;
        private PlatformsIGDBDAO platformsInfoIGDBDAO;
        private CoversIGDBDAO coversIGDBDAO;
        private FranchisesIGDBDAO franchisesIGDBDAO;
        

        PasswordHasher hash = new PasswordHasher();
        public DataBaseMediator()
        {
            coverSQLDAO = new CoversSQLDAO(ConnectionString);
            franchiseSQLDAO = new FranchisesSQLDAO(ConnectionString);
            gameInfoSQLDAO = new GameInfoSQLDAO(ConnectionString);
            gameRatingSQLDAO = new GameRatingSQLDAO(ConnectionString);
            genresSQLDAO = new GenresSQLDAO(ConnectionString);
            userGameInfoSQLDAO = new UserGameInfoSQLDAO(ConnectionString);
            userLoginSQLDAO = new UserLoginSqlDao(ConnectionString, hash);
            genresIGDBDAO = new GenresIGDBDAO(UserApiKey);
            platformsInfoIGDBDAO = new PlatformsIGDBDAO(UserApiKey);
            gameInfoIGDBDAO = new GameInfoIGDBDAO(UserApiKey, ConnectionString);
            coversIGDBDAO = new CoversIGDBDAO(UserApiKey);
            franchisesIGDBDAO = new FranchisesIGDBDAO(UserApiKey);
        }
        public bool Login(string userName, string password)
        {
            //User attempts to login to SQL and returns if it was succesfull or not
            bool validLogin = userLoginSQLDAO.CheckLogin(userName, password);

            return validLogin;
        }
        public bool ChangePassword(string userName,string oldpassword,string newPassword1,string newPassword2)
        {
            bool changedPassword;
            //Verifies that the account and old password are valid
            if(userLoginSQLDAO.CheckLogin(userName,oldpassword))
            {
                //Verifies that password matches and meets criteria 
                if(ValidPassword(newPassword1,newPassword2))
                {
                    changedPassword = true;
                    //Once verified it will change the password
                    userLoginSQLDAO.ChangeLoginPassword(userName, newPassword1);
                }
                else
                {
                    changedPassword = false;
                }
            }
            else
            {
                changedPassword = false;
            }

            return changedPassword;
        }
        public bool ValidPassword(string password1,string password2)
        {
            //Verifies password match and meet criteria of at least 8 in length, has one upper, contains number and special character
            bool validPassword = (password1.Equals(password2) && password1.Length >= 8 && password1.Any( x=> char.IsUpper(x)))
            && password1.Any(x=>char.IsNumber(x)) && password1.Any(x=> ! char.IsLetterOrDigit(x));

            return validPassword;
        }
        public string CreateLogin(string userName,string password1,string password2)
        {
            string result;
            //Makes sure user name does not exist already
            if(userLoginSQLDAO.CheckIfUserNameExists(userName))
            {
                result = "Username already exists, please try a different one";
            }
            else
            {
                //Makes sure password matches and meets criteria 
                if(ValidPassword(password1,password2))
                {
                    userLoginSQLDAO.CreateLogin(userName, password1);
                    result = "Account was succesfully created!";
                }
                else
                {
                    result = "Passwords either do not match or do not fit the criteria, Passwords must be 8 in length, contain one number,one uppercase" +
                        "and one special character";
                }
            }

            return result;
        }
        public IList<GameInfo> GetGames(string name, bool gameIsListed = true)
        {
            //Returns the number of results found in SQL DB
            int checkGame = gameInfoSQLDAO.CheckGameInfo(name);
            IList<GameInfo> getGameList = new List<GameInfo>();
            //if the games that are listed are not what the user is looking for or if the SQL DB does not return anything, it will then pull from IGDB
            if (gameIsListed = true && checkGame != 0)
            {
                getGameList = gameInfoSQLDAO.PullMuliGameInfo(name);
            }
            else
            {
                //Gets game list from IGDB and adds any missing games to SQL DB
                getGameList = gameInfoIGDBDAO.PullMuliGameInfo(name);
                PushCoversIntoSQL(getGameList);
                PushFranchiseIntoSQL(getGameList);
                PushGameInfoToSQL(getGameList);
            }


            return getGameList;
        }
        public void PushCoversIntoSQL(IList<GameInfo> games)
        {
            //Takes the results from IGDB query and inserts any missing covers into SQL DB
            foreach (GameInfo x in games)
            {
                if (!coverSQLDAO.CheckCoverValid(x.coverID))
                {
                    Covers newCover = new Covers();
                    newCover = coversIGDBDAO.PullCover(x.coverID);
                    coverSQLDAO.PushCover(newCover.cover_ID, newCover.cover_Url);
                }

            }
        }
        public void PushFranchiseIntoSQL(IList<GameInfo> games)
        {
            //Takes the results from IGDB query and inserts any missing franchises into SQL DB
            foreach (GameInfo x in games)
            {
                if (!franchiseSQLDAO.CheckFranchiseID(x.franchiseID))
                {
                    Franchises newFranchise = new Franchises();
                    newFranchise = franchisesIGDBDAO.PullSpecificFranchise(x.franchiseID);
                    franchiseSQLDAO.PushFranchise(newFranchise.franchise_Id, newFranchise.franchise_Name);
                }

            }

        }
        public void PushGameInfoToSQL(IList<GameInfo> games)
        {
            //Takes the results from IGDB query and inserts any missing games into SQL DB
            foreach (GameInfo x in games)
            {
                gameInfoSQLDAO.PushGameInfo(x.game_ID, x.gameName, x.gameDescription, x.genreID, x.platformID, x.franchiseID, x.coverID, x.ratingId);
            }
        }
        


    }
}

