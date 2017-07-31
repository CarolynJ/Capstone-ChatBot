using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teHelperResourceManager.DAL;
using teHelperResourceManager.Models;

namespace teHelperResourceManager.Controllers
{
    public class ResourceController : Controller
    {
        IKeywordSource keywordDal;
        IResourceSource resourceDal;

        public ResourceController(IKeywordSource keywordDal, IResourceSource resourceDal)
        {
            this.keywordDal = keywordDal;
            this.resourceDal = resourceDal;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }


        public ActionResult AddResource()
        {
            NewResourceKeywordViewModel model = new NewResourceKeywordViewModel();

            return View("AddResource", model);
        }

        [HttpPost]
        public ActionResult AddResource(NewResourceKeywordViewModel model)
        {
            Resource r = new Resource()
            {
                ResourceContent = model.newResource.ResourceContent,
                ResourceTitle = model.newResource.ResourceTitle,
                PathwayResource = model.newResource.PathwayResource
            };

            List<string> newKeywordStrings = model.keywordsAsString.Split(',').Select(str => str.Trim()).ToList();
            List<Keywords> newKeywords = new List<Keywords>();

            foreach (string kw in newKeywordStrings)
            {
                if (keywordDal.DoesKeywordAlreadyExist(kw) == null)
                {
                    keywordDal.SaveNewKeyword(new Keywords() { Keyword = kw });
                }

                newKeywords.Add(keywordDal.DoesKeywordAlreadyExist(kw));
            }

            bool successfullyAddedResource = keywordDal.AddKeywordsToOneResource(newKeywords, r);

            if (successfullyAddedResource)
            {
                TempData["NewResource_Success"] = true;
            }

            return RedirectToAction("Index", "Home");

        }
    }
}