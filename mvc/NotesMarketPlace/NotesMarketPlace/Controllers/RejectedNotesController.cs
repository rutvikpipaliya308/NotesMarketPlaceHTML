using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Models;

namespace NotesMarketPlace.Controllers
{  
    public class RejectedNotesController : Controller
    {
        readonly NotesMarketPlaceEntities db;
        public RejectedNotesController()
        {
            db = new NotesMarketPlaceEntities();
        }

        [HttpGet]
        [Authorize]
        [Route("RejectedNotes")]
        public ActionResult RejectedNotes(String sortOrder, string RN_search, int RejecteNotes_page = 1)
        {
            ViewBag.navClass = "white-nav";

            ViewBag.TitleSortParm = sortOrder == "Title" ? "title_desc" : "Title";
            ViewBag.CategorySortParm = sortOrder == "Category" ? "category_desc" : "Category";            

            var user = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();

            //Query string
            var rejectednotes = from slr in db.SellerNotes 
                                join cat in db.NoteCategories on slr.Category equals cat.ID
                                where
                                slr.SellerID == user.ID &&
                                slr.Status == 10 &&
                                slr.IsActive == true
                                select new RejectedNotesViewModel { rejectednotestbl = slr , notecategorytbl = cat};

            //Searching
            if (RN_search != null)
            {
                rejectednotes = rejectednotes.Where(
                            x => x.rejectednotestbl.Title.Contains(RN_search) ||
                                 x.notecategorytbl.Name.Contains(RN_search));
            }

            //Sorting
            switch (sortOrder)
            {
                case "title_desc":
                    rejectednotes = rejectednotes.OrderByDescending(s => s.rejectednotestbl.Title);
                    break;
                case "Title":
                    rejectednotes = rejectednotes.OrderBy(s => s.rejectednotestbl.Title);
                    break;
                case "category_desc":
                    rejectednotes = rejectednotes.OrderByDescending(s => s.notecategorytbl.Name);
                    break;
                case "Category":
                    rejectednotes = rejectednotes.OrderBy(s => s.notecategorytbl.Name);
                    break;                    
                default:
                    rejectednotes = rejectednotes.OrderByDescending(s => s.rejectednotestbl.Title);
                    break;
            }

            //Pagination
            var pager = new Pager(rejectednotes.Count(), RejecteNotes_page, 10);
            ViewBag.currentPage = pager.CurrentPage;
            ViewBag.endPage = pager.EndPage;
            ViewBag.startpage = pager.StartPage;

            ViewBag.pageNumber = RejecteNotes_page;
            ViewBag.srno = RejecteNotes_page;
            ViewBag.TotalRejectedNotesPage = Math.Ceiling(rejectednotes.Count() / 10.0);
            rejectednotes = rejectednotes.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);

            return View(rejectednotes);          
            
        }

        [HttpGet]
        public ActionResult CloneNote(int noteid)
        {
            var user = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();

            var rejectednote = db.SellerNotes.Find(noteid);

            SellerNotes clonenote = new SellerNotes();

            clonenote.SellerID = rejectednote.SellerID;
            clonenote.Status = 6;
            clonenote.Title = rejectednote.Title;
            clonenote.Category = rejectednote.Category;
            clonenote.NoteType = rejectednote.NoteType;
            clonenote.NumberofPages = rejectednote.NumberofPages;
            clonenote.Description = rejectednote.Description;
            clonenote.UniversityName = rejectednote.UniversityName;
            clonenote.Country = rejectednote.Country;
            clonenote.Course = rejectednote.Course;
            clonenote.CourseCode = rejectednote.CourseCode;
            clonenote.Professor = rejectednote.Professor;
            clonenote.IsPaid = rejectednote.IsPaid;
            clonenote.SellingPrice = rejectednote.SellingPrice;
            clonenote.CreatedBy = user.ID;
            clonenote.CreatedDate = DateTime.Now;
            clonenote.IsActive = true;

            db.SellerNotes.Add(clonenote);
            db.SaveChanges();

            clonenote = db.SellerNotes.Find(clonenote.ID);

            //Note image
            if (rejectednote.DisplayPicture != null)
            {
                var rejectednotefilepath = Server.MapPath(rejectednote.DisplayPicture);
                var clonenotefilepath = "~/Members/" + user.ID + "/" + clonenote.ID + "/";

                var filepath = Path.Combine(Server.MapPath(clonenotefilepath));

                FileInfo file = new FileInfo(rejectednotefilepath);

                Directory.CreateDirectory(filepath);
                if (file.Exists)
                {
                    System.IO.File.Copy(rejectednotefilepath, Path.Combine(filepath, Path.GetFileName(rejectednotefilepath)));
                }

                clonenote.DisplayPicture = Path.Combine(clonenotefilepath, Path.GetFileName(rejectednotefilepath));
                db.SaveChanges();
            }

            //Note preview
            if (rejectednote.NotesPreview != null)
            {
                var rejectednotefilepath = Server.MapPath(rejectednote.NotesPreview);
                var clonenotefilepath = "~/Members/" + user.ID + "/" + clonenote.ID + "/";

                var filepath = Path.Combine(Server.MapPath(clonenotefilepath));

                FileInfo file = new FileInfo(rejectednotefilepath);

                Directory.CreateDirectory(filepath);

                if (file.Exists)
                {
                    System.IO.File.Copy(rejectednotefilepath, Path.Combine(filepath, Path.GetFileName(rejectednotefilepath)));
                }

                clonenote.NotesPreview = Path.Combine(clonenotefilepath, Path.GetFileName(rejectednotefilepath));
                db.SaveChanges();
            }

            //upload notes
            var rejectednoteattachement = Server.MapPath("~/Members/" + user.ID + "/" + rejectednote.ID + "/Attachements/");
            var clonenoteattachement = "~/Members/" + user.ID + "/" + clonenote.ID + "/Attachements/";

            var attachementfilepath = Path.Combine(Server.MapPath(clonenoteattachement));

            Directory.CreateDirectory(attachementfilepath);

            foreach (var files in Directory.GetFiles(rejectednoteattachement))
            {
                FileInfo file = new FileInfo(files);

                if (file.Exists)
                {
                    System.IO.File.Copy(file.ToString(), Path.Combine(attachementfilepath, Path.GetFileName(file.ToString())));
                }

                SellerNotesAttachements attachement = new SellerNotesAttachements();
                attachement.NoteID = clonenote.ID;
                attachement.FileName = Path.GetFileName(file.ToString());
                attachement.FilePath = clonenoteattachement;
                attachement.CreatedDate = DateTime.Now;
                attachement.CreatedBy = user.ID;
                attachement.IsActive = true;

                db.SellerNotesAttachements.Add(attachement);
                db.SaveChanges();
            }

            return RedirectToAction("Dashboard", "SellNotes");
        }

    }
}