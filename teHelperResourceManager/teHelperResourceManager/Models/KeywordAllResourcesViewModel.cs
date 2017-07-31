using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace teHelperResourceManager.Models
{
    public class KeywordAllResourcesViewModel
    {
        public List<Resource> AllResources { get; set; }
        public Keywords Keyword { get; set; }
    }
}