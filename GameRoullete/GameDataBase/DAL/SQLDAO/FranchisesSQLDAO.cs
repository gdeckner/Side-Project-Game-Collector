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
        public IList<Franchises> pulledFranchises = new List<Franchises>();

        public FranchisesSQLDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public bool CheckFranchiseID(int franchiseID)
        {
            bool result = false;
            foreach (Franchises x in pulledFranchises)
            {
                if (x.franchise_Id == franchiseID)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public Franchises InsertFranchises(int franchiseID, string name)
        {
            Franchises franchise = new Franchises
            {
                franchise_Id = franchiseID,
                franchise_Name = name
            };
            pulledFranchises.Add(franchise);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "insert into Franchises (franchise_Id,franchise_Name) values(@franchId,@franchName)";
                cmd.Parameters.AddWithValue("@franchId", franchiseID);
                cmd.Parameters.AddWithValue("@franchName", name);

                cmd.ExecuteNonQuery();
            }


            return franchise;
        }

        public IList<Franchises> PullFranchise()
        {
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from Franchises";

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pulledFranchises.Add(new Franchises
                    {
                        franchise_Id = (int)reader["franchise_Id"],
                        franchise_Name = (string)reader["franchise_Name"]

                    });
                }
            }
            return pulledFranchises;
            
        }

        public Franchises PullSpecificFranchise(int franchiseID)
        {
           for(int i = 0;i<pulledFranchises.Count;i++)
            {
                if(pulledFranchises[i].franchise_Id == franchiseID)
                {
                    return pulledFranchises[i];
                }
            }
            return null;
        }
    }
}
