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
        private const string SQL_GetAllAlphabeticalKeywords = "SELECT * FROM Keyword ORDER BY Keyword ASC;";
        private const string SQL_SaveNewKeyword = "INSERT INTO Keyword VALUES (@keywordName);";
        private string connectionString;

        public KeywordSqlDal(string connectionString)
        {
            this.connectionString = connectionString;
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

        public void SaveNewKeyword(Keywords newKeyword)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    conn.Execute(SQL_SaveNewKeyword, new { keywordName = newKeyword.Keyword });
                }
            }
            catch
            {
                throw;
            }
        }
    }
}