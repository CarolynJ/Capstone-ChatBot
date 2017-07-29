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
    }
}