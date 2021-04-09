using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Models;

namespace NotesMarketPlace.Controllers
{
    [RoutePrefix("Admin")]
    public class AdminManageCategoryController : Controller
    {
        readonly NotesMarketPlaceEntities db;
        public AdminManageCategoryController()
        {
            db = new NotesMarketPlaceEntities();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("ManageCategory")]
        public ActionResult ManageCategory(string SortOrder, string Category_search, int Category_page = 1)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

            ViewBag.Settings = "active";
            ViewBag.managecategory = "active";

            //sorting
            ViewBag.CategorySortParm = SortOrder == "Category" ? "Category_desc" : "Category";
            ViewBag.DescriptionSortParm = SortOrder == "Description" ? "Description_desc" : "Description";
            ViewBag.AddedBySortParm = SortOrder == "AddedBy" ? "AddedBy_desc" : "AddedBy";
            ViewBag.ActiveSortParm = SortOrder == "Active" ? "Active_desc" : "Active";
            ViewBag.DateSortParm = SortOrder == "Date" ? "Date_desc" : "Date";

            ManageCategoryViewModel Categories = new ManageCategoryViewModel();

            //Query string
            var cate = from cat in db.NoteCategories
                       join ur in db.Users on cat.CreatedBy equals ur.ID
                       select new ManageCat { categories = cat, users = ur };

            //searching
            if (Category_search != null)
            {
                cate = cate.Where(x => x.categories.Name.Contains(Category_search) ||
                                            x.categories.Description.Contains(Category_search) ||
                                            x.categories.CreatedDate.ToString().Contains(Category_search) ||
                                            x.users.FirstName.Contains(Category_search) ||
                                            x.users.LastName.Contains(Category_search));
            }

            //sorting
            switch (SortOrder)
            {
                case "Category_desc":
                    cate = cate.OrderByDescending(s => s.categories.Name);
                    break;
                case "Category":
                    cate = cate.OrderBy(s => s.categories.Name);
                    break;
                case "Description_desc":
                    cate = cate.OrderByDescending(s => s.categories.Description);
                    break;
                case "Description":
                    cate = cate.OrderBy(s => s.categories.Description);
                    break;
                case "AddedBy_desc":
                    cate = cate.OrderByDescending(s => s.users.FirstName);
                    break;
                case "AddedBy":
                    cate = cate.OrderBy(s => s.users.FirstName);
                    break;
                case "Active_desc":
                    cate = cate.OrderByDescending(s => s.categories.IsActive);
                    break;
                case "Active":
                    cate = cate.OrderBy(s => s.categories.IsActive);
                    break;
                case "Date_desc":
                    cate = cate.OrderByDescending(s => s.categories.CreatedDate);
                    break;
                case "Date":
                    cate = cate.OrderBy(s => s.categories.CreatedDate);
                    break;
                default:
                    cate = cate.OrderByDescending(s => s.categories.CreatedDate);
                    break;
            }

            //assign data to model
            Categories.Category = cate;

            //pagination
            var pager = new Pager(cate.Count(), Category_page, 10);
            ViewBag.currentPage = pager.CurrentPage;
            ViewBag.endPage = pager.EndPage;
            ViewBag.startpage = pager.StartPage;
            ViewBag.totalPage = cate.Count();

            Categories.Category = cate.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);

            return View(Categories);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [Route("AddCategory")]
        public ActionResult AddCategory()
        {
            ViewBag.Settings = "active";
            ViewBag.managecategory = "active";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("AddCategory")]
        public ActionResult AddCategory(ManageCategoryViewModel category)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

            if (ModelState.IsValid)
            {
                //add data into category table
                NoteCategories Notes = new NoteCategories();

                Notes.Name = category.categoryName;
                Notes.Description = category.description;
                Notes.CreatedDate = DateTime.Now;
                Notes.CreatedBy = user.ID;
                Notes.IsActive = true;

                db.NoteCategories.Add(Notes);
                db.SaveChanges();

                return RedirectToAction("ManageCategory");
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("EditCategory/{id}")]
        public ActionResult EditCategory(int id)
        {
            ViewBag.Settings = "active";
            ViewBag.managecategory = "active";

            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);          
            var detail = db.NoteCategories.FirstOrDefault(x => x.ID == id);

            ManageCategoryViewModel categoryModel = new ManageCategoryViewModel();            

            categoryModel.ID = detail.ID;
            categoryModel.categoryName = detail.Name;
            categoryModel.description = detail.Description;

            return View(categoryModel);
        }

        [HttpPost]        
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("EditCategory/{id}")]
        public ActionResult EditCategory(ManageCategoryViewModel catModel)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);           

            var model = db.NoteCategories.Where(x => x.ID == catModel.ID).FirstOrDefault();
            if (ModelState.IsValid)
            {                              
                model.Name = catModel.categoryName;
                model.Description = catModel.description;
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = user.ID;
                model.IsActive = true;

                db.SaveChanges();

                return RedirectToAction("ManageCategory");
            }            

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("DeleteCategory/{catID}")]
        public ActionResult DeleteCategory(int catID)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);          
            var model = db.NoteCategories.FirstOrDefault(x => x.ID == catID && x.IsActive == true);
                        
            if(user != null && model != null)
            {
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = user.ID;
                model.IsActive = false;

                db.SaveChanges();
            }           

            return RedirectToAction("ManageCategory");
        }

    }
}