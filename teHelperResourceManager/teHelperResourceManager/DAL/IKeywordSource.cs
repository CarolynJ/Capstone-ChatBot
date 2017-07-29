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
        bool SaveNewKeyword(Keywords newKeyword);
        bool DoesKeywordAlreadyExist(string checkKeyword);
    }
}
