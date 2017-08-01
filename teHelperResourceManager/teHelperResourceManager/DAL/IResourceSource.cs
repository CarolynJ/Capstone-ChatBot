using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using teHelperResourceManager.Models;

namespace teHelperResourceManager.DAL
{
    public interface IResourceSource
    {
        List<Resource> GetAllResources();
        //bool FindMatchingLinksToResources(string resourceLink);
        bool AddNewResource(Resource newResource);
        List<Resource> GetAllResourcesForAKeyword(Keywords kw);
        Resource GetResource(int resourceId);
        Resource GetResource(string resourceName);
        bool UpdateExistingResource(Resource r);
        bool DeleteResource(int id);
    }
}