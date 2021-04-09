using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Models;
using System.Diagnostics;
using System.IO;

namespace NotesMarketPlace.Controllers
{
    [Authorize]    
    public class UserProfileController : Controller
    {
        readonly NotesMarketPlaceEntities db;
        public UserProfileController()
        {
            db = new NotesMarketPlaceEntities();
        }

        // GET: MyProfile
        [HttpGet]
        [Route("MyProfile")]
        public ActionResult MyProfile()
        {
            ViewBag.navclass = "white-nav";

            var gender = db.ReferenceData.Where(x => x.IsActive == true && x.RefCategory == "Gender").ToList();
            
            Users user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            UserProfileViewModel userprofilemodel = new UserProfileViewModel();
            var userprofiledata = db.UserProfile.FirstOrDefault(x => x.UserID == user.ID);
            
            //auto fill details
            userprofilemodel.FirstName = user.FirstName;
            userprofilemodel.LastName = user.LastName;
            userprofilemodel.Email = user.Email;
            userprofilemodel.GenderList = gender;

            if (db.UserProfile.Any(x => x.UserID == user.ID))
            {
                userprofilemodel.DOB = userprofiledata.DOB;
                userprofilemodel.Gender = userprofiledata.Gender;
                userprofilemodel.PhoneNumber_CountryCode = userprofiledata.PhoneNumber_CountryCode;
                userprofilemodel.PhoneNumber = userprofiledata.PhoneNumber;
                userprofilemodel.AddressLine1 = userprofiledata.AddressLine1;
                userprofilemodel.AddressLine2 = userprofiledata.AddressLine2;
                userprofilemodel.City = userprofiledata.City;
                userprofilemodel.ZipCode = userprofiledata.ZipCode;
                userprofilemodel.Country = userprofiledata.Country;
                userprofilemodel.State = userprofiledata.State;
                userprofilemodel.University = userprofiledata.University;
                userprofilemodel.College = userprofiledata.College;                

            }

             return View(userprofilemodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("MyProfile")]
        public ActionResult Myprofile(UserProfileViewModel userProfilemodel)
        {
            ViewBag.navclass = "white-nav";

            Users user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

            if (user != null && ModelState.IsValid)
            {
                if (!db.UserProfile.Any(x => x.UserID == user.ID))
                {
                    UserProfile userProfile = new UserProfile();
                    
                    if(user.FirstName != userProfilemodel.FirstName || user.LastName != userProfilemodel.LastName )
                    {
                        user.FirstName = userProfilemodel.FirstName;
                        user.LastName = userProfilemodel.LastName;
                        user.ModifiedDate = DateTime.Now;
                        db.SaveChanges();
                    }

                    userProfile.UserID = user.ID;
                    userProfile.DOB = userProfilemodel.DOB;
                    userProfile.Gender = userProfilemodel.Gender;
                    userProfile.PhoneNumber_CountryCode = userProfilemodel.PhoneNumber_CountryCode;
                    userProfile.PhoneNumber = userProfilemodel.PhoneNumber;
                    userProfile.AddressLine1 = userProfilemodel.AddressLine1;
                    userProfile.AddressLine2 = userProfilemodel.AddressLine2;
                    userProfile.City = userProfilemodel.City;
                    userProfile.State = userProfilemodel.State;
                    userProfile.ZipCode = userProfilemodel.ZipCode;
                    userProfile.Country = userProfilemodel.Country;
                    userProfile.University = userProfilemodel.University;
                    userProfile.College = userProfilemodel.College;
                    userProfile.CreatedDate = DateTime.Now;
                    userProfile.IsActive = true;
                    userProfile.CreatedBy = user.ID;

                    if (userProfilemodel.ProfilePicture != null)
                    {
                        //var userprofile = Profile;
                        string userprofilefilename = System.IO.Path.GetFileName(userProfilemodel.ProfilePicture.FileName);
                        string userprofilepath = "~/Members/" + user.ID + "/";
                        CreateDirectoryIfMissing(userprofilepath);
                        string userprofilefilepath = Path.Combine(Server.MapPath(userprofilepath), userprofilefilename);
                        userProfile.ProfilePicture = userprofilepath + userprofilefilename;
                        userProfilemodel.ProfilePicture.SaveAs(userprofilefilepath);
                    }
                    else
                    {                        
                        //fetch default file name 
                        string defaultFilename= Directory.GetFiles(Server.MapPath("~/Content/image/default-user-img/")).FirstOrDefault();
                        string finalfilename = System.IO.Path.GetFileName(defaultFilename);
                        
                        //store default file name
                        string userprofilepath = "~/Content/image/default-user-img/";                                               
                        userProfile.ProfilePicture = userprofilepath + finalfilename;
                        
                    }                   

                    db.UserProfile.Add(userProfile);                    
                    db.SaveChanges();                   

                }
                else
                {
                    user.FirstName = userProfilemodel.FirstName;
                    user.LastName = userProfilemodel.LastName;

                    UserProfile profile = db.UserProfile.FirstOrDefault(x => x.UserID == user.ID);
                    if (profile.IsActive == true)
                    {
                        profile.DOB = userProfilemodel.DOB;
                        profile.Gender = userProfilemodel.Gender;
                        profile.PhoneNumber_CountryCode = userProfilemodel.PhoneNumber_CountryCode;
                        profile.PhoneNumber = userProfilemodel.PhoneNumber;
                        profile.AddressLine1 = userProfilemodel.AddressLine1;
                        profile.AddressLine2 = userProfilemodel.AddressLine2;
                        profile.City = userProfilemodel.City;
                        profile.State = userProfilemodel.State;
                        profile.ZipCode = userProfilemodel.ZipCode;
                        profile.Country = userProfilemodel.Country;
                        profile.University = userProfilemodel.University;
                        profile.College = userProfilemodel.College;
                        profile.ModifiedDate = DateTime.Now;

                        if (userProfilemodel.ProfilePicture != null)
                        {
                            string FileNameDelete = System.IO.Path.GetFileName(profile.ProfilePicture);
                            string PathImage = Request.MapPath("~/Members/" + user.ID + "/" + FileNameDelete);
                            FileInfo file = new FileInfo(PathImage);
                            if (file.Exists)
                            {
                                file.Delete();
                            }
                            string profilepicturefilename = Path.GetFileName(userProfilemodel.ProfilePicture.FileName);
                            string profilepicturepath = "~/Members/" + user.ID + "/";
                            string profilepicturefilepath = Path.Combine(Server.MapPath(profilepicturepath), profilepicturefilename);
                            profile.ProfilePicture = profilepicturepath + profilepicturefilename;
                            userProfilemodel.ProfilePicture.SaveAs(profilepicturefilepath);

                            db.Configuration.ValidateOnSaveEnabled = false;                            
                        }

                        db.SaveChanges();
                    }
                    else
                    {
                        return Content("Record Note found");
                    }
                }

            }

            UserProfileViewModel userprofiledetail = new UserProfileViewModel();

            //get gender list 
            var gender = db.ReferenceData.Where(x => x.IsActive == true && x.RefCategory == "Gender").ToList();
            userprofiledetail.GenderList = gender;
            
            return View(userprofiledetail);             
        }

        private void CreateDirectoryIfMissing(string path)
        {
            bool folderExists = Directory.Exists(Server.MapPath(path));
            if (!folderExists)
            {
                Directory.CreateDirectory(Server.MapPath(path));
            }

        }
    }
}