using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using teHelperResourceManager.Models;

namespace teHelperResourceManager.DAL
{
    public interface IKeywordSource
    {
        List<Keywords> GetAllKeywords();
        List<Keywords> GetAllKeywordsForAResource(Resource r);
        bool SaveNewKeyword(Keywords newKeyword);
        bool DoesKeywordAlreadyExist(string checkKeyword);
        bool AddKeywordsToOneResource(List<Keywords> kw, Resource r);
        bool DeleteKeywordFromResource(Keywords kw, Resource r);
    }
}
