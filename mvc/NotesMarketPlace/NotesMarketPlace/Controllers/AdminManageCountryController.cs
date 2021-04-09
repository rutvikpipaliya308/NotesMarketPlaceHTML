using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Models;

namespace NotesMarketPlace.Controllers
{
    [RoutePrefix("Admin")]
    public class AdminManageCountryController : Controller
    {
        readonly NotesMarketPlaceEntities db;
        public AdminManageCountryController()
        {
            db = new NotesMarketPlaceEntities();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("ManageCountry")]
        public ActionResult ManageCountry(string SortOrder, string Country_search, int Country_page = 1)
        {
            ViewBag.Settings = "active";
            ViewBag.managecountries = "active";

            //sroting
            ViewBag.CountryNameSortParm = SortOrder == "CountryName" ? "CountryName_desc" : "CountryName";
            ViewBag.CountryCodeSortParm = SortOrder == "CountryCode" ? "CountryCode_desc" : "CountryCode";
            ViewBag.AddedBySortParm = SortOrder == "AddedBy" ? "AddedBy_desc" : "AddedBy";
            ViewBag.ActiveSortParm = SortOrder == "Active" ? "Active_desc" : "Active";
            ViewBag.DateSortParm = SortOrder == "Date" ? "Date_desc" : "Date";

            ManageCountryViewModel Countries = new ManageCountryViewModel();

            //Query string
            var country = from con in db.Countries
                       join ur in db.Users on con.CreatedBy equals ur.ID
                       select new ManageCountry { countries = con, users = ur };

            //searching
            if (Country_search != null)
            {
                country = country.Where(x => x.countries.Name.Contains(Country_search) ||
                                            x.countries.CountryCode.Contains(Country_search) ||
                                            x.countries.CreatedDate.ToString().Contains(Country_search) ||
                                            x.users.FirstName.Contains(Country_search) ||
                                            x.users.LastName.Contains(Country_search));
            }

            //sortorder
            switch (SortOrder)
            {
                case "CountryName_desc":
                    country = country.OrderByDescending(s => s.countries.Name);
                    break;
                case "CountryName":
                    country = country.OrderBy(s => s.countries.Name);
                    break;
                case "CountryCode_desc":
                    country = country.OrderByDescending(s => s.countries.CountryCode);
                    break;
                case "CountryCode":
                    country = country.OrderBy(s => s.countries.CountryCode);
                    break;
                case "AddedBy_desc":
                    country = country.OrderByDescending(s => s.users.FirstName);
                    break;
                case "AddedBy":
                    country = country.OrderBy(s => s.users.FirstName);
                    break;
                case "Active_desc":
                    country = country.OrderByDescending(s => s.countries.IsActive);
                    break;
                case "Active":
                    country = country.OrderBy(s => s.countries.IsActive);
                    break;
                case "Date_desc":
                    country = country.OrderByDescending(s => s.countries.CreatedDate);
                    break;
                case "Date":
                    country = country.OrderBy(s => s.countries.CreatedDate);
                    break;
                default:
                    country = country.OrderByDescending(s => s.countries.CreatedDate);
                    break;
            }

            //assign data to model
            Countries.Country = country;

            //pagination
            var pager = new Pager(country.Count(), Country_page, 10);
            ViewBag.currentPage = pager.CurrentPage;
            ViewBag.endPage = pager.EndPage;
            ViewBag.startpage = pager.StartPage;
            ViewBag.totalPage = country.Count();

            Countries.Country = country.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);

            return View(Countries);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [Route("AddCountry")]
        public ActionResult AddCountry()
        {
            ViewBag.Settings = "active";
            ViewBag.managecountries = "active";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("AddCountry")]
        public ActionResult AddCountry(ManageCountryViewModel countrymodel)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
           
            if (ModelState.IsValid)
            {
                //add data to country table
                Countries Con = new Countries();

                Con.Name = countrymodel.countryName;
                Con.CountryCode = countrymodel.countryCode;
                Con.CreatedDate = DateTime.Now;
                Con.CreatedBy = user.ID;
                Con.IsActive = true;

                db.Countries.Add(Con);
                db.SaveChanges();

                return RedirectToAction("ManageCountry");
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("EditCountry/{id}")]
        public ActionResult EditCountry(int id)
        {
            ViewBag.Settings = "active";
            ViewBag.managecountries = "active";

            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);           
            var detail = db.Countries.FirstOrDefault(x => x.ID == id);

            ManageCountryViewModel countryModel = new ManageCountryViewModel();

            countryModel.ID = detail.ID;
            countryModel.countryName = detail.Name;
            countryModel.countryCode = detail.CountryCode;

            return View(countryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("EditCountry/{id}")]
        public ActionResult EditCountry(ManageCountryViewModel conModel)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
           
            var model = db.Countries.Where(x => x.ID == conModel.ID).FirstOrDefault();
            if (ModelState.IsValid)
            {
                model.Name = conModel.countryName;
                model.CountryCode = conModel.countryCode;
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = user.ID;
                model.IsActive = true;

                db.SaveChanges();

                return RedirectToAction("ManageCountry");
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("DeleteCountry/{conID}")]
        public ActionResult DeleteCountry(int conID)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);            
            var model = db.Countries.FirstOrDefault(x => x.ID == conID && x.IsActive == true);

            if(user != null && model != null)
            {
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = user.ID;
                model.IsActive = false;

                db.SaveChanges();
            }            

            return RedirectToAction("ManageCountry");
        }

    }
}