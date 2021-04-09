using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Models;
using System.IO;

namespace NotesMarketPlace.Controllers
{    
    [RoutePrefix("Home")]
    
    public class HomeController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "Member")]
        public ActionResult Index()
        {
            return View();
        }
    }    
}