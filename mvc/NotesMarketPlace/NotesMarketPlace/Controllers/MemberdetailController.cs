using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Models;

namespace NotesMarketPlace.Controllers
{
    [Route("Admin")]
    public class MemberdetailController : Controller
    {
        readonly NotesMarketPlaceEntities db;
        public MemberdetailController()
        {
            db = new NotesMarketPlaceEntities();
        }

        [Route("MemberDetail/{memberId}")]
        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [OutputCache(Duration = 0)]
        public ActionResult MemberDetail(int memberId, string SortOrder, int MemDetail_page = 1)
        {
            MemberDetailViewModel Member = new MemberDetailViewModel();

            Member.member= db.Users.Where(x => x.ID == memberId && x.IsActive == true && x.RoleID == 2 && x.IsEmailVerified == true).FirstOrDefault();

            Member.memberProfile = db.UserProfile.Where(x => x.UserID == memberId && x.IsActive == true).FirstOrDefault();

            //sortorder
            ViewBag.TitleSortParm = SortOrder == "Title" ? "title_desc" : "Title";
            ViewBag.CategorySortParm = SortOrder == "Category" ? "category_desc" : "Category";
            ViewBag.StatusSortParm = SortOrder == "Status" ? "Status_desc" : "Status";
            ViewBag.TotalDownloadSortParm = SortOrder == "Downloaded" ? "Downloaded_desc" : "Downloaded";
            ViewBag.TotalEarningSortParm = SortOrder == "Earning" ? "Earning_desc" : "Earning";
            ViewBag.DateAddSortParm = SortOrder == "DateAdd" ? "DateAdd_desc" : "DateAdd";
            ViewBag.DatePubSortParm = SortOrder == "DatePub" ? "DatePub_desc" : "DatePub";

            var notes = db.SellerNotes.Where(x => x.SellerID == memberId && x.Status != 6);

            //sorting
            switch (SortOrder)
            {
                case "title_desc":
                    notes = notes.OrderByDescending(s => s.Title);
                    break;
                case "Title":
                    notes = notes.OrderBy(s => s.Title);
                    break;
                case "category_desc":
                    notes = notes.OrderByDescending(s => s.NoteCategories.Name);
                    break;
                case "Category":
                    notes = notes.OrderBy(s => s.NoteCategories.Name);
                    break;
                case "Status_desc":
                    notes = notes.OrderByDescending(s => s.ReferenceData.Value);
                    break;
                case "Status":
                    notes = notes.OrderBy(s => s.ReferenceData.Value);
                    break;
                case "DateAdd_desc":
                    notes = notes.OrderByDescending(s => s.CreatedDate);
                    break;
                case "DateAdd":
                    notes = notes.OrderBy(s => s.CreatedDate);
                    break;                
                case "DatePub_desc":
                    notes = notes.OrderByDescending(s => s.PublishedDate);
                    break;
                case "DatePub":
                    notes = notes.OrderBy(s => s.PublishedDate);
                    break;
                default:
                    notes = notes.OrderByDescending(s => s.PublishedDate);
                    break;
            }

            List<detailmodel> dmodel = new List<detailmodel>();

            //calculating totaldownload and money
            foreach(var total in notes)
            {
                int totalDownloaded;
                decimal? totalMoney;

                if(total.Status == 9 || total.Status == 11)
                {
                    totalDownloaded = db.Downloads.Where(x => x.NoteID == total.ID && x.IsSellerHasAllowedDownload == true).Count();
                    var records = db.Downloads.Where(x => x.NoteID == total.ID && x.IsSellerHasAllowedDownload == true);                    
                    totalMoney = records.Sum(x => x.PurchasedPrice);
                    if (records.Count() == 0)
                    {
                        totalMoney = 0;
                    }
                }
                else
                {
                    totalDownloaded = 0;
                    totalMoney = 0;
                }

                //add data to modal
                detailmodel detailModel = new detailmodel()
                {
                    sellnote = total,
                    totalDownload = totalDownloaded,
                    totalEarning = totalMoney
                };

                dmodel.Add(detailModel);
            }

            //sorting for two column 
            if (SortOrder == "Downloaded_desc")
            {
                dmodel = dmodel.OrderByDescending(x => x.totalDownload).ToList();
            }
            if (SortOrder == "Downloaded")
            {
                dmodel = dmodel.OrderBy(x => x.totalDownload).ToList();
            }
            if (SortOrder == "Earning_desc")
            {
                dmodel = dmodel.OrderByDescending(x => x.totalEarning).ToList();
            }
            if (SortOrder == "Earning")
            {
                dmodel = dmodel.OrderBy(x => x.totalEarning).ToList();
            }

            Member.memDetails = dmodel;

            //pagination
            var pager = new Pager(notes.Count(), MemDetail_page, 10);
            ViewBag.currentPage = pager.CurrentPage;
            ViewBag.endPage = pager.EndPage;
            ViewBag.startpage = pager.StartPage;
            ViewBag.totalPage = notes.Count();

            Member.memDetails = dmodel.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);

            return View(Member);
        }
    }
}