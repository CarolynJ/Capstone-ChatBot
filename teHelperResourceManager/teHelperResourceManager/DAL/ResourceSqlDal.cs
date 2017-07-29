using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using teHelperResourceManager.Models;

namespace teHelperResourceManager.DAL
{
    public class ResourceSqlDal : IResourceSource
    {
        private const string SQL_AllGetAlphabeticalResources = "SELECT * FROM Resource ORDER BY ResourceTitle ASC;";
        private string connectionString;

        public ResourceSqlDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Resource> GetAllResources()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    List<Resource> allResources = conn.Query<Resource>(SQL_AllGetAlphabeticalResources).ToList();

                    return allResources;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}