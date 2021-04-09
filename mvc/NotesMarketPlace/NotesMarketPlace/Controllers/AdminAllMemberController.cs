using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Models;

namespace NotesMarketPlace.Controllers
{
    [RoutePrefix("Admin")]
    public class AdminAllMemberController : Controller
    {
        readonly NotesMarketPlaceEntities db;
        public AdminAllMemberController()
        {
            db = new NotesMarketPlaceEntities();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("AllMember")]
        public ActionResult AllMember(string SortOrder, string AllMember_search, int AllMember_page = 1)
        {
            ViewBag.Members = "active";

            //sort order
            ViewBag.FirstNameSortParm = SortOrder == "FirstName" ? "FirstName_desc" : "FirstName";
            ViewBag.LastNameSortParm = SortOrder == "LastName" ? "LastName_desc" : "LastName";
            ViewBag.EmailSortParm = SortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.JDateSortParm = SortOrder == "JDate" ? "JDate_desc" : "JDate";
            ViewBag.UnderReviewNoteSortParm = SortOrder == "UnderReviewNote" ? "UnderReviewNote_desc" : "UnderReviewNote";
            ViewBag.PublishedNoteSortParm = SortOrder == "PublishedNote" ? "PublishedNote_desc" : "PublishedNote";
            ViewBag.DownloadedNoteSortParm = SortOrder == "DownloadedNote" ? "DownloadedNote_desc" : "DownloadedNote";
            ViewBag.TotalExpensesSortParm = SortOrder == "TotalExpenses" ? "TotalExpenses_desc" : "TotalExpenses";
            ViewBag.TotalEarningSortParm = SortOrder == "TotalEarning" ? "TotalEarning_desc" : "TotalEarning";

            AdminAllMemberViewModel AllMember = new AdminAllMemberViewModel();

            //Query string 
            var allUsers = db.Users.Where(x => x.IsEmailVerified == true && x.IsActive == true && x.RoleID == 2);

            //searching
            if(AllMember_search != null)
            {
                allUsers = allUsers.Where(x => x.FirstName.Contains(AllMember_search) ||
                                                x.LastName.Contains(AllMember_search) ||
                                                x.Email.Contains(AllMember_search) ||
                                                x.CreatedDate.ToString().Contains(AllMember_search));
            }

            //sorting
            switch (SortOrder)
            {
                case "FirstName_desc":
                    allUsers = allUsers.OrderByDescending(s => s.FirstName);
                    break;
                case "FirstName":
                    allUsers = allUsers.OrderBy(s => s.FirstName);
                    break;
                case "LastName_desc":
                    allUsers = allUsers.OrderByDescending(s => s.LastName);
                    break;
                case "LastName":
                    allUsers = allUsers.OrderBy(s => s.LastName);
                    break;
                case "Email_desc":
                    allUsers = allUsers.OrderByDescending(s => s.Email);
                    break;
                case "Email":
                    allUsers = allUsers.OrderBy(s => s.Email);
                    break;                               
                case "JDate_desc":
                    allUsers = allUsers.OrderByDescending(s => s.CreatedDate);
                    break;
                case "JDate":
                    allUsers = allUsers.OrderBy(s => s.CreatedDate);
                    break;
                default:
                    allUsers = allUsers.OrderByDescending(s => s.CreatedDate);
                    break;
            }

            List<allmember> members = new List<allmember>();

            //calculate the data for TOTAL underreview, published, downloaded note & total expenses, earning 
            foreach(var item in allUsers)
            {
                decimal? TotalExpenses;
                decimal? TotalEarning;

                int totalInReviewNotes = db.SellerNotes.Where(x => x.SellerID == item.ID && x.Status == 8 && x.IsActive == true).Count();
                int totalPublishedNotes = db.SellerNotes.Where(x => x.SellerID == item.ID && x.Status == 9 && x.IsActive == true).Count();
                int totalDownloadedNotes = db.Downloads.Where(x => x.Downloader == item.ID &&
                                                                    x.IsSellerHasAllowedDownload == true &&
                                                                    x.IsAttachmentDownloaded == true &&
                                                                    x.IsActive == true).Count();

                //calc. total expenses
                var mynotes = db.Downloads.Where(x => x.Downloader == item.ID && x.IsPaid == true && x.IsSellerHasAllowedDownload == true);
                TotalExpenses = mynotes.Sum(x => x.PurchasedPrice);
                if (mynotes.Count() == 0)
                {
                    TotalExpenses = 0;
                }

                //calc. total earning
                var myearning = db.Downloads.Where(x => x.Seller == item.ID &&
                                                        x.IsActive == true && 
                                                        x.IsSellerHasAllowedDownload == true && 
                                                        x.IsPaid == true);
                TotalEarning = myearning.Sum(x => x.PurchasedPrice);
                if(myearning.Count() == 0)
                {
                    TotalEarning = 0;
                }

                //add data in model
                allmember member = new allmember()
                {
                    user = item,
                    underReviewNote = totalInReviewNotes,
                    publichedNote = totalPublishedNotes,
                    downloadedNote = totalDownloadedNotes,
                    totalExpenses = TotalExpenses,
                    totalEarning = TotalEarning
                };

                members.Add(member);
            }
            
            //sorting
            switch (SortOrder)
            {
                case "UnderReviewNote_desc":
                    members = members.OrderByDescending(s => s.underReviewNote).ToList();
                    break;
                case "UnderReviewNote":
                    members = members.OrderBy(s => s.underReviewNote).ToList();
                    break;
                case "PublishedNote_desc":
                    members = members.OrderByDescending(s => s.publichedNote).ToList();
                    break;
                case "PublishedNote":
                    members = members.OrderBy(s => s.publichedNote).ToList();
                    break;
                case "DownloadedNote_desc":
                    members = members.OrderByDescending(s => s.downloadedNote).ToList();
                    break;
                case "DownloadedNote":
                    members = members.OrderBy(s => s.downloadedNote).ToList();
                    break;
                case "TotalExpenses_desc":
                    members = members.OrderByDescending(s => s.totalExpenses).ToList();
                    break;
                case "TotalExpenses":
                    members = members.OrderBy(s => s.totalExpenses).ToList();
                    break;
                case "TotalEarning_desc":
                    members = members.OrderByDescending(s => s.totalEarning).ToList();
                    break;
                case "TotalEarning":
                    members = members.OrderBy(s => s.totalEarning).ToList();
                    break;                
                default:
                    members = members.ToList();
                break;
            }

            //assign the data model
            AllMember.Members = members;

            //pagination
            var pager = new Pager(allUsers.Count(), AllMember_page, 10);
            ViewBag.currentPage = pager.CurrentPage;
            ViewBag.endPage = pager.EndPage;
            ViewBag.startpage = pager.StartPage;
            ViewBag.totalPage = allUsers.Count();

            AllMember.Members = members.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);

            return View(AllMember);
        }


        [HttpGet]
        [Authorize(Roles = "Admin, SUperAdmin")]
        [Route("DeactiveUser")]
        public ActionResult DeactiveUser(int userId)
        {
            var user = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();

            //deactive from user table
            var deactiveUser = db.Users.Where(x => x.ID == userId && x.IsActive == true).FirstOrDefault();
            deactiveUser.IsActive = false;

            //deactive data from seller table 
            var deactiveSeller = db.SellerNotes.Where(x => x.SellerID == userId && x.IsActive == true);
            foreach(var item in deactiveSeller)
            {
                item.IsActive = false;
                item.ActionedBy = user.ID;
                item.ModifiedDate = DateTime.Now;
                item.ModifiedBy = user.ID;
                var attachment = db.SellerNotesAttachements.Where(x => x.NoteID == item.ID & x.IsActive == true);
                foreach(var note in attachment)
                {
                    note.IsActive = false;
                }
            }

            //deactive data from downloads table
            var downloadDeactive = db.Downloads.Where(x => x.Downloader == userId && x.IsActive == true);
            foreach(var item in downloadDeactive)
            {
                item.ModifiedBy = user.ID;
                item.ModifiedDate = DateTime.Now;
                item.IsActive = false;
            }

            //deactive data from review table
            var reviewDeactive = db.SellerNotesReviews.Where(x => x.ReviewedByID == userId && x.IsActive == true);
            foreach(var item in reviewDeactive)
            {
                item.ModifiedBy = user.ID;
                item.ModifiedDate = DateTime.Now;
                item.IsActive = false;
            }            

            db.SaveChanges();

            return RedirectToAction("AllMember");
        }


    }
}