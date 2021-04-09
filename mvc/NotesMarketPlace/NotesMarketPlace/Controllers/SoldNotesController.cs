using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Models;

namespace NotesMarketPlace.Controllers
{
    public class SoldNotesController : Controller
    {

        readonly NotesMarketPlaceEntities db;

        public SoldNotesController()
        {
            db = new NotesMarketPlaceEntities();
        }

        // GET: SoldNotes
        [HttpGet]
        [Authorize]
        [Route("MySoldNotes")]
        [OutputCache(Duration = 0)]
        public ActionResult MySoldNotes(String sortOrder, string SN_search, int MySoldNotes_page = 1)
        {
            ViewBag.navClass = "white-nav";

            ViewBag.TitleSortParm = sortOrder == "Title" ? "title_desc" : "Title";
            ViewBag.CategorySortParm = sortOrder == "Category" ? "category_desc" : "Category";
            ViewBag.BuyerNameSortParm = sortOrder == "Buyeremail" ? "Buyeremail_desc" : "Buyeremail";
            ViewBag.SellTypeSortParm = sortOrder == "SellType" ? "SellType_desc" : "SellType";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";

            ViewBag.pageNumber = MySoldNotes_page;

            var user = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();

            var mysoldnotes = from dwl in db.Downloads
                              join ur in db.Users on dwl.Downloader equals ur.ID
                              where
                              dwl.Seller == user.ID &&
                              dwl.IsSellerHasAllowedDownload == true &&
                              dwl.IsActive == true
                              select new MySoldNotesViewModel { downloadtbl = dwl, usertbl = ur };

            //searching
            if (SN_search != null)
            {
                mysoldnotes = mysoldnotes.Where(
                    x => x.downloadtbl.NoteTitle.Contains(SN_search) ||
                         x.downloadtbl.NoteCategory.Contains(SN_search) ||
                         x.usertbl.Email.Contains(SN_search) ||                         
                         x.downloadtbl.IsPaid.ToString().Contains(SN_search) ||
                         x.downloadtbl.PurchasedPrice.ToString().Contains(SN_search));
            }

            //sorting
            switch (sortOrder)
            {
                case "title_desc":
                    mysoldnotes = mysoldnotes.OrderByDescending(s => s.downloadtbl.NoteTitle);
                    break;
                case "Title":
                    mysoldnotes = mysoldnotes.OrderBy(s => s.downloadtbl.NoteTitle);
                    break;
                case "category_desc":
                    mysoldnotes = mysoldnotes.OrderByDescending(s => s.downloadtbl.NoteCategory);
                    break;
                case "Category":
                    mysoldnotes = mysoldnotes.OrderBy(s => s.downloadtbl.NoteCategory);
                    break;
                case "Buyeremail_desc":
                    mysoldnotes = mysoldnotes.OrderByDescending(s => s.usertbl.Email);
                    break;
                case "Buyeremail":
                    mysoldnotes = mysoldnotes.OrderBy(s => s.usertbl.Email);
                    break;
                case "SellType_desc":
                    mysoldnotes = mysoldnotes.OrderByDescending(s => s.downloadtbl.IsPaid);
                    break;
                case "SellType":
                    mysoldnotes = mysoldnotes.OrderBy(s => s.downloadtbl.IsPaid);
                    break;
                case "Price_desc":
                    mysoldnotes = mysoldnotes.OrderByDescending(s => s.downloadtbl.PurchasedPrice);
                    break;
                case "Price":
                    mysoldnotes = mysoldnotes.OrderBy(s => s.downloadtbl.PurchasedPrice);
                    break;
                case "Date_desc":
                    mysoldnotes = mysoldnotes.OrderByDescending(s => s.downloadtbl.AttachmentDownloadedDate);
                    break;
                case "Date":
                    mysoldnotes = mysoldnotes.OrderBy(s => s.downloadtbl.AttachmentDownloadedDate);
                    break;
                default:
                    mysoldnotes = mysoldnotes.OrderByDescending(s => s.downloadtbl.AttachmentDownloadedDate);
                    break;
            }

            //pagination
            var pager = new Pager(mysoldnotes.Count(), MySoldNotes_page, 10);
            ViewBag.currentPage = pager.CurrentPage;
            ViewBag.endPage = pager.EndPage;
            ViewBag.startpage = pager.StartPage;

            ViewBag.srno = MySoldNotes_page;
            ViewBag.TotalMySoldNotesPage = Math.Ceiling(mysoldnotes.Count() / 10.0);
            mysoldnotes = mysoldnotes.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);

            return View(mysoldnotes);
        }
    }
}