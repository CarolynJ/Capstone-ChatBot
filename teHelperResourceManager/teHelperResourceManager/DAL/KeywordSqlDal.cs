using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using teHelperResourceManager.Models;

namespace teHelperResourceManager.DAL
{
    public class KeywordSqlDal : IKeywordSource
    {
        private const string SQL_GetAllAlphabeticalKeywords = "SELECT * FROM Keywords ORDER BY Keyword ASC;";
        private const string SQL_SaveNewKeyword = "INSERT INTO Keywords VALUES (@keywordName);";
        private const string SQL_FindAllExistingKeywordMatches = "SELECT * FROM Keywords WHERE Keyword = @checkKeyword;";
        private const string SQL_FindAllExistingKeywordMatchesById = "SELECT * FROM Keywords WHERE KeywordId = @kwId;";
        private const string SQL_DeleteResourceAndKeywordCombo = "DELETE FROM Keyword_Resource WHERE ResourceId = @resourceId AND KeywordId = @keywordId;";
        private const string SQL_AddKeywordToResource = "INSERT INTO Resource_Keyword VALUES (@rId, @kId);";
        private const string SQL_GetAllKeywordsForAResource = "SELECT Keywords.* FROM Resource_Keyword INNER JOIN Resources on Resource_Keyword.ResourceId = Resources.ResourceId INNER JOIN Keywords on Resource_Keyword.KeywordId = Keywords.KeywordId WHERE Resources.ResourceId = @resourceId;";
        private const string SQL_DeleteAllReferencesToKeywordResourcePair = "DELETE FROM Resource_Keyword WHERE ResourceId = @resourceId;";
        private string connectionString;

        public KeywordSqlDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool AddKeywordsToOneResource(List<Keywords> kw, Resource r)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    int rowsAffected = 0;

                    foreach (Keywords k in kw)
                    {
                        int oneSuccessfulRow = conn.Execute(SQL_AddKeywordToResource, new { rId = r.ResourceId, kId = k.KeywordId });

                        if (oneSuccessfulRow > 0)
                        {
                            rowsAffected++;
                        }
                    }
                    
                    if (rowsAffected == kw.Count)
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

        public bool DeleteKeywordFromResource(Keywords kw, Resource r)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    int rowsAffected = conn.Execute(SQL_DeleteResourceAndKeywordCombo, new { resourceId = r.ResourceId, keywordId = kw.KeywordId });

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

        public List<Keywords> GetAllKeywords()
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    List<Keywords> allKeywords = conn.Query<Keywords>(SQL_GetAllAlphabeticalKeywords).ToList();

                    return allKeywords;
                }
            }
            catch
            {
                throw;
            }
        }

        public List<Keywords> GetAllKeywordsForAResource(Resource r)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    List<Keywords> allKeywordsForAResource = conn.Query<Keywords>(SQL_GetAllKeywordsForAResource, new { resourceId = r.ResourceId }).ToList();

                    return allKeywordsForAResource;
                }
            }
            catch
            {
                throw;
            }
        }

        public Keywords GetSingleKeyword(string kw)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    Keywords key = conn.Query<Keywords>(SQL_FindAllExistingKeywordMatches, new { checkKeyword = kw }).FirstOrDefault();

                    return key;
                }
            }
            catch
            {
                throw;
            }
        }

        public Keywords GetSingleKeyword(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    Keywords key = conn.Query<Keywords>(SQL_FindAllExistingKeywordMatchesById, new { kwId = id }).FirstOrDefault();

                    return key;
                }
            }
            catch
            {
                throw;
            }
        }

        public bool SaveNewKeyword(Keywords newKeyword)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    bool keywordAlreadyExists = (conn.Query(SQL_FindAllExistingKeywordMatches, new { checkKeyword = newKeyword.Keyword }).ToList().Count > 0);

                    if (!keywordAlreadyExists)
                    {
                        int rowsAffected = conn.Execute(SQL_SaveNewKeyword, new { keywordName = newKeyword.Keyword });

                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                    }
                    
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateKeywordsToOneResource(List<Keywords> newKeywords, Resource r)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // delete all Resource_Keyword references to that resource first, and we'll add them back in because we don't care about IDs for this db
                    conn.Execute(SQL_DeleteAllReferencesToKeywordResourcePair, new { resourceId = r.ResourceId });
                    
                    int rowsAffected = 0;

                    foreach (Keywords k in newKeywords)
                    {
                        int oneSuccessfulRow = conn.Execute(SQL_AddKeywordToResource, new { rId = r.ResourceId, kId = k.KeywordId });

                        if (oneSuccessfulRow > 0)
                        {
                            rowsAffected++;
                        }
                    }

                    if (rowsAffected == newKeywords.Count)
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