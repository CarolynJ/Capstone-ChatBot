using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teHelperResourceManager.DAL;
using teHelperResourceManager.Models;

namespace teHelperResourceManager.Controllers
{
    public class KeywordController : Controller
    {
        IKeywordSource keywordDal;
        IResourceSource resourceDal;

        public KeywordController(IKeywordSource keywordDal, IResourceSource resourceDal)
        {
            this.keywordDal = keywordDal;
            this.resourceDal = resourceDal;
        }
        
        // GET: Keyword
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
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
    }
}