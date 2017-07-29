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
        private const string SQL_DeleteResourceAndKeywordCombo = "DELETE FROM Keyword_Resource WHERE ResourceId = @resourceId AND KeywordId = @keywordId;";
        private const string SQL_AddKeywordToResource = "INSERT INTO Resource_Keyword VALUES(@ResourceId, @KeywordId);";
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
                        int oneSuccessfulRow = conn.Execute(SQL_AddKeywordToResource, new { ResourceId = r.ResourceId, KeywordId = k.KeywordId });

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

        public bool DoesKeywordAlreadyExist(string checkKeyword)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    int rowsReturned = conn.Query<Keywords>(SQL_FindAllExistingKeywordMatches, new { checkKeyword = checkKeyword }).ToList().Count;

                    if (rowsReturned > 0)
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

        public bool SaveNewKeyword(Keywords newKeyword)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    int rowsAffected = conn.Execute(SQL_SaveNewKeyword, new { keywordName = newKeyword.Keyword });

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