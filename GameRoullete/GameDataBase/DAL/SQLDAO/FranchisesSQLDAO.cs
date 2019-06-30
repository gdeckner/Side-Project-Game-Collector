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
    public class FranchisesSQLDAO : IFranchisesDAO
    {
        private string connectionString;

        public FranchisesSQLDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public void PushFranchise(int franchiseID, string name)
        {
            //Pushes the parameters into the SQL table Franchises
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"insert into Franchises (franchise_id,franchise_name) values(@franchiseId,@franchiseName)";
                cmd.Parameters.AddWithValue("@franchiseId", franchiseID);
                cmd.Parameters.AddWithValue("@franchiseName", name);
                cmd.ExecuteNonQuery();
            }
        }

        public bool CheckFranchiseID(int franchiseID)
        {
            //Verifies if franchise exists in SQL database
            bool isValdidFranchise = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"select franchise_id,franchise_name from Franchises
                where franchise_id = @franchiseId";
                cmd.Parameters.AddWithValue("@franchiseId", franchiseID);
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    isValdidFranchise = true;
                }
            }
            return isValdidFranchise;
        }

        public Franchises PullSpecificFranchise(int franchiseID)
        {
            //Pulls specific Franchise based on the id from the DB
            Franchises pulledFranchise = new Franchises();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"select franchise_id,franchise_name from Franchises
                where franchise_id = @franchiseId";
                cmd.Parameters.AddWithValue("@franchiseId", franchiseID);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    pulledFranchise.franchise_Id = (int)reader["franchise_id"];
                    pulledFranchise.franchise_Name = (string)reader["franchise_name"];
                }
            }
            return pulledFranchise;
        }
    }
}
