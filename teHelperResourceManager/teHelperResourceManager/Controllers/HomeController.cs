using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teHelperResourceManager.DAL;
using teHelperResourceManager.Models;

namespace teHelperResourceManager.Controllers
{
    public class HomeController : Controller
    {
        IKeywordSource keywordDal;
        IResourceSource resourceDal;
        
        public HomeController(IKeywordSource keywordDal, IResourceSource resourceDal)
        {
            this.keywordDal = keywordDal;
            this.resourceDal = resourceDal;
        }

        // GET: Home
        public ActionResult Index()
        {
            ResourceKeywordViewModel model = new ResourceKeywordViewModel()
            {
                allKeywords = keywordDal.GetAllKeywords(),
                allResources = resourceDal.GetAllResources(),
                ResourcesAndKeywords = new Dictionary<Resource, List<Keywords>>()
            };

            foreach(Resource r in model.allResources)
            {
                model.ResourcesAndKeywords.Add(r, keywordDal.GetAllKeywordsForAResource(r));
            }

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Index(ResourceKeywordViewModel resourceKeywordLink)
        {
            return RedirectToAction("Index");
        }

        public ActionResult AddKeyword()
        {
            Keywords model = new Keywords();

            return View("AddKeyword", model);
        }

        [HttpPost]
        public ActionResult AddKeyword(Keywords newKeyword)
        {
            if (newKeyword == null)
            {
                return View("AddKeyword", newKeyword);
            }

            bool successfullyAddedKeyword = keywordDal.SaveNewKeyword(newKeyword);

            if (successfullyAddedKeyword)
            {
                TempData["NewKeyword_Success"] = true;
            }

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

            foreach(string kw in newKeywordStrings)
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