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
        private const string SQL_GetAllResourcesForAKeyword = "SELECT Resources.* FROM Resources INNER JOIN Resource_Keyword ON Resource_Keyword.ResourceId = Resources.ResourceId WHERE Resource_Keyword.KeywordId = @kwId;";
        private const string SQL_GetResourceById = "SELECT * FROM Resources WHERE ResourceId = @rId;";
        private const string SQL_GetResourceByName = "SELECT * FROM Resources WHERE ResourceTitle = @rName;";
        private const string SQL_UpdateExistingResource = "UPDATE Resources SET ResourceTitle = @resourceTitle, ResourceContent = @resourceContent, PathwayResource = @isPathway WHERE ResourceId = @resourceId;";
        private string connectionString;

        public ResourceSqlDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool AddNewResource(Resource newResource)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
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

        public List<Resource> GetAllResourcesForAKeyword(Keywords kw)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    List<Resource> matchingResources = conn.Query<Resource>(SQL_GetAllResourcesForAKeyword, new { kwId = kw.KeywordId }).ToList();

                    return matchingResources;
                }
            }
            catch
            {
                throw;
            }
        }

        public Resource GetResource(int resourceId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    Resource r = conn.Query<Resource>(SQL_GetResourceById, new { rId = resourceId }).FirstOrDefault();

                    return r;
                }
            }
            catch
            {
                throw;
            }
        }

        public Resource GetResource(string resourceName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    Resource r = conn.Query<Resource>(SQL_GetResourceByName, new { rName = resourceName }).FirstOrDefault();

                    return r;
                }
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateExistingResource(Resource r)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    int rowsAffected = conn.Execute(SQL_UpdateExistingResource, new { resourceTitle = r.ResourceTitle, resourceContent = r.ResourceContent, isPathway = r.PathwayResource, resourceId = r.ResourceId });

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
    }
}