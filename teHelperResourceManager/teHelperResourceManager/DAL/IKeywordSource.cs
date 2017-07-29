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
        List<Keyword> GetAllKeywords();
    }
}
