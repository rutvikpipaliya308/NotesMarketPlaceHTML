using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Models;

namespace NotesMarketPlace.Controllers
{
    [RoutePrefix("Admin")]
    public class AdminManageTypeController : Controller
    {
        readonly NotesMarketPlaceEntities db;
        public AdminManageTypeController()
        {
            db = new NotesMarketPlaceEntities();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("ManageType")]
        public ActionResult ManageType(string SortOrder, string Type_search, int Type_page = 1)
        {           
            ViewBag.Settings = "active";
            ViewBag.managetype = "active";

            //sortorder
            ViewBag.TypeSortParm = SortOrder == "Type" ? "Type_desc" : "Type";
            ViewBag.DescriptionSortParm = SortOrder == "Description" ? "Description_desc" : "Description";
            ViewBag.AddedBySortParm = SortOrder == "AddedBy" ? "AddedBy_desc" : "AddedBy";
            ViewBag.ActiveSortParm = SortOrder == "Active" ? "Active_desc" : "Active";
            ViewBag.DateSortParm = SortOrder == "Date" ? "Date_desc" : "Date";

            ManageTypeViewModel Types = new ManageTypeViewModel();

            //Query string
            var type = from typ in db.NoteTypes
                       join ur in db.Users on typ.CreatedBy equals ur.ID
                       select new ManageType { Type = typ, users = ur };

            //searching
            if (Type_search != null)
            {
                type = type.Where(x => x.Type.Name.Contains(Type_search) ||
                                            x.Type.Description.Contains(Type_search) ||
                                            x.Type.CreatedDate.ToString().Contains(Type_search) ||
                                            x.users.FirstName.Contains(Type_search) ||
                                            x.users.LastName.Contains(Type_search));
            }

            //sorting
            switch (SortOrder)
            {
                case "Type_desc":
                    type = type.OrderByDescending(s => s.Type.Name);
                    break;
                case "Type":
                    type = type.OrderBy(s => s.Type.Name);
                    break;
                case "Description_desc":
                    type = type.OrderByDescending(s => s.Type.Description);
                    break;
                case "Description":
                    type = type.OrderBy(s => s.Type.Description);
                    break;
                case "AddedBy_desc":
                    type = type.OrderByDescending(s => s.users.FirstName);
                    break;
                case "AddedBy":
                    type = type.OrderBy(s => s.users.FirstName);
                    break;
                case "Active_desc":
                    type = type.OrderByDescending(s => s.Type.IsActive);
                    break;
                case "Active":
                    type = type.OrderBy(s => s.Type.IsActive);
                    break;
                case "Date_desc":
                    type = type.OrderByDescending(s => s.Type.CreatedDate);
                    break;
                case "Date":
                    type = type.OrderBy(s => s.Type.CreatedDate);
                    break;
                default:
                    type = type.OrderByDescending(s => s.Type.CreatedDate);
                    break;
            }

            //assign data to model
            Types.NoteTypes = type;

            //pagination
            var pager = new Pager(type.Count(), Type_page, 10);
            ViewBag.currentPage = pager.CurrentPage;
            ViewBag.endPage = pager.EndPage;
            ViewBag.startpage = pager.StartPage;
            ViewBag.totalPage = type.Count();

            Types.NoteTypes = type.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);

            return View(Types);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [Route("AddType")]
        public ActionResult AddType()
        {
            ViewBag.Settings = "active";
            ViewBag.managetype = "active";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("AddType")]
        public ActionResult AddType(ManageTypeViewModel type)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            
            if (ModelState.IsValid)
            {
                //add data to notetype table
                NoteTypes Types = new NoteTypes();

                Types.Name = type.typeName;
                Types.Description = type.description;
                Types.CreatedDate = DateTime.Now;
                Types.CreatedBy = user.ID;
                Types.IsActive = true;

                db.NoteTypes.Add(Types);
                db.SaveChanges();

                return RedirectToAction("ManageType");
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("EditType/{id}")]
        public ActionResult EditType(int id)
        {
            ViewBag.Settings = "active";
            ViewBag.managetype = "active";

            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);           
            var detail = db.NoteTypes.FirstOrDefault(x => x.ID == id);

            ManageTypeViewModel typeModel = new ManageTypeViewModel();

            typeModel.ID = detail.ID;
            typeModel.typeName = detail.Name;
            typeModel.description = detail.Description;

            return View(typeModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("EditType/{id}")]
        public ActionResult EditType(ManageTypeViewModel typModel)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);            
            var model = db.NoteTypes.Where(x => x.ID == typModel.ID).FirstOrDefault();

            if (ModelState.IsValid)
            {
                model.Name = typModel.typeName;
                model.Description = typModel.description;
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = user.ID;
                model.IsActive = true;

                db.SaveChanges();

                return RedirectToAction("ManageType");
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("DeleteType/{typID}")]
        public ActionResult DeleteType(int typID)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);           
            var model = db.NoteTypes.FirstOrDefault(x => x.ID == typID && x.IsActive == true);
            if (user != null && model != null)
            {
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = user.ID;
                model.IsActive = false;

                db.SaveChanges();
            }           

            return RedirectToAction("ManageType");
        }
    }
}