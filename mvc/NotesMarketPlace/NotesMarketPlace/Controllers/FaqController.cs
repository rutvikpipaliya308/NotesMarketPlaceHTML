using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotesMarketPlace.Controllers
{
    public class FaqController : Controller
    {
        // GET: Faq/Faq
        [HttpGet]
        [Route("Faq")]
        [OutputCache(Duration = 0)]
        public ActionResult Faq()
        {
            ViewBag.navClass = "white-nav";
            ViewBag.Faq = "active";
            return View();
        }
    }
}