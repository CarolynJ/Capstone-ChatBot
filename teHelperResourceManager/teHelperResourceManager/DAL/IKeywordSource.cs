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
        bool AddKeywordsToOneResource(List<Keywords> kw, Resource r);
        bool DeleteKeywordFromResource(Keywords kw, Resource r);
        Keywords GetSingleKeyword(string kw);
        Keywords GetSingleKeyword(int id);
        bool UpdateKeywordsToOneResource(List<Keywords> newKeywords, Resource r);
    }
}
