using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentChatBot.Models;
using System.Data.SqlClient;
using Dapper;

namespace StudentChatBot.DAL
{
    public class MotivationSQLDAL : IMotivationDAL
    {
        private string connectionString;
        private const string SQL_GetAllMotivations = "Select * from motivation;";

        public MotivationSQLDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Motivation> GetAllMotivations()
        {
            List<Motivation> allMotivations = new List<Motivation>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    allMotivations = conn.Query<Motivation>(SQL_GetAllMotivations).ToList();
                }

                return allMotivations;
            }

            catch (SqlException ex)
            {
                throw;
            }
        }

    }
}

