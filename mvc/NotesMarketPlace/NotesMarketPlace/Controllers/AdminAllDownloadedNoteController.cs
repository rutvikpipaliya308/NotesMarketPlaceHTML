using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Models;

namespace NotesMarketPlace.Controllers
{
    [RoutePrefix("Admin")]
    public class AdminAllDownloadedNoteController : Controller
    {
        readonly NotesMarketPlaceEntities db;
        public AdminAllDownloadedNoteController()
        {
            db = new NotesMarketPlaceEntities();
        }

        // GET: AdminAllDownloadedNote
        [HttpGet]
        [Route("Downloaded-Notes")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public ActionResult AllDownloadedNote(string SortOrder, string ADNotes_search, string sellerName, string buyerName, string Note, int memberId = 0, int noteid = 0, int ADNotes_page = 1)
        {
            ViewBag.Notes = "active";
            ViewBag.downloaded = "active";
            ViewBag.MemberID = memberId;
            ViewBag.NoteID = noteid;            

            //For sortorder
            ViewBag.TitleSortParm = SortOrder == "Title" ? "title_desc" : "Title";
            ViewBag.CategorySortParm = SortOrder == "Category" ? "category_desc" : "Category";
            ViewBag.BuyerSortParm = SortOrder == "Buyer" ? "Buyer_desc" : "Buyer";
            ViewBag.SellerSortParm = SortOrder == "Seller" ? "Seller_desc" : "Seller";
            ViewBag.SellTypeSortParm = SortOrder == "SellType" ? "SellType_desc" : "SellType";
            ViewBag.PriceSortParm = SortOrder == "Price" ? "Price_desc" : "Price";
            ViewBag.DateSortParm = SortOrder == "Date" ? "Date_desc" : "Date";

            //Query string
            var notes = from dwl in db.Downloads
                        join sur in db.Users on dwl.Seller equals sur.ID
                        join bur in db.Users on dwl.Downloader equals bur.ID
                        where
                        dwl.IsSellerHasAllowedDownload == true && dwl.IsActive == true && dwl.IsAttachmentDownloaded == true &&
                        sur.IsActive == true && bur.IsActive == true
                        select new AdminAllDownloadedViewModel { download = dwl, buyer = bur, seller = sur };

            //show all downloads for any note
            if(noteid != 0)
            {
                notes = notes.Where(x => x.download.NoteID == noteid && x.download.IsActive == true);
            }

            //show all downloads for any user
            if(memberId != 0)
            {
                notes = notes.Where(x => x.download.Downloader == memberId && x.download.IsActive == true);
            }

            //fetch the data
            ViewBag.sellerNameList = notes.Select(x => x.seller.FirstName).OrderBy(x => x).Distinct().ToList();
            ViewBag.buyerNamelist = notes.Select(x => x.buyer.FirstName).OrderBy(x => x).Distinct().ToList();
            ViewBag.noteNameList = notes.Select(x => x.download.NoteTitle).OrderBy(x => x).Distinct().ToList();

            //filter base on names
            if (sellerName != null)
            {
                notes = notes.Where(x => x.seller.FirstName.Contains(sellerName));
            }
            if(buyerName != null)
            {
                notes = notes.Where(x => x.buyer.FirstName.Contains(buyerName));
            }
            if(Note != null)
            {
                notes = notes.Where(x => x.download.NoteTitle.Contains(Note));
            }

            //Search
            if (ADNotes_search != null)
            {
                notes = notes.Where(x => x.download.NoteTitle.Contains(ADNotes_search) ||
                                        x.download.NoteCategory.Contains(ADNotes_search) ||
                                        x.buyer.FirstName.Contains(ADNotes_search) ||
                                        x.seller.FirstName.Contains(ADNotes_search) ||
                                        x.download.PurchasedPrice.ToString().Contains(ADNotes_search) ||
                                        x.download.IsPaid.ToString().Contains(ADNotes_search) ||
                                        x.download.AttachmentDownloadedDate.ToString().Contains(ADNotes_search));
            }

            //sorting
            switch (SortOrder)
            {
                case "title_desc":
                    notes = notes.OrderByDescending(s => s.download.NoteTitle);
                    break;
                case "Title":
                    notes = notes.OrderBy(s => s.download.NoteTitle);
                    break;
                case "category_desc":
                    notes = notes.OrderByDescending(s => s.download.NoteCategory);
                    break;
                case "Category":
                    notes = notes.OrderBy(s => s.download.NoteCategory);
                    break;
                case "Buyer_desc":
                    notes = notes.OrderByDescending(s => s.buyer.FirstName);
                    break;
                case "Buyer":
                    notes = notes.OrderBy(s => s.buyer.FirstName);
                    break;
                case "Seller_desc":
                    notes = notes.OrderByDescending(s => s.seller.FirstName);
                    break;
                case "Seller":
                    notes = notes.OrderBy(s => s.seller.FirstName);
                    break;
                case "SellType_desc":
                    notes = notes.OrderByDescending(s => s.download.IsPaid);
                    break;
                case "SellType":
                    notes = notes.OrderBy(s => s.download.IsPaid);
                    break;
                case "Price_desc":
                    notes = notes.OrderByDescending(s => s.download.PurchasedPrice);
                    break;
                case "Price":
                    notes = notes.OrderBy(s => s.download.PurchasedPrice);
                    break;
                case "Date_desc":
                    notes = notes.OrderByDescending(s => s.download.AttachmentDownloadedDate);
                    break;
                case "Date":
                    notes = notes.OrderBy(s => s.download.AttachmentDownloadedDate);
                    break;
                default:
                    notes = notes.OrderByDescending(s => s.download.AttachmentDownloadedDate);
                    break;
            }

            //pagination
            var pager = new Pager(notes.Count(), ADNotes_page, 10);
            ViewBag.currentPage = pager.CurrentPage;
            ViewBag.endPage = pager.EndPage;
            ViewBag.startpage = pager.StartPage;
            ViewBag.totalPage = notes.Count();

            notes = notes.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);

            return View(notes);
                        
        }
    }
}