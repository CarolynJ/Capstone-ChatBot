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
        private const string SQL_AllGetAlphabeticalResources = "SELECT * FROM Resources ORDER BY ResourceTitle ASC;";
        private const string SQL_AddNewResource = "INSERT INTO Resources VALUES (@resourceTitle, @resourceContent, @pathwayResource);";
        private string connectionString;

        public ResourceSqlDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool AddNewResource(Resource newResource)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    int rowsAffected = conn.Execute(SQL_AddNewResource, new
                        {
                            resourceTitle = newResource.ResourceTitle,
                            resourceContent = newResource.ResourceContent,
                            pathwayResource = newResource.PathwayResource
                        });

                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }

        public bool FindMatchingLinksToResources(string resourceLink)
        {
            throw new NotImplementedException();
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