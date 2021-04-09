using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Models;

namespace NotesMarketPlace.Controllers
{
    [RoutePrefix("Admin")]    
    public class AdminProfileController : Controller
    {
        readonly NotesMarketPlaceEntities db;
        public AdminProfileController()
        {
            db = new NotesMarketPlaceEntities();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("AdminProfile")]        
        public ActionResult AdminProfile()
        {
            @ViewBag.profile = "active";

            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name && x.IsActive == true);
            var userProfile = db.UserProfile.FirstOrDefault(x => x.UserID == user.ID && x.IsActive == true);

            AdminProfileViewModel profile = new AdminProfileViewModel();

            profile.FirstName = user.FirstName;
            profile.LastName = user.LastName;
            profile.Email = user.Email;

            profile.PhoneNumber_CountryCode = userProfile.PhoneNumber_CountryCode;
            profile.PhoneNumber = userProfile.PhoneNumber;

            if(userProfile.SecondaryEmailAddress != null)
            {
                profile.secEmail = userProfile.SecondaryEmailAddress;
            }                      

            return View(profile);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("AdminProfile")]
        public ActionResult AdminProfile(AdminProfileViewModel profileModel)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == profileModel.Email && x.IsActive == true);
            var userProfile = db.UserProfile.FirstOrDefault(x => x.UserID == user.ID && x.IsActive == true);

            //add data into user & profile table
            user.FirstName = profileModel.FirstName;
            user.LastName = profileModel.LastName;
            userProfile.SecondaryEmailAddress = profileModel.secEmail;
            userProfile.PhoneNumber = profileModel.PhoneNumber;
            userProfile.PhoneNumber_CountryCode = profileModel.PhoneNumber_CountryCode;

            if (userProfile.ProfilePicture == null)
            {
                //get default file name 
                string defaultFilename = Directory.GetFiles(Server.MapPath("~/Content/image/default-user-img/")).FirstOrDefault();
                string finalfilename = System.IO.Path.GetFileName(defaultFilename); 

                //defalt path if path is null
                string userprofilepath = "~/Content/image/default-user-img/";
                userProfile.ProfilePicture = userprofilepath + finalfilename; //store path

                if (profileModel.ProfilePicture != null)
                {
                    string profilePictureFileName = Path.GetFileName(profileModel.ProfilePicture.FileName); //get file name
                    string imagePath = "~/Admins/" + user.ID + "/";
                    CreateDirectoryIfMissing(imagePath);
                    string admiProfileFilePath = Path.Combine(Server.MapPath(imagePath), profilePictureFileName);
                    userProfile.ProfilePicture = imagePath + profilePictureFileName; //path save in database
                    profileModel.ProfilePicture.SaveAs(admiProfileFilePath); //image save in folder
                }
            }
            else
            {
                if (profileModel.ProfilePicture != null)
                {
                    string FileNameDelete = System.IO.Path.GetFileName(userProfile.ProfilePicture);
                    string PathImage = Request.MapPath("~/Admins/" + user.ID + "/" + FileNameDelete); //full path for delete file

                    //delete file
                    FileInfo file = new FileInfo(PathImage);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    string profilepicturefilename = Path.GetFileName(profileModel.ProfilePicture.FileName); //get new file name
                    string profilepicturepath = "~/Admins/" + user.ID + "/";
                    CreateDirectoryIfMissing(profilepicturepath);
                    string profilepicturefilepath = Path.Combine(Server.MapPath(profilepicturepath), profilepicturefilename); //get full path
                    userProfile.ProfilePicture = profilepicturepath + profilepicturefilename; //path save in database
                    profileModel.ProfilePicture.SaveAs(profilepicturefilepath); //image save in folder

                    db.Configuration.ValidateOnSaveEnabled = false;
                }
            }

            db.SaveChanges();

            return View();
        }

        //folder structure create if missing
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