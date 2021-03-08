using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Models;

namespace NotesMarketPlace.Controllers
{
    public class SearchNoteController : Controller
    {
        // GET: SearchNote
        [HttpGet]
        public ActionResult NoteDetails(int id)
        {
            using (var db = new NotesMarketPlaceEntities())
            {
                SellerNotes notedetail = new SellerNotes();
                
                notedetail = db.SellerNotes.FirstOrDefault(x => x.ID == id);
                return View(notedetail);
            }
           
        }

        public FileResult Download(int noteId, int userId)
        {              
            using(var db = new NotesMarketPlaceEntities())
            {
                //Download the file 
                string filename = db.SellerNotesAttachements.FirstOrDefault(x => x.NoteID == noteId).FileName;
                string notesattachementpath = "~/Members/" + userId + "/" + noteId + "/Attachements/";
                string attachmentspath = notesattachementpath + filename;
                string fullpath = System.IO.Path.Combine(notesattachementpath, filename);

                //Save data in database 
                SellerNotes obj = new SellerNotes();
                NoteCategories cat = new NoteCategories();
                int category = db.SellerNotes.FirstOrDefault(x => x.ID == noteId).Category;
                string title = db.SellerNotes.FirstOrDefault(x => x.ID == noteId).Title;

                Downloads downloadnotedetail = new Downloads();

                downloadnotedetail.NoteID = noteId;
                downloadnotedetail.Seller = userId;
                downloadnotedetail.Downloader = userId;
                downloadnotedetail.IsSellerHasAllowedDownload = false;
                downloadnotedetail.AttachmentPath = attachmentspath;
                downloadnotedetail.IsAttachmentDownloaded = true;
                downloadnotedetail.AttachmentDownloadedDate = DateTime.Now;
                downloadnotedetail.NoteTitle = title;
                downloadnotedetail.NoteCategory = db.NoteCategories.FirstOrDefault(x => x.ID == category).Name;
                downloadnotedetail.CreatedDate = DateTime.Now;
                downloadnotedetail.IsActive = true;

                bool paid = db.SellerNotes.FirstOrDefault(x => x.ID == noteId).IsPaid;                

                if (paid)
                {
                    downloadnotedetail.IsPaid = true;
                    downloadnotedetail.PurchasedPrice = db.SellerNotes.FirstOrDefault(x => x.ID == noteId).SellingPrice;                    
                }
                else
                {
                    downloadnotedetail.IsPaid = false;
                    downloadnotedetail.PurchasedPrice = 0;
                }

                db.Downloads.Add(downloadnotedetail);
                db.SaveChanges();


                return File(fullpath, "text/plain", filename);
            }
          
        }

        readonly NotesMarketPlaceEntities context;

        public SearchNoteController()
        {
            context = new NotesMarketPlaceEntities();
        }

        [HttpGet]
        [Authorize]
        public ActionResult BuyerRequest(string BR_search, string sortOrder, int BuyerRequest_page = 1)
        {

            ViewBag.TitleSortParm = sortOrder == "Title" ? "title_desc" : "Title";
            ViewBag.CategorySortParm = sortOrder == "Category" ? "category_desc" : "Category";
            ViewBag.BuyerNameSortParm = sortOrder == "BuyerName" ? "BuyerName_desc" : "BuyerName";
            ViewBag.PhoneSortParm = sortOrder == "Phone" ? "Phone_desc" : "Phone";
            ViewBag.SellTypeSortParm = sortOrder == "SellType" ? "SellType_desc" : "SellType";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";

            ViewBag.pageNumber = BuyerRequest_page;
            BuyerRequestViewModel buyer = new BuyerRequestViewModel();
            var user = context.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();

             var buyerrequest = from dwl in context.Downloads
                                                            join up in context.UserProfile on dwl.Downloader equals up.UserID
                                                            join ur in context.Users on dwl.Downloader equals ur.ID
                                                            where dwl.Seller == user.ID && dwl.IsSellerHasAllowedDownload == false
                                                            select new BuyerRequestViewModel { Downloadstbl = dwl, UserProfiletbl = up, Userstbl = ur };

            if (BR_search != null)
            {
                buyerrequest = buyerrequest.Where(
                    x => x.Downloadstbl.NoteTitle.Contains(BR_search) || 
                         x.Downloadstbl.NoteCategory.Contains(BR_search) || 
                         x.Userstbl.Email.Contains(BR_search) || 
                         x.UserProfiletbl.PhoneNumber.Contains(BR_search) ||
                         x.Downloadstbl.IsPaid.ToString().Contains(BR_search) ||
                         x.Downloadstbl.PurchasedPrice.ToString().Contains(BR_search));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    buyerrequest = buyerrequest.OrderByDescending(s => s.Downloadstbl.NoteTitle);
                    break;
                case "Title":
                    buyerrequest = buyerrequest.OrderBy(s => s.Downloadstbl.NoteTitle);
                    break;
                case "category_desc":
                    buyerrequest = buyerrequest.OrderByDescending(s => s.Downloadstbl.NoteCategory);
                    break;
                case "Category":
                    buyerrequest = buyerrequest.OrderBy(s => s.Downloadstbl.NoteCategory);
                    break;
                case "BuyerName_desc":
                    buyerrequest = buyerrequest.OrderByDescending(s => s.Userstbl.Email);
                    break;
                case "BuyerName":
                    buyerrequest = buyerrequest.OrderBy(s => s.Userstbl.Email);
                    break;
                case "Phone_desc":
                    buyerrequest = buyerrequest.OrderByDescending(s => s.UserProfiletbl.PhoneNumber);
                    break;
                case "Phone":
                    buyerrequest = buyerrequest.OrderBy(s => s.UserProfiletbl.PhoneNumber);
                    break;
                case "SellType_desc":
                    buyerrequest = buyerrequest.OrderByDescending(s => s.Downloadstbl.IsPaid);
                    break;
                case "SellType":
                    buyerrequest = buyerrequest.OrderBy(s => s.Downloadstbl.IsPaid);
                    break;
                case "Price_desc":
                    buyerrequest = buyerrequest.OrderByDescending(s => s.Downloadstbl.PurchasedPrice);
                    break;
                case "Price":
                    buyerrequest = buyerrequest.OrderBy(s => s.Downloadstbl.PurchasedPrice);
                    break;
                case "Date_desc":
                    buyerrequest = buyerrequest.OrderByDescending(s => s.Downloadstbl.AttachmentDownloadedDate);
                    break;
                case "Date":
                    buyerrequest = buyerrequest.OrderBy(s => s.Downloadstbl.AttachmentDownloadedDate);
                    break;
                default:
                    buyerrequest = buyerrequest.OrderByDescending(s => s.Downloadstbl.AttachmentDownloadedDate);
                    break;
            }

            ViewBag.srno = BuyerRequest_page;
            ViewBag.TotalBuyerRequestPage = Math.Ceiling(buyerrequest.Count() / 5.0);
            buyerrequest = buyerrequest.OrderBy(s => s.Downloadstbl.ID).Skip((BuyerRequest_page - 1) * 5).Take(5);

            return View(buyerrequest);
        }
    }
}