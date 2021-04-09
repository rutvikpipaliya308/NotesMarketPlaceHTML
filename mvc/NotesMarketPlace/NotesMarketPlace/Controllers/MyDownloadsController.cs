using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Models;

namespace NotesMarketPlace.Controllers
{
    public class MyDownloadsController : Controller
    {
        readonly NotesMarketPlaceEntities db;
        public MyDownloadsController()
        {
            db = new NotesMarketPlaceEntities();
        }
        
        [HttpGet]
        [Authorize(Roles = "Member")]
        [Route("MyDownloads")]
        [OutputCache(Duration = 0)]
        public ActionResult MyDownloads(String sortOrder, string MD_search, int MyDownloads_page = 1)
        {
            ViewBag.navClass = "white-nav";

            ViewBag.TitleSortParm = sortOrder == "Title" ? "title_desc" : "Title";
            ViewBag.CategorySortParm = sortOrder == "Category" ? "category_desc" : "Category";
            ViewBag.BuyerNameSortParm = sortOrder == "Buyeremail" ? "Buyeremail_desc" : "Buyeremail";            
            ViewBag.SellTypeSortParm = sortOrder == "SellType" ? "SellType_desc" : "SellType";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";            

            var user = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();

            //Query string
            var mydownloads = from dwl in db.Downloads                               
                               join ur in db.Users on dwl.Downloader equals ur.ID
                               where 
                               dwl.Downloader == user.ID && 
                               dwl.IsSellerHasAllowedDownload == true &&
                               dwl.IsActive == true
                               select new MyDownloadsViewModel { mydownloadtbl = dwl, userstbl = ur };

            //Search
            if (MD_search != null)
            {
                mydownloads = mydownloads.Where(
                    x => x.mydownloadtbl.NoteTitle.Contains(MD_search) ||
                         x.mydownloadtbl.NoteCategory.Contains(MD_search) ||
                         x.userstbl.Email.Contains(MD_search) ||                         
                         x.mydownloadtbl.IsPaid.ToString().Contains(MD_search) ||
                         x.mydownloadtbl.PurchasedPrice.ToString().Contains(MD_search) ||
                         x.mydownloadtbl.AttachmentDownloadedDate.ToString().Contains(MD_search));
            }

            //sorting
            switch (sortOrder)
            {
                case "title_desc":
                    mydownloads = mydownloads.OrderByDescending(s => s.mydownloadtbl.NoteTitle);
                    break;
                case "Title":
                    mydownloads = mydownloads.OrderBy(s => s.mydownloadtbl.NoteTitle);
                    break;
                case "category_desc":
                    mydownloads = mydownloads.OrderByDescending(s => s.mydownloadtbl.NoteCategory);
                    break;
                case "Category":
                    mydownloads = mydownloads.OrderBy(s => s.mydownloadtbl.NoteCategory);
                    break;
                case "Buyeremail_desc":
                    mydownloads = mydownloads.OrderByDescending(s => s.userstbl.Email);
                    break;
                case "Buyeremail":
                    mydownloads = mydownloads.OrderBy(s => s.userstbl.Email);
                    break;              
                case "SellType_desc":
                    mydownloads = mydownloads.OrderByDescending(s => s.mydownloadtbl.IsPaid);
                    break;
                case "SellType":
                    mydownloads = mydownloads.OrderBy(s => s.mydownloadtbl.IsPaid);
                    break;
                case "Price_desc":
                    mydownloads = mydownloads.OrderByDescending(s => s.mydownloadtbl.PurchasedPrice);
                    break;
                case "Price":
                    mydownloads = mydownloads.OrderBy(s => s.mydownloadtbl.PurchasedPrice);
                    break;
                case "Date_desc":
                    mydownloads = mydownloads.OrderByDescending(s => s.mydownloadtbl.AttachmentDownloadedDate);
                    break;
                case "Date":
                    mydownloads = mydownloads.OrderBy(s => s.mydownloadtbl.AttachmentDownloadedDate);
                    break;
                default:
                    mydownloads = mydownloads.OrderByDescending(s => s.mydownloadtbl.AttachmentDownloadedDate);
                    break;
            }

            //Pagination
            var pager = new Pager(mydownloads.Count(), MyDownloads_page, 10);
            ViewBag.currentPage = pager.CurrentPage;
            ViewBag.endPage = pager.EndPage;
            ViewBag.startpage = pager.StartPage;
                        
            ViewBag.srno = MyDownloads_page;
            ViewBag.TotalMyDownloadsPage = Math.Ceiling(mydownloads.Count() / 10.0);
            mydownloads = mydownloads.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);

            return View(mydownloads);
        }

        [HttpPost]
        [Authorize(Roles = "Member")]
        [ValidateAntiForgeryToken]
        [OutputCache(Duration = 0)]
        public ActionResult NoteReview(FormCollection form)
        {
            var user = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();

            SellerNotesReviews reviews = new SellerNotesReviews();

            int NoteID = Convert.ToInt32(form["noteid"]);
            if (!db.SellerNotesReviews.Any(x => x.NoteID == NoteID && x.ReviewedByID == user.ID))
            {
                reviews.NoteID = Convert.ToInt32(form["noteid"]);
                reviews.AgainstDownloadsID = Convert.ToInt32(form["downloadid"]);
                reviews.ReviewedByID = user.ID;
                reviews.Ratings = Convert.ToDecimal(form["rate"]);
                reviews.Comments = form["review"];
                reviews.CreatedDate = DateTime.Now;
                reviews.CreatedBy = user.ID;
                reviews.IsActive = true;

                db.SellerNotesReviews.Add(reviews);
                db.SaveChanges();
            }
            else
            {
                var review = db.SellerNotesReviews.FirstOrDefault(x => x.NoteID == NoteID && x.ReviewedByID == user.ID);
                review.AgainstDownloadsID = Convert.ToInt32(form["downloadid"]);
                review.Ratings = Convert.ToDecimal(form["rate"]);
                review.Comments = form["review"];
                review.ModifiedDate = DateTime.Now;
                review.ModifiedBy = user.ID;

                db.SaveChanges();
            }

            return RedirectToAction("MyDownloads");
        }

        [HttpPost]
        [Authorize(Roles = "Member")]
        [ValidateAntiForgeryToken]
        [OutputCache(Duration = 0)]
        public ActionResult ReportSpam(FormCollection form)
        {
            var user = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();

            SellerNotesReportedIssues spamnote = new SellerNotesReportedIssues();
            int NoteID = Convert.ToInt32(form["noteid"]);

            if (!db.SellerNotesReportedIssues.Any(x => x.NoteID == NoteID && x.ReportedByID == user.ID))
            {
                spamnote.NoteID = Convert.ToInt32(form["noteid"]);
                spamnote.AgainstDownloadID = Convert.ToInt32(form["downloadid"]);
                spamnote.ReportedByID = user.ID;
                spamnote.Remarks = form["spamreport"];
                spamnote.CreatedDate = DateTime.Now;
                spamnote.CreatedBy = user.ID;

                db.SellerNotesReportedIssues.Add(spamnote);
                db.SaveChanges();
            }
            else
            {
                var spamreport = db.SellerNotesReportedIssues.FirstOrDefault(x => x.NoteID == NoteID && x.ReportedByID == user.ID);
                spamreport.AgainstDownloadID = Convert.ToInt32(form["downloadid"]);                
                spamreport.Remarks = form["spamreport"];
                spamreport.ModifiedDate = DateTime.Now;
                spamreport.ModifiedBy = user.ID;

                db.SaveChanges();
            }
            return RedirectToAction("MyDownloads");
        }

    }
}