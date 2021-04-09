using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Models;

namespace NotesMarketPlace.Controllers
{
    [RoutePrefix("SuperAdmin")]
    public class AdminManageAdministratorController : Controller
    {
        readonly NotesMarketPlaceEntities db;
        public AdminManageAdministratorController()
        {
            db = new NotesMarketPlaceEntities();
        }

        [HttpGet]
        [Route("ManageAdministrator")]
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult ManageAdministrator(string SortOrder, string Admini_search, int Admini_page = 1)
        {
            ViewBag.Settings = "active";
            ViewBag.manageadmini = "active";

            //sorting
            ViewBag.FirstnameSortParm = SortOrder == "Firstname" ? "Firstname_desc" : "Firstname";
            ViewBag.LastnameSortParm = SortOrder == "Lastname" ? "Lastname_desc" : "Lastname";
            ViewBag.EmailSortParm = SortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.PhoneNoSortParm = SortOrder == "PhoneNo" ? "PhoneNo_desc" : "PhoneNo";
            ViewBag.ActiveSortParm = SortOrder == "Active" ? "Active_desc" : "Active";
            ViewBag.DateSortParm = SortOrder == "Date" ? "Date_desc" : "Date";

            ManageAdministratorViewModel adminModel = new ManageAdministratorViewModel();

            //Query string
            var admins = from ur in db.Users
                         join urp in db.UserProfile on ur.ID equals urp.UserID
                         where ur.RoleID == 3
                         select new ManageAdmini { admins = ur, adminProfiles = urp };

            //searching
            if (Admini_search != null)
            {
                admins = admins.Where(x => x.admins.FirstName.Contains(Admini_search) ||
                                            x.admins.LastName.Contains(Admini_search) ||
                                            x.admins.Email.Contains(Admini_search) ||
                                            x.adminProfiles.PhoneNumber.Contains(Admini_search) ||
                                            x.adminProfiles.PhoneNumber_CountryCode.Contains(Admini_search) ||
                                            x.admins.CreatedDate.ToString().Contains(Admini_search) ||
                                            x.admins.IsActive.ToString().Contains(Admini_search));
            }

            //sorting
            switch (SortOrder)
            {
                case "Firstname_desc":
                    admins = admins.OrderByDescending(s => s.admins.FirstName);
                    break;
                case "Firstname":
                    admins = admins.OrderBy(s => s.admins.FirstName);
                    break;
                case "Lastname_desc":
                    admins = admins.OrderByDescending(s => s.admins.LastName);
                    break;
                case "Lastname":
                    admins = admins.OrderBy(s => s.admins.LastName);
                    break;
                case "Email_desc":
                    admins = admins.OrderByDescending(s => s.admins.Email);
                    break;
                case "Email":
                    admins = admins.OrderBy(s => s.admins.Email);
                    break;
                case "PhoneNo_desc":
                    admins = admins.OrderByDescending(s => s.adminProfiles.PhoneNumber);
                    break;
                case "PhoneNo":
                    admins = admins.OrderBy(s => s.adminProfiles.PhoneNumber);
                    break;
                case "Active_desc":
                    admins = admins.OrderByDescending(s => s.admins.IsActive);
                    break;
                case "Active":
                    admins = admins.OrderBy(s => s.admins.IsActive);
                    break;
                case "Date_desc":
                    admins = admins.OrderByDescending(s => s.admins.CreatedDate);
                    break;
                case "Date":
                    admins = admins.OrderBy(s => s.admins.CreatedDate);
                    break;
                default:
                    admins = admins.OrderByDescending(s => s.admins.CreatedDate);
                    break;
            }

            //assign value to model
            adminModel.allAdmins = admins;

            //pagination
            var pager = new Pager(admins.Count(), Admini_page, 10);
            ViewBag.currentPage = pager.CurrentPage;
            ViewBag.endPage = pager.EndPage;
            ViewBag.startpage = pager.StartPage;
            ViewBag.totalPage = admins.Count();

            adminModel.allAdmins = admins.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);

            return View(adminModel);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        [Route("AddAdmin")]
        public ActionResult AddAdmin()
        {
            ViewBag.Settings = "active";
            ViewBag.manageadmini = "active";

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        [ValidateAntiForgeryToken]
        [Route("AddAdmin")]
        public ActionResult AddAdmin(ManageAdministratorViewModel category)
        {
            if (ModelState.IsValid)
            {
                bool isEmailRegistered = db.Users.Any(x => x.Email == category.Email);
                if (!isEmailRegistered)
                {
                    //add data in user table
                    Users adminUser = new Users();

                    adminUser.RoleID = 3;
                    adminUser.FirstName = category.FirstName;
                    adminUser.LastName = category.LastName;
                    adminUser.Email = category.Email;
                    string adminPassword = "Admin@123";
                    adminUser.Password = adminPassword;
                    adminUser.IsEmailVerified = true;
                    adminUser.CreatedDate = DateTime.Now;
                    adminUser.CreatedBy = 4;
                    adminUser.IsActive = true;

                    db.Users.Add(adminUser);
                    db.SaveChanges();

                    int userID = db.Users.FirstOrDefault(x => x.Email == category.Email && x.IsActive == true).ID;

                    //add table is user profile table 
                    UserProfile adminProfile = new UserProfile();

                    adminProfile.UserID = userID;
                    adminProfile.DOB = DateTime.Now;
                    adminProfile.Gender = 1;
                    adminProfile.PhoneNumber_CountryCode = "NA";
                    adminProfile.PhoneNumber = category.PhoneNumber;                    
                    adminProfile.AddressLine1 = "NA";
                    adminProfile.AddressLine2 = "NA";
                    adminProfile.City = "NA";
                    adminProfile.State = "NA";
                    adminProfile.ZipCode = "NA";
                    adminProfile.Country = "NA";                                      
                    adminProfile.CreatedBy = 4;
                    adminProfile.CreatedDate = DateTime.Now;
                    adminProfile.IsActive = true;

                    db.UserProfile.Add(adminProfile);
                    db.SaveChanges();                    
                        
                     return RedirectToAction("ManageAdministrator");                    
                }
                else
                {
                    ModelState.AddModelError("Email", "Email is already registered");
                    return View();                    
                }
            }           

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        [Route("EditAdmin/{id}")]
        public ActionResult EditAdmins(int id)
        {
            ViewBag.Settings = "active";
            ViewBag.manageadmini = "active";
            
            //fetch data of admin
            var detail = db.Users.FirstOrDefault(x => x.ID == id);
            var profiledetail = db.UserProfile.FirstOrDefault(x => x.UserID == id);
            ManageAdministratorViewModel adminModel = new ManageAdministratorViewModel();

            adminModel.ID = detail.ID;
            adminModel.FirstName = detail.FirstName;
            adminModel.LastName = detail.LastName;
            adminModel.Email = detail.Email;
            adminModel.PhoneNumber_CountryCode = profiledetail.PhoneNumber_CountryCode;
            adminModel.PhoneNumber = profiledetail.PhoneNumber;            

            return View(adminModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]       
        [Route("EditAdmin/{id}")]
        public ActionResult EditAdmins(ManageAdministratorViewModel adminModel)
        {
            var User = db.Users.FirstOrDefault(x => x.ID == adminModel.ID);
            var UserProfile = db.UserProfile.FirstOrDefault(x => x.UserID == adminModel.ID);

            if (ModelState.IsValid)
            {
                //save new data in tables 
                if(adminModel.Email != User.Email)
                {
                    if(db.Users.Any(x => x.Email == adminModel.Email))
                    {
                        ModelState.AddModelError("Email", "This email is already registered");
                        return View();
                    }                    
                    User.Email = adminModel.Email;
                    db.SaveChanges();
                }
               
                User.FirstName = adminModel.FirstName;
                User.LastName = adminModel.LastName;
                UserProfile.PhoneNumber_CountryCode = adminModel.PhoneNumber_CountryCode;
                UserProfile.PhoneNumber = adminModel.PhoneNumber;
                User.IsActive = true;
                User.ModifiedDate = DateTime.Now;
                UserProfile.IsActive = true;
                UserProfile.ModifiedDate = DateTime.Now;

                db.SaveChanges();                                            

                return RedirectToAction("ManageAdministrator");
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        [Route("DeleteAdmin/{admID}")]
        public ActionResult DeleteAdmin(int admID)
        {
            var User = db.Users.FirstOrDefault(x => x.ID == admID && x.IsActive == true);
            var userProfile = db.UserProfile.FirstOrDefault(x => x.UserID == admID && x.IsActive == true);

            if (User != null && userProfile != null)
            {
                User.IsActive = false;
                User.ModifiedDate = DateTime.Now;

                userProfile.IsActive = false;
                userProfile.ModifiedDate = DateTime.Now;

                db.SaveChanges();
            }

            return RedirectToAction("ManageAdministrator");
        }

    }
}