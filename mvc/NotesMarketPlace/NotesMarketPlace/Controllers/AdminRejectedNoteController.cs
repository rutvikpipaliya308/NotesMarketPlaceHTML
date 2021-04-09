using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Models;

namespace NotesMarketPlace.Controllers
{
    [Route("Admin")]
    public class AdminRejectedNoteController : Controller
    {
        readonly NotesMarketPlaceEntities db;
        public AdminRejectedNoteController()
        {
            db = new NotesMarketPlaceEntities();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("RejectedNote")]
        [OutputCache(Duration = 0)]
        public ActionResult RejectedNote(string SortOrder, string RejNote_search, string sellerName, int RejNote_page = 1)
        {
            ViewBag.Notes = "active";
            ViewBag.reject = "active";

            ViewBag.TitleSortParm = SortOrder == "Title" ? "title_desc" : "Title";
            ViewBag.CategorySortParm = SortOrder == "Category" ? "category_desc" : "Category";
            ViewBag.RejectSortParm = SortOrder == "Reject" ? "Reject_desc" : "Reject";
            ViewBag.SellerSortParm = SortOrder == "Seller" ? "Seller_desc" : "Seller";
            ViewBag.DateSortParm = SortOrder == "Date" ? "Date_desc" : "Date";

            var note = from slr in db.SellerNotes
                       join sur in db.Users on slr.SellerID equals sur.ID
                       join rur in db.Users on slr.ActionedBy equals rur.ID
                       where slr.Status == 10 && slr.IsActive == true && slr.AdminRemarks != null &&
                       sur.IsActive == true && rur.IsActive == true
                       select new AdminRejectedNoteViewModel { reject = slr, rejector = rur, seller = sur };

            //get seller name list
            ViewBag.sellerNameList = note.Select(x => x.seller.FirstName).OrderBy(x => x).Distinct().ToList();

            //fetch data based on sellername
            if (sellerName != null)
            {
                note = note.Where(x => x.seller.FirstName.Contains(sellerName));
            }

            if (RejNote_search != null)
            {
                note = note.Where(x => x.reject.Title.Contains(RejNote_search) ||
                                    x.reject.NoteCategories.Name.Contains(RejNote_search) ||                                    
                                    x.seller.FirstName.Contains(RejNote_search) ||
                                    x.rejector.FirstName.Contains(RejNote_search));
            }

            //sorting
            switch (SortOrder)
            {
                case "title_desc":
                    note = note.OrderByDescending(s => s.reject.Title);
                    break;
                case "Title":
                    note = note.OrderBy(s => s.reject.Title);
                    break;
                case "category_desc":
                    note = note.OrderByDescending(s => s.reject.NoteCategories.Name);
                    break;
                case "Category":
                    note = note.OrderBy(s => s.reject.NoteCategories.Name);
                    break;
                case "Reject_desc":
                    note = note.OrderByDescending(s => s.rejector.FirstName);
                    break;
                case "Reject":
                    note = note.OrderBy(s => s.rejector.FirstName);
                    break;
                case "Seller_desc":
                    note = note.OrderByDescending(s => s.seller.FirstName);
                    break;
                case "Seller":
                    note = note.OrderBy(s => s.seller.FirstName);
                    break;
                case "Date_desc":
                    note = note.OrderByDescending(s => s.reject.CreatedDate);
                    break;
                case "Date":
                    note = note.OrderBy(s => s.reject.CreatedDate);
                    break;
                default:
                    note = note.OrderByDescending(s => s.reject.CreatedDate);
                    break;
            }

            //pagination
            var pager = new Pager(note.Count(), RejNote_page, 10);
            ViewBag.currentPage = pager.CurrentPage;
            ViewBag.endPage = pager.EndPage;
            ViewBag.startpage = pager.StartPage;
            ViewBag.totalPage = note.Count();

            note = note.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);

            return View(note);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [OutputCache(Duration = 0)]
        public ActionResult ApproveNote(int noteid)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            var note = db.SellerNotes.Where(x => x.ID == noteid && x.IsActive == true).FirstOrDefault();

            note.Status = 9;
            note.ActionedBy = user.ID;
            note.PublishedDate = DateTime.Now;
            note.ModifiedDate = DateTime.Now;
            note.ModifiedBy = user.ID;

            db.SaveChanges();

            return RedirectToAction("RejectedNote");
        }
    }
}