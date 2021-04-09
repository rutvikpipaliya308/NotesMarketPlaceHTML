using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Models;

namespace NotesMarketPlace.Controllers
{
    [Route("Admin")]
    public class AdminSpamReportsController : Controller
    {
        readonly NotesMarketPlaceEntities db;
        public AdminSpamReportsController()
        {
            db = new NotesMarketPlaceEntities();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("NotesSpamReport")]
        [OutputCache(Duration = 0)]
        public ActionResult NotesSpamReport(string SortOrder, string Spam_search, int Spam_page = 1)
        {
            ViewBag.Reports = "active";

            ViewBag.ReportedBySortParm = SortOrder == "ReportedBy" ? "ReportedBy_desc" : "ReportedBy";
            ViewBag.TitleSortParm = SortOrder == "Title" ? "Title_desc" : "Title";
            ViewBag.CategorySortParm = SortOrder == "Category" ? "Category_desc" : "Category";
            ViewBag.DateSortParm = SortOrder == "Date" ? "Date_desc" : "Date";

            AdminSpamNotesIssuesViewModel reports = new AdminSpamNotesIssuesViewModel();

            var report = db.SellerNotesReportedIssues.Select(x => x);

            if(Spam_search != null)
            {
                report = report.Where(x => x.Users.FirstName.Contains(Spam_search) ||
                                            x.SellerNotes.Title.Contains(Spam_search) ||
                                            x.SellerNotes.NoteCategories.Name.Contains(Spam_search) ||
                                            x.Remarks.Contains(Spam_search));
            }

            switch (SortOrder)
            {
                case "ReportedBy_desc":
                    report = report.OrderByDescending(s => s.Users.FirstName);
                    break;
                case "ReportedBy":
                    report = report.OrderBy(s => s.Users.FirstName);
                    break;
                case "Title_desc":
                    report = report.OrderByDescending(s => s.SellerNotes.Title);
                    break;
                case "Title":
                    report = report.OrderBy(s => s.SellerNotes.Title);
                    break;
                case "Category_desc":
                    report = report.OrderByDescending(s => s.SellerNotes.NoteCategories.Name);
                    break;
                case "Category":
                    report = report.OrderBy(s => s.SellerNotes.NoteCategories.Name);
                    break;
                case "Date_desc":
                    report = report.OrderByDescending(s => s.CreatedDate);
                    break;
                case "Date":
                    report = report.OrderBy(s => s.CreatedDate);
                    break;                
                default:
                    report = report.OrderByDescending(s => s.CreatedDate);
                    break;
            }

            reports.issue = report;

            //pagination
            var pager = new Pager(report.Count(), Spam_page, 10);
            ViewBag.currentPage = pager.CurrentPage;
            ViewBag.endPage = pager.EndPage;
            ViewBag.startpage = pager.StartPage;
            ViewBag.totalPage = report.Count();

            reports.issue = report.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);

            return View(reports);
        }

        [HttpGet]
        [Route("DeleteSpamReport/{issueId}")]
        [OutputCache(Duration = 0)]
        public ActionResult DeleteSpamReport(int issueId)
        {
            var issue = db.SellerNotesReportedIssues.FirstOrDefault(x => x.ID == issueId);
            db.SellerNotesReportedIssues.Remove(issue);
            db.SaveChanges();

            return RedirectToAction("NotesSpamReport");
        }

    }
}