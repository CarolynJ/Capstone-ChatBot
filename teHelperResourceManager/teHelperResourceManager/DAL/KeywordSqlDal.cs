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
        private string connectionString;

        public KeywordSqlDal(string connectionString)
        {
            this.connectionString = connectionString;
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