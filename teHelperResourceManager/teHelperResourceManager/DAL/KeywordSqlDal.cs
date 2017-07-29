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
        private string connectionString;

        public KeywordSqlDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Keyword> GetAllKeywords()
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    List<Keyword> allKeywords = conn.Query<Keyword>(SQL_GetAllAlphabeticalKeywords).ToList();

                    return allKeywords;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}