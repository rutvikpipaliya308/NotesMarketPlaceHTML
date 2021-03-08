using System;
using System.Collections.Generic;
using NotesMarketPlace.Models;
using System.Linq;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace NotesMarketPlace.Controllers
{
    [Authorize]
    public class SellNotesController : Controller
    {

        readonly NotesMarketPlaceEntities db;

        public SellNotesController()
        {
            db = new NotesMarketPlaceEntities();
        }


        //SellNotes (Searching, Sorting, Paging)
        //Inprogress Notes & Published Notes
        //GET : SellNotes/Dashboard        
        [Authorize]
        [HttpGet]
        public ActionResult Dashboard(string Pro_search, string Pub_search, string sortOrderPro, string sortOrderPub, int Pro_page = 1, int Pub_page = 1)
        {

            ViewBag.navclass = "white-nav";
            ViewBag.SellNotes = "active";

            //Inprogress Notes
            ViewBag.TitleSortParm = sortOrderPro == "Title" ? "title_desc" : "Title";
            ViewBag.DateSortParm = sortOrderPro == "Date" ? "date_desc" : "Date";
            ViewBag.CategorySortParm = sortOrderPro == "Category" ? "category_desc" : "Category";
            ViewBag.StatusSortParm = sortOrderPro == "Status" ? "status_desc" : "Status";

            //Published notes
            ViewBag.TitleSortParm1 = sortOrderPub == "Title" ? "title_desc" : "Title";
            ViewBag.DateSortParm1 = sortOrderPub == "Date" ? "date_desc" : "Date";
            ViewBag.CategorySortParm1 = sortOrderPub == "Category" ? "category_desc" : "Category";
            ViewBag.STypeSortParm1 = sortOrderPub == "SellType" ? "sellType_desc" : "SellType";
            ViewBag.PriceSortParm1 = sortOrderPub == "Price" ? "price_desc" : "Price";

            ViewBag.PageNumber = Pro_page;
            ViewBag.PageNumberPublished = Pub_page;

            DashboardViewModel dashboardviewmodel = new DashboardViewModel();
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

            var Pro_Note = db.SellerNotes.Where(x => x.SellerID == user.ID && (x.Status == 6 || x.Status == 7 || x.Status == 8));
            var Pub_Note = db.SellerNotes.Where(x => x.SellerID == user.ID && x.Status == 9);

            if (Pro_search != null)
            {
                Pro_Note = Pro_Note.Where(x => x.Title.Contains(Pro_search) || x.NoteCategories.Name.Contains(Pro_search) || x.ReferenceData.Value.Contains(Pro_search));
            }
            if (Pub_search != null)
            {
                Pub_Note = Pub_Note.Where(x => x.Title.Contains(Pub_search) || x.NoteCategories.Name.Contains(Pub_search) || x.ReferenceData.Value.Contains(Pub_search));
            }
            switch (sortOrderPro)
            {
                case "title_desc":
                    dashboardviewmodel.InProgressNote = Pro_Note.OrderByDescending(s => s.Title);
                    break;
                case "Title":
                    dashboardviewmodel.InProgressNote = Pro_Note.OrderBy(s => s.Title);
                    break;
                case "category_desc":
                    dashboardviewmodel.InProgressNote = Pro_Note.OrderByDescending(s => s.NoteCategories.Name);
                    break;
                case "Category":
                    dashboardviewmodel.InProgressNote = Pro_Note.OrderBy(s => s.NoteCategories.Name);
                    break;
                case "status_desc":
                    dashboardviewmodel.InProgressNote = Pro_Note.OrderByDescending(s => s.Status);
                    break;
                case "Status":
                    dashboardviewmodel.InProgressNote = Pro_Note.OrderBy(s => s.Status);
                    break;
                case "date_desc":
                    dashboardviewmodel.InProgressNote = Pro_Note.OrderByDescending(s => s.ModifiedDate);
                    break;
                case "Date":
                    dashboardviewmodel.InProgressNote = Pro_Note.OrderBy(s => s.ModifiedDate);
                    break;
                default:
                    dashboardviewmodel.InProgressNote = Pro_Note.OrderByDescending(s => s.ModifiedDate);
                    break;
            }
            switch (sortOrderPub)
            {
                case "title_desc":
                    dashboardviewmodel.PublishedNote = Pub_Note.OrderByDescending(s => s.Title);
                    break;
                case "Title":
                    dashboardviewmodel.PublishedNote = Pub_Note.OrderBy(s => s.Title);
                    break;
                case "category_desc":
                    dashboardviewmodel.PublishedNote = Pub_Note.OrderByDescending(s => s.NoteCategories.Name);
                    break;
                case "Category":
                    dashboardviewmodel.PublishedNote = Pub_Note.OrderBy(s => s.NoteCategories.Name);
                    break;
                case "sellType_desc":
                    dashboardviewmodel.PublishedNote = Pub_Note.OrderByDescending(s => s.IsPaid);
                    break;
                case "SellType":
                    dashboardviewmodel.PublishedNote = Pub_Note.OrderBy(s => s.IsPaid);
                    break;
                case "date_desc":
                    dashboardviewmodel.PublishedNote = Pub_Note.OrderByDescending(s => s.ModifiedDate);
                    break;
                case "Date":
                    dashboardviewmodel.PublishedNote = Pub_Note.OrderBy(s => s.ModifiedDate);
                    break;
                case "price_desc":
                    dashboardviewmodel.PublishedNote = Pub_Note.OrderByDescending(s => s.SellingPrice);
                    break;
                case "Price":
                    dashboardviewmodel.PublishedNote = Pub_Note.OrderBy(s => s.SellingPrice);
                    break;
                default:
                    dashboardviewmodel.PublishedNote = Pub_Note.OrderByDescending(s => s.ModifiedDate);
                    break;
            }

            ViewBag.TotalPagesInProgress = Math.Ceiling(dashboardviewmodel.InProgressNote.Count() / 5.0);
            ViewBag.TotalPagesInPublished = Math.Ceiling(dashboardviewmodel.PublishedNote.Count() / 5.0);
            dashboardviewmodel.InProgressNote = dashboardviewmodel.InProgressNote.Skip((Pro_page - 1) * 5).Take(5);
            dashboardviewmodel.PublishedNote = dashboardviewmodel.PublishedNote.Skip((Pub_page - 1) * 5).Take(5);

            return View(dashboardviewmodel);
        }



        //Delete Seller Note
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var note = db.SellerNotes.Where(x => x.ID == id).FirstOrDefault();

            if (note == null)
            {
                return Content("This Note is not Available....");
            }

            var noteFile = db.SellerNotesAttachements.Where(x => x.NoteID == note.ID).FirstOrDefault();
            db.SellerNotesAttachements.Remove(noteFile);
            db.SaveChanges();

            db.SellerNotes.Remove(note);
            db.SaveChanges();

            return RedirectToAction("Index");
        }




        // GET: SellNotes/AddNotes
        [Authorize]
        [HttpGet]
        public ActionResult AddNotes()
        {
            ViewBag.navclass = "white-nav";

            //Fetch data from database
            var category = db.NoteCategories.Where(x => x.IsActive == true).ToList();
            var type = db.NoteTypes.Where(x => x.IsActive == true).ToList();
            var country = db.Countries.Where(x => x.IsActive == true).ToList();

            AddNotesViewModel viewModel = new AddNotesViewModel
            {
                NoteCategoryList = category,
                NoteTypeList = type,
                CountryList = country
            };

            return View(viewModel);
        }

        [HttpPost]
        [Route("SellNote/AddNotes")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddNotes(AddNotesViewModel note, string Command)
        {
            
            Users user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            if (user != null && ModelState.IsValid)
            {
                SellerNotes sellernotes = new SellerNotes();

                sellernotes.SellerID = user.ID;
                sellernotes.ActionedBy = user.ID;
                sellernotes.Title = note.Title;
                sellernotes.Status = 6;
                sellernotes.Category = note.Category;
                sellernotes.NoteType = note.NoteType;
                sellernotes.NumberofPages = note.NumberofPages;
                sellernotes.Description = note.Description;
                sellernotes.UniversityName = note.UniversityName;
                sellernotes.Country = note.Country;
                sellernotes.Course = note.Course;
                sellernotes.CourseCode = note.CourseCode;
                sellernotes.Professor = note.Professor;
                sellernotes.IsPaid = note.IsPaid;
                sellernotes.SellingPrice = sellernotes.IsPaid == false ? 0 : note.SellingPrice;
                sellernotes.CreatedDate = DateTime.Now;
                sellernotes.CreatedBy = user.ID;
                sellernotes.IsActive = true;

                if (sellernotes.IsPaid)
                {
                    if (sellernotes.SellingPrice == null || sellernotes.SellingPrice < 1)
                    {
                        ModelState.AddModelError("SellingPrice", "Enter valid Selling price");

                        AddNotesViewModel viewModel = GetData();

                        return View(viewModel);
                    }
                }

                db.SellerNotes.Add(sellernotes);
                db.SaveChanges();

                Debug.WriteLine(sellernotes.ID);

                sellernotes = db.SellerNotes.Find(sellernotes.ID);

                if (note.DisplayPicture != null)
                {
                    string displaypicturefilename = System.IO.Path.GetFileName(note.DisplayPicture.FileName);
                    string displaypicturepath = "~/Members/" + user.ID + "/" + sellernotes.ID + "/";
                    CreateDirectoryIfMissing(displaypicturepath);
                    string displaypicturefilepath = Path.Combine(Server.MapPath(displaypicturepath), displaypicturefilename);
                    sellernotes.DisplayPicture = displaypicturepath + displaypicturefilename;
                    note.DisplayPicture.SaveAs(displaypicturefilepath);
                }

                if (note.NotesPreview != null)
                {
                    string notespreviewfilename = System.IO.Path.GetFileName(note.NotesPreview.FileName);
                    string notespreviewpath = "~/Members/" + user.ID + "/" + sellernotes.ID + "/";
                    CreateDirectoryIfMissing(notespreviewpath);
                    string notespreviewfilepath = Path.Combine(Server.MapPath(notespreviewpath), notespreviewfilename);
                    sellernotes.NotesPreview = notespreviewpath + notespreviewfilename;
                    note.NotesPreview.SaveAs(notespreviewfilepath);
                }

                db.SellerNotes.Attach(sellernotes);
                db.Entry(sellernotes).Property(x => x.DisplayPicture).IsModified = true;
                db.Entry(sellernotes).Property(x => x.NotesPreview).IsModified = true;
                db.SaveChanges();

                string notesattachementfilename = System.IO.Path.GetFileName(note.UploadNotes.FileName);
                string notesattachementpath = "~/Members/" + user.ID + "/" + sellernotes.ID + "/Attachements/";
                CreateDirectoryIfMissing(notesattachementpath);
                string notesattachementfilepath = Path.Combine(Server.MapPath(notesattachementpath), notesattachementfilename);

                note.NotesPreview.SaveAs(notesattachementfilepath);

                SellerNotesAttachements notesattachements = new SellerNotesAttachements
                {
                    NoteID = sellernotes.ID,
                    FileName = notesattachementfilename,
                    FilePath = notesattachementpath + notesattachementfilename,
                    CreatedDate = DateTime.Now,
                    CreatedBy = user.ID,
                    IsActive = true
                };

                db.SellerNotesAttachements.Add(notesattachements);

                db.SaveChanges();
                db.Dispose();

                return RedirectToAction("AddNotes", "SellNotes");

            }
            else
            {
                AddNotesViewModel viewModel = GetData();
                return View(viewModel);
            }
        }


        public AddNotesViewModel GetData()
        {
            var category = db.NoteCategories.Where(x => x.IsActive == true).ToList();
            var type = db.NoteTypes.Where(x => x.IsActive == true).ToList();
            var country = db.Countries.Where(x => x.IsActive == true).ToList();

            AddNotesViewModel viewModel = new AddNotesViewModel
            {
                NoteCategoryList = category,
                NoteTypeList = type,
                CountryList = country
            };

            return viewModel;
        }

        private void CreateDirectoryIfMissing(string path)
        {
            bool folderExists = Directory.Exists(Server.MapPath(path));
            if (!folderExists)
            {
                Directory.CreateDirectory(Server.MapPath(path));
            }

        }


        //Edit note
        [HttpGet]
        [Authorize]
        public ActionResult EditNote(int id)
        {
            ViewBag.Class = "white-nav";

            var category = db.NoteCategories.Where(x => x.IsActive == true).ToList();
            var type = db.NoteTypes.Where(x => x.IsActive == true).ToList();
            var country = db.Countries.Where(x => x.IsActive == true).ToList();


            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

            var note = db.SellerNotes.Where(x => x.ID == id && x.SellerID == user.ID && x.Status == 6).FirstOrDefault();

            var FileNote = db.SellerNotesAttachements.Where(x => x.NoteID == note.ID).FirstOrDefault();

            if (note == null)
            {
                return Content("You can't have access to this file");
            }

            EditNoteViewModel editnote = new EditNoteViewModel();


            editnote.NoteCategoryList = category;
            editnote.NoteTypeList = type;
            editnote.CountryList = country;

            editnote.Title = note.Title;
            editnote.Category = note.Category;
            editnote.NoteType = note.NoteType;
            editnote.NumberofPages = note.NumberofPages;
            editnote.Description = note.Description;
            editnote.Country = note.Country;
            editnote.UniversityName = note.UniversityName;
            editnote.Course = note.Course;
            editnote.CourseCode = note.CourseCode;
            editnote.Professor = note.Professor;
            editnote.IsPaid = note.IsPaid;
            editnote.SellingPrice = note.SellingPrice;

            editnote.DisplayPicturePathName = note.DisplayPicture;
            editnote.NotePreviewPathName = note.NotesPreview;
            editnote.NotePathName = FileNote.FilePath;

            ViewBag.ImageName = Path.GetFileName(note.DisplayPicture);
            ViewBag.PreviewName = Path.GetFileName(note.NotesPreview);
            ViewBag.FileName = Path.GetFileName(FileNote.FilePath);

            return View(editnote);
        }




        [HttpPost]
        [Route("SellNote/EditNote/{id}")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult EditNote(EditNoteViewModel editnote, string Command)
        {
            ViewBag.Class = "white-nav";
            Users user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

            var note = db.SellerNotes.Where(x => x.ID == editnote.ID && x.Status == 6 && x.SellerID == user.ID).FirstOrDefault();

            var AttachFile = db.SellerNotesAttachements.Where(x => x.NoteID == note.ID).FirstOrDefault();

            if (user != null && ModelState.IsValid)
            {
                note.Title = editnote.Title;
                note.Status = Command == "Save" ? 6 : 7 ;
                note.Category = editnote.Category;
                note.NoteType = editnote.NoteType;
                note.NumberofPages = editnote.NumberofPages;
                note.Description = editnote.Description;
                note.UniversityName = editnote.UniversityName;
                note.Country = editnote.Country;
                note.Course = editnote.Course;
                note.CourseCode = editnote.CourseCode;
                note.Professor = editnote.Professor;
                note.IsPaid = editnote.IsPaid;
                note.SellingPrice = editnote.IsPaid == false ? 0 : note.SellingPrice;
                note.ModifiedDate = DateTime.Now;

                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();


                if (editnote.DisplayPicture != null)
                {
                    string FileNameDelete = System.IO.Path.GetFileName(note.DisplayPicture);
                    string PathImage = Request.MapPath("~/Members/" + note.SellerID + "/" + note.ID + "/" + FileNameDelete);
                    FileInfo file = new FileInfo(PathImage);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    string displaypicturefilename = Path.GetFileName(editnote.DisplayPicture.FileName);
                    string displaypicturepath = "~/Members/" + note.SellerID + "/" + note.ID + "/";
                    string displaypicturefilepath = Path.Combine(Server.MapPath(displaypicturepath), displaypicturefilename);
                    note.DisplayPicture = displaypicturepath + displaypicturefilename;
                    editnote.DisplayPicture.SaveAs(displaypicturefilepath);

                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                }

                if (editnote.NotesPreview != null)
                {
                    string FileNameDelete = System.IO.Path.GetFileName(note.NotesPreview);
                    string PathPreview = Request.MapPath("~/Members/" + note.SellerID + "/" + note.ID + "/" + FileNameDelete);
                    FileInfo file = new FileInfo(PathPreview);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    string notespreviewfilename = Path.GetFileName(editnote.NotesPreview.FileName);
                    string notespreviewpath = "~/Members/" + note.SellerID + "/" + note.ID + "/";
                    string notespreviewfilepath = Path.Combine(Server.MapPath(notespreviewpath), notespreviewfilename);
                    note.NotesPreview = notespreviewpath + notespreviewfilename;
                    editnote.NotesPreview.SaveAs(notespreviewfilepath);

                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                }
                if (editnote.UploadNotes != null)
                {
                    string FileNameDelete = System.IO.Path.GetFileName(note.NotesPreview);
                    string PathPreview = Request.MapPath("~/Members/" + note.SellerID + "/" + note.ID + "/Attachements" + FileNameDelete);
                    FileInfo file = new FileInfo(PathPreview);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    string notesattachementfilename = System.IO.Path.GetFileName(editnote.UploadNotes.FileName);
                    string notesattachementpath = "~/Members/" + note.SellerID + "/" + note.ID + "/Attachements/";
                    string notesattachementfilepath = Path.Combine(Server.MapPath(notesattachementpath), notesattachementfilename);

                    editnote.UploadNotes.SaveAs(notesattachementfilepath);

                    AttachFile.FileName = notesattachementfilename;
                    AttachFile.FilePath = notesattachementpath + notesattachementfilename;
                    AttachFile.ModifiedDate = DateTime.Now;
                    AttachFile.ModifiedBy = user.ID;

                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                }
                return RedirectToAction("Dashboard", "SellNotes");

            }
            return View();
        }




    }
}