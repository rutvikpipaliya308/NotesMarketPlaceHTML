using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Models;

namespace NotesMarketPlace.Controllers
{    
    public class AdminNotesUnderReviewController : Controller
    {
        readonly NotesMarketPlaceEntities db;
        public AdminNotesUnderReviewController()
        {
            db = new NotesMarketPlaceEntities();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("Admin/UnderReviewNotes")]
        public ActionResult UnderReviewNotes(string SortOrder, string NUReview_search, string sellerName, int memberId = 0, int NUR_page = 1)
        {
            ViewBag.Notes = "active";
            ViewBag.underReview = "active";
            ViewBag.tempId = memberId;            

            //sortorder
            ViewBag.TitleSortParm = SortOrder == "Title" ? "title_desc" : "Title";
            ViewBag.CategorySortParm = SortOrder == "Category" ? "category_desc" : "Category";            
            ViewBag.StatusSortParm = SortOrder == "Status" ? "Status_desc" : "Status";
            ViewBag.SellerSortParm = SortOrder == "Seller" ? "Seller_desc" : "Seller";
            ViewBag.DateSortParm = SortOrder == "Date" ? "Date_desc" : "Date";

            //Querystring
            var note = from slr in db.SellerNotes
                       join ur in db.Users on slr.SellerID equals ur.ID
                       where (slr.Status == 7 || slr.Status == 8) && slr.IsActive == true && ur.IsActive == true
                       select new AdminNotesUnderReviewViewModel { sellNotes = slr, user = ur };

            AdminNotesUnderReviewViewModel notesReview = new AdminNotesUnderReviewViewModel();

            //fetch record based on member id
            if(memberId != 0)
            {
                note = note.Where(x => x.sellNotes.SellerID == memberId && x.sellNotes.Status == 8);
            }

            //pass the sellername list 
            ViewBag.sellerNameList = note.Select(x => x.user.FirstName).OrderBy(x => x).Distinct().ToList();

            //fetch record based on seller name
            if (sellerName != null)
            {
                note = note.Where(x => x.user.FirstName.Contains(sellerName));
            }

            //searching
            if(NUReview_search != null)
            {
                note = note.Where(x => x.sellNotes.Title.Contains(NUReview_search) ||
                                    x.sellNotes.NoteCategories.Name.Contains(NUReview_search) ||
                                    x.sellNotes.ReferenceData.Value.Contains(NUReview_search) ||
                                    x.user.FirstName.Contains(NUReview_search));
            }

            //sorting
            switch (SortOrder)
            {
                case "title_desc":
                    note = note.OrderByDescending(s => s.sellNotes.Title);
                    break;
                case "Title":
                    note = note.OrderBy(s => s.sellNotes.Title);
                    break;
                case "category_desc":
                    note = note.OrderByDescending(s => s.sellNotes.NoteCategories.Name);
                    break;
                case "Category":
                    note = note.OrderBy(s => s.sellNotes.NoteCategories.Name);
                    break;
                case "Status_desc":
                    note = note.OrderByDescending(s => s.sellNotes.ReferenceData.Value);
                    break;
                case "Status":
                    note = note.OrderBy(s => s.sellNotes.ReferenceData.Value);
                    break;
                case "Seller_desc":
                    note = note.OrderByDescending(s => s.user.FirstName);
                    break;
                case "Seller":
                    note = note.OrderBy(s => s.user.FirstName);
                    break;                
                case "Date_desc":
                    note = note.OrderByDescending(s => s.sellNotes.CreatedDate);
                    break;
                case "Date":
                    note = note.OrderBy(s => s.sellNotes.CreatedDate);
                    break;
                default:
                    note = note.OrderByDescending(s => s.sellNotes.CreatedDate);
                    break;
            }

            //pagination
            var pager = new Pager(note.Count(), NUR_page, 10);
            ViewBag.currentPage = pager.CurrentPage;
            ViewBag.endPage = pager.EndPage;
            ViewBag.startpage = pager.StartPage;
            ViewBag.totalPage = note.Count();

            note = note.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);

            return View(note);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult ChangeStatus(int noteid , string value)
        {
            Users user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            var result = db.SellerNotes.Where(x => x.ID == noteid && x.IsActive == true).FirstOrDefault();

            //approved
            if (value == "approved")
            {                
                result.Status = 9;
                result.ActionedBy = user.ID;
                result.PublishedDate = DateTime.Now;
                result.ModifiedDate = DateTime.Now;
                result.ModifiedBy = user.ID;
                db.SaveChanges();
            }           
            
            //submitted for review  to inreview 
            if(value == "inreview")
            {
                result.Status = 8;
                result.ModifiedDate = DateTime.Now;
                result.ModifiedBy = user.ID;
                db.SaveChanges();
            }

            return RedirectToAction("UnderReviewNotes");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult rejectNote(FormCollection form)
        {
            Users user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            int noteid = Convert.ToInt32(form["noteid"]);
            var result = db.SellerNotes.Where(x => x.ID == noteid).FirstOrDefault();

            result.Status = 10;
            result.ActionedBy = user.ID;
            result.AdminRemarks = form["remarkReject"];
            result.ModifiedDate = DateTime.Now;
            result.ModifiedBy = user.ID;

            db.SaveChanges();

            return RedirectToAction("UnderReviewNotes");
        }

    }
}