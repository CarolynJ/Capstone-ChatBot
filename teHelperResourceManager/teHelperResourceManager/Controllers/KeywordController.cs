using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace teHelperResourceManager.Controllers
{
    public class KeywordController : Controller
    {
        // GET: Keyword
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}