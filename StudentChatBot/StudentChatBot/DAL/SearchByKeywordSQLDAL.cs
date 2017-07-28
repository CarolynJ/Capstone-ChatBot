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
        private const string SQL_GetResource = "Select * from Resources inner join Resource_Keyword on Resources.ResourceID = Resource_Keyword.ResourceID inner join Keywords on Keywords.KeywordId = Resource_Keyword.KeywordId where Keywords.Keyword = @keyword;";

        public SearchByKeywordSQLDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Resource GetResource(string keyword)
        {
            Resource r = new Resource();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    r = conn.QueryFirstOrDefault<Resource>(SQL_GetResource, new { @keyword = keyword });
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
