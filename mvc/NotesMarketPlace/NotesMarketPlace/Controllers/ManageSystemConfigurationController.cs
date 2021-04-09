using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Models;

namespace NotesMarketPlace.Controllers
{
    [RoutePrefix("SuperAdmin")]
    public class ManageSystemConfigurationController : Controller
    {
        readonly NotesMarketPlaceEntities db;
        public ManageSystemConfigurationController()
        {
            db = new NotesMarketPlaceEntities();
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        [OutputCache(Duration = 0)]
        public ActionResult SystemConfiguration()
        {
            ViewBag.managesystem = "active";

            SystemConfigViewModel systemModel = new SystemConfigViewModel();
            var data = db.SystemConfigurations.Select(x => x);

            systemModel.SupportEmail = data.Where(x => x.Key == "SupportEmailAddress").FirstOrDefault().Value;
            systemModel.SupportContact = data.Where(x => x.Key == "SupportContactNumber").FirstOrDefault().Value;
            systemModel.FacebookURL = data.Where(x => x.Key == "FBICON").FirstOrDefault().Value;
            systemModel.TwitterURL = data.Where(x => x.Key == "TWITTERICON").FirstOrDefault().Value;
            systemModel.LinkedinURL = data.Where(x => x.Key == "LNICON").FirstOrDefault().Value;
            systemModel.EmailAddresses =  data.Where(x => x.Key == "EmailAddressesForNotify").FirstOrDefault().Value;

            return View(systemModel);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]    
        [ValidateAntiForgeryToken]
        [OutputCache(Duration = 0)]
        public ActionResult SystemConfiguration(SystemConfigViewModel sysModel)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

            if (ModelState.IsValid)
            {
                var data = db.SystemConfigurations.Select(x => x);

                data.Where(x => x.Key == "SupportEmailAddress").FirstOrDefault().Value = sysModel.SupportEmail;
                data.Where(x => x.Key == "SupportContactNumber").FirstOrDefault().Value = sysModel.SupportContact;
                data.Where(x => x.Key == "FBICON").FirstOrDefault().Value = sysModel.FacebookURL;
                data.Where(x => x.Key == "TWITTERICON").FirstOrDefault().Value = sysModel.TwitterURL;
                data.Where(x => x.Key == "LNICON").FirstOrDefault().Value = sysModel.LinkedinURL;

                if (sysModel.EmailAddresses == null)
                {
                    data.Where(x => x.Key == "EmailAddressesForNotify").FirstOrDefault().Value = "NA";
                }
                else
                {
                    data.Where(x => x.Key == "EmailAddressesForNotify").FirstOrDefault().Value = sysModel.EmailAddresses;
                }

                if (sysModel.NotePicture != null)
                {
                    string noteName = System.IO.Path.GetFileName(sysModel.NotePicture.FileName); //get file name 

                    string defaultFilename = Directory.GetFiles(Server.MapPath("~/Content/image/default-note-img/")).FirstOrDefault(); //get path
                    string finalfilename = System.IO.Path.GetFileName(defaultFilename); //get already saved file name

                    string PathImage = Request.MapPath("~/Content/image/default-note-img" + "/" + finalfilename);
                    //delete prev file
                    FileInfo file = new FileInfo(PathImage);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    string notefilepath = "~/Content/image/default-note-img/";
                    string notefinalfilepath = Path.Combine(Server.MapPath(notefilepath), noteName);
                    sysModel.NotePicture.SaveAs(notefinalfilepath); //save image in folder

                    //store path in database
                    data.Where(x => x.Key == "DefaultNoteDisplayPicture").FirstOrDefault().Value = notefilepath + noteName;
                }

                if (sysModel.UserPicture != null)
                {
                    string userName = System.IO.Path.GetFileName(sysModel.UserPicture.FileName);

                    string defaultFilename = Directory.GetFiles(Server.MapPath("~/Content/image/default-user-img/")).FirstOrDefault();
                    string finalfilename = System.IO.Path.GetFileName(defaultFilename);

                    string PathImage = Request.MapPath("~/Content/image/default-user-img" + "/" + finalfilename);
                    FileInfo file = new FileInfo(PathImage);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    string notefilepath = "~/Content/image/default-user-img/";
                    string notefinalfilepath = Path.Combine(Server.MapPath(notefilepath), userName);
                    sysModel.UserPicture.SaveAs(notefinalfilepath);

                    data.Where(x => x.Key == "DefaultMemberDisplayPicture").FirstOrDefault().Value = notefilepath + userName;
                }
                          
                foreach(var item in data)
                {
                    item.ModifiedDate = DateTime.Now;
                }

                db.SaveChanges();
                ViewBag.sucess = "Record Updated Sucessfully";
                return View();
            }

            return View();
        }               

    }
}