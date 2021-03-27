using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Models;

namespace NotesMarketPlace.Controllers
{
    public class ChangePasswordController : Controller
    {
        readonly NotesMarketPlaceEntities db;
        public ChangePasswordController()
        {
            db = new NotesMarketPlaceEntities();
        }
       
        [HttpGet]
        [Authorize]
        [Route("ChangePassword")]
        public ActionResult ChangePassword()
        {            
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [Route("ChangePassword")]
        public ActionResult ChangePassword(ChangePasswordViewModel changepass)
        {
            Users user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name && x.IsActive == true);

            if (ModelState.IsValid && user != null)
            {
                if(changepass.oldPassword == user.Password)
                {
                    user.Password = changepass.newPassword;
                    db.SaveChanges();
                    ViewBag.status = "sucess";
                }
                else
                {
                    ModelState.AddModelError("oldPassword", "Password dosen't match");
                }
            }
            return View();
        }
    }
}