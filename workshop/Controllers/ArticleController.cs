using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace workshop.Controllers
{
    public class ArticleController : Controller
    {
        public ActionResult Hot()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Details(int? id )
        {
            ViewBag.Test = id.HasValue ? id : 0;
            return View();
        }
    }
}