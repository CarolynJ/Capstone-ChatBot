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
    }
}