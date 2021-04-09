using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Models;

namespace NotesMarketPlace.Controllers
{
    [Route("Admin")]
    public class AdminPublishedNoteController : Controller
    {
        readonly NotesMarketPlaceEntities db;
        public AdminPublishedNoteController()
        {
            db = new NotesMarketPlaceEntities();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("PublishedNote")]
        public ActionResult PublishedNote(string SortOrder, string PubNote_search, string sellerName, int memberId = 0, int PubNote_page = 1)
        {
            ViewBag.Notes = "active";
            ViewBag.MemberID = memberId;
            ViewBag.published = "active";

            //dortorder
            ViewBag.TitleSortParm = SortOrder == "Title" ? "title_desc" : "Title";
            ViewBag.CategorySortParm = SortOrder == "Category" ? "category_desc" : "Category";
            ViewBag.SellTypeSortParm = SortOrder == "SellType" ? "SellType_desc" : "SellType";
            ViewBag.PriceSortParm = SortOrder == "Price" ? "Price_desc" : "Price";
            ViewBag.NameSortParm = SortOrder == "Name" ? "Name_desc" : "Name";
            ViewBag.ApprovedSortParm = SortOrder == "Approved" ? "Approved_desc" : "Approved";
            ViewBag.TotalDownloadSortParm = SortOrder == "TotalDownload" ? "TotalDownload_desc" : "TotalDownload";
            ViewBag.DateSortParm = SortOrder == "Date" ? "Date_desc" : "Date";

            //Query string
            var pnote = from sellnote in db.SellerNotes
                       join usr in db.Users on sellnote.SellerID equals usr.ID
                       join apuser in db.Users on sellnote.ActionedBy equals apuser.ID
                       where sellnote.Status == 9 &&
                       sellnote.IsActive == true &&
                       usr.IsActive == true
                       orderby sellnote.PublishedDate descending
                       select new APublishedNote { psnotes = sellnote, user = usr, auser = apuser};
                       
            //fetch data based on member id 
            if(memberId != 0)
            {
                pnote = pnote.Where(x => x.psnotes.SellerID == memberId);
            }

            //pass the seller data 
            ViewBag.sellerNameList = pnote.Select(x => x.user.FirstName).OrderBy(x => x).Distinct().ToList();

            //filter data on sellr name
            if (sellerName != null)
            {
                pnote = pnote.Where(x => x.user.FirstName.Contains(sellerName));
            }

            //searching
            if (PubNote_search != null)
            {
                pnote = pnote.Where(x => x.psnotes.Title.Contains(PubNote_search) ||
                                    x.psnotes.NoteCategories.Name.Contains(PubNote_search) ||
                                    x.psnotes.ReferenceData.Value.Contains(PubNote_search) ||
                                    x.user.FirstName.Contains(PubNote_search) ||
                                    x.auser.FirstName.Contains(PubNote_search));
            }

            //sorting
            switch (SortOrder)
            {
                case "title_desc":
                    pnote = pnote.OrderByDescending(s => s.psnotes.Title);
                    break;
                case "Title":
                    pnote = pnote.OrderBy(s => s.psnotes.Title);
                    break;
                case "category_desc":
                    pnote = pnote.OrderByDescending(s => s.psnotes.NoteCategories.Name);
                    break;
                case "Category":
                    pnote = pnote.OrderBy(s => s.psnotes.NoteCategories.Name);
                    break;
                case "SellType_desc":
                    pnote = pnote.OrderByDescending(s => s.psnotes.ReferenceData.Value);
                    break;
                case "SellType":
                    pnote = pnote.OrderBy(s => s.psnotes.ReferenceData.Value);
                    break;
                case "Price_desc":
                    pnote = pnote.OrderByDescending(s => s.psnotes.SellingPrice);
                    break;
                case "Price":
                    pnote = pnote.OrderBy(s => s.psnotes.SellingPrice);
                    break;
                case "Name_desc":
                    pnote = pnote.OrderByDescending(s => s.user.FirstName);
                    break;
                case "Name":
                    pnote = pnote.OrderBy(s => s.user.FirstName);
                    break;
                case "Approved_desc":
                    pnote = pnote.OrderByDescending(s => s.auser.FirstName);
                    break;
                case "Approved":
                    pnote = pnote.OrderBy(s => s.auser.FirstName);
                    break;
                case "Date_desc":
                    pnote = pnote.OrderByDescending(s => s.psnotes.PublishedDate);
                    break;
                case "Date":
                    pnote = pnote.OrderBy(s => s.psnotes.PublishedDate);
                    break;                
                default:
                    pnote = pnote.OrderByDescending(s => s.psnotes.PublishedDate);
                    break;
            }

            List<AdminPublishedNoteViewModel> noteList = new List<AdminPublishedNoteViewModel>();

            //total downloaded 
            foreach (var total in pnote)
            {
                int totalcount = db.Downloads.Where(x => x.NoteID == total.psnotes.ID && x.IsSellerHasAllowedDownload == true).Count();                               
               
                AdminPublishedNoteViewModel dash = new AdminPublishedNoteViewModel()
                {
                    pNote = total,
                    TotalDownload = totalcount                    
                };

                noteList.Add(dash);
            }

            if (SortOrder == "TotalDownload_desc")
            {
                noteList = noteList.OrderByDescending(x => x.TotalDownload).ToList();
            }
            if (SortOrder == "TotalDownload")
            {
                noteList = noteList.OrderBy(x => x.TotalDownload).ToList();
            }

            //pagination
            var pager = new Pager(pnote.Count(), PubNote_page, 10);
            ViewBag.currentPage = pager.CurrentPage;
            ViewBag.endPage = pager.EndPage;
            ViewBag.startpage = pager.StartPage;
            ViewBag.totalPage = pnote.Count();

            IEnumerable<AdminPublishedNoteViewModel> result = noteList.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);

            return View(result);          
        }
    }
}