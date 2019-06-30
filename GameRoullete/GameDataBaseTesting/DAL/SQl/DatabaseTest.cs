using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;
using Game_Collector.DAL;
using Game_Collector.Models;

namespace GameDataBase.test.DAL
{
    [TestClass]
    public abstract class DatabaseTest
    {
        private IConfigurationRoot config;
        private TransactionScope transaction;
        protected IConfigurationRoot Config
        {
            get
            {
                if(config == null)
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
        [TestInitialize]
        public virtual void Setup()
        {
            
            transaction = new TransactionScope();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"delete from UserGameInfo
                                    delete from UserInfo
                                    delete from Games
                                    delete from Ratings
                                    delete from Covers
                                    delete from Platforms
                                    delete from Genres
                                    delete from Franchises";
                
                conn.Open();
                cmd.ExecuteNonQuery();

            }


        }
        [TestCleanup]
        public void Cleanup()
        {
            transaction.Dispose();
        }
        
    }
}
