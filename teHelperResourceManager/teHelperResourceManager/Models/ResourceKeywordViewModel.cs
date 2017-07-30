using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace teHelperResourceManager.Models
{
    public class ResourceKeywordViewModel
    {
        public List<Keywords> allKeywords { get; set; }
        public List<Resource> allResources { get; set; }
        public Dictionary<Resource, List<Keywords>> ResourcesAndKeywords { get; set; }
    }
}