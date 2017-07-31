using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentChatBot.Models;
using System.Data.SqlClient;
using Dapper;

namespace StudentChatBot.DAL
{
    public class SearchByKeywordSQLDAL : ISearchByKeyword
    {
        private string connectionString;
        private const string SQL_GetResource = "Select top 5 * from Resources inner join Resource_Keyword on Resources.ResourceID = Resource_Keyword.ResourceID inner join Keywords on Keywords.KeywordId = Resource_Keyword.KeywordId where Keywords.Keyword = @keyword;";

        public SearchByKeywordSQLDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Resource> GetResources(string keyword)
        {
            List<Resource> r = new List<Resource>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    r = conn.Query<Resource>(SQL_GetResource, new { @keyword = keyword }).ToList();
                }

                return r;
            }

            catch (SqlException ex)
            {
                throw;
            }
        }

    }
}
