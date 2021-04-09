using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using NotesMarketPlace.Models;

namespace NotesMarketPlace.Controllers
{
    [Authorize]
    public class SearchNoteController : Controller
    {
        readonly NotesMarketPlaceEntities db;     
        public SearchNoteController()
        {
            db = new NotesMarketPlaceEntities();
        }
        
        [HttpGet]
        [Route("SearchNotes")]
        [AllowAnonymous]
        [OutputCache(Duration = 0)]
        public ActionResult SearchNotes(string search, string type, string category, string university, string course, string country, string ratings, int page = 1)
        {
            ViewBag.navclass = "white-nav";
            ViewBag.SearchNotes = "active";

            ViewBag.Search = search;
            ViewBag.Category = category;
            ViewBag.Type = type;
            ViewBag.University = university;
            ViewBag.Course = course;
            ViewBag.Country = country;
            ViewBag.Rating = ratings;

            //Fetch the data 
            ViewBag.categoryList = db.NoteCategories.Where(x => x.IsActive == true).ToList();
            ViewBag.typeList = db.NoteTypes.Where(x => x.IsActive == true).ToList();
            ViewBag.universityList = db.SellerNotes.Where(x => x.IsActive == true && x.Status == 9 && x.UniversityName != null).Select(p => p.UniversityName).OrderBy(p => p).ToList().Distinct();
            ViewBag.courseList = db.SellerNotes.Where(x => x.IsActive == true && x.Status == 9 && x.Course != null).Select(p => p.Course).OrderBy(p => p).ToList().Distinct();
            ViewBag.countryList = db.Countries.Where(x => x.IsActive == true).ToList();

            ViewBag.RatingList = new List<SelectListItem> { new SelectListItem { Text = "1+", Value = "1" }, new SelectListItem { Text = "2+", Value = "2" }, new SelectListItem { Text = "3+", Value = "3" }, new SelectListItem { Text = "4+", Value = "4" }, new SelectListItem { Text = "5", Value = "5" } };

            var noteslist = db.SellerNotes.Where(x => x.Status == 9 && x.IsActive == true);    //only published note        


            //fetch data based on filters
            if (!String.IsNullOrEmpty(search))
            {
                noteslist = noteslist.Where(x => x.Title.ToLower().Contains(search.ToLower()) ||
                                                 x.NoteCategories.Name.ToLower().Contains(search.ToLower()));
            }

            if (!String.IsNullOrEmpty(type))
            {
                noteslist = noteslist.Where(x => x.NoteType.ToString().ToLower().Contains(type.ToLower()));
            }
            if (!String.IsNullOrEmpty(category))
            {
                noteslist = noteslist.Where(x => x.Category.ToString().ToLower().Contains(category.ToLower()));
            }
            if (!String.IsNullOrEmpty(university))
            {
                noteslist = noteslist.Where(x => x.UniversityName.ToLower().Contains(university.ToLower()));
            }
            if (!String.IsNullOrEmpty(course))
            {
                noteslist = noteslist.Where(x => x.Course.ToLower().Contains(course.ToLower()));
            }
            if (!String.IsNullOrEmpty(country))
            {
                noteslist = noteslist.Where(x => x.Country.ToString().ToLower().Contains(country.ToLower()));
            }

            List<SearchNoteViewModel> searchNotesList = new List<SearchNoteViewModel>();

            //count total and avg review
            if (String.IsNullOrEmpty(ratings))
            {
                foreach (var item in noteslist)
                {
                    var review = db.SellerNotesReviews.Where(x => x.NoteID == item.ID && x.IsActive == true).Select(x => x.Ratings);
                    var totalreview = review.Count();
                    var avgreview = totalreview > 0 ? Math.Ceiling(review.Average()) : 0;
                    var spamcount = db.SellerNotesReportedIssues.Where(x => x.NoteID == item.ID).Count();

                    SearchNoteViewModel note = new SearchNoteViewModel()
                    {
                        note = item,
                        averageRating = Convert.ToInt32(avgreview),
                        totalRating = totalreview,
                        totalSpam = spamcount                       
                    };
                    searchNotesList.Add(note);
                }
            }
            else
            {
                foreach (var item in noteslist)
                {
                    var review = db.SellerNotesReviews.Where(x => x.NoteID == item.ID && x.IsActive == true).Select(x => x.Ratings);
                    var totalreview = review.Count();
                    var avgreview = totalreview > 0 ? Math.Ceiling(review.Average()) : 0;
                    var spamcount = db.SellerNotesReportedIssues.Where(x => x.NoteID == item.ID).Count();                    

                    if (avgreview >= Convert.ToInt32(ratings))
                    {
                        SearchNoteViewModel note = new SearchNoteViewModel()
                        {
                            note = item,
                            averageRating = Convert.ToInt32(avgreview),
                            totalRating = totalreview,
                            totalSpam = spamcount                            
                        };
                        searchNotesList.Add(note);
                    }

                }
            }
            //pagination
            var pager = new Pager(noteslist.Count(), page, 9);
            ViewBag.currentPage = pager.CurrentPage;
            ViewBag.endPage = pager.EndPage;
            ViewBag.startpage = pager.StartPage;

            IEnumerable<SearchNoteViewModel> result = searchNotesList.AsEnumerable().Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);

            ViewBag.resultCount = searchNotesList.Count();

            return View(result);
        }


        // GET: SearchNote
        [HttpGet]
        [Route("NoteDetail/{id}")]
        [OutputCache(Duration = 0)]
        [AllowAnonymous]
        public ActionResult NoteDetails(int id)
        {
            ViewBag.navClass = "white-nav";
            ViewBag.SearchNotes = "active";

            ViewBag.SadminPhone = db.SystemConfigurations.FirstOrDefault(x => x.Key == "SupportContactNumber").Value;

            if(Session["ID"] != null)
            {
                var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

                //check user enter detail of profile or not
                if (user.RoleID == 3 || user.RoleID == 4)
                {
                    ViewBag.isAdmin = user.RoleID;
                }
            }                        

            var totalReportedIssue = db.SellerNotesReportedIssues.Where(x => x.NoteID == id).Count();            

            //total review
            var rating = db.SellerNotesReviews.Where(x => x.NoteID == id && x.IsActive == true).Average(x => (int?)x.Ratings) ?? 0;
            var totalreview = db.SellerNotesReviews.Where(x => x.NoteID == id && x.IsActive == true).Count();

            //fetch country name 
            var country = db.SellerNotes.FirstOrDefault(x => x.ID == id).Country;
            ViewBag.countryName = db.Countries.FirstOrDefault(x => x.ID == country && x.IsActive == true).Name;

            //Querystring
            IEnumerable<ReviewsModel> reviews = from review in db.SellerNotesReviews
                                                join users in db.Users on review.ReviewedByID equals users.ID
                                                join userprofile in db.UserProfile on review.ReviewedByID equals userprofile.UserID
                                                where review.NoteID == id && review.IsActive == true
                                                orderby review.Ratings descending, review.CreatedDate descending
                                                select new ReviewsModel { sellerNotesReviews = review, users = users, userProfiles = userprofile };

            NoteDetailViewModel detail = new NoteDetailViewModel();

            detail.sellnote = db.SellerNotes.FirstOrDefault(x => x.ID == id);
            detail.reportedIssue = totalReportedIssue;
            detail.TotalReview = totalreview;
            detail.AvgRating = (int)rating;
            detail.notesreview = reviews;

            //show popup box
            if (TempData["Requested"] != null)
            {
                ViewBag.Requested = "Requested";
            }

            return View(detail);            
        }       

        //download note for valid user
        [Authorize]
        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult Download(int noteId, int userId)
        {
            //Download the file                                                
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

            //check its member or admin
            if(user.RoleID == 3 || userId == user.ID || user.RoleID == 4)
            {
                return RedirectToAction("AdminDownload", "AdminDownloadNote", new { noteId = noteId, userId = userId});
            }

            if (Session["ID"] == null)
            {
                return RedirectToAction("Login","Account");
            }            

            //check user enter the profile details or not
            bool temp = db.UserProfile.Any(x => x.UserID == user.ID);
            if (!temp)
            {
                return RedirectToAction("MyProfile", "UserProfile");
            }

            //count notes for zip or simple note download
            var note = db.SellerNotes.Find(noteId);
            var count = db.SellerNotesAttachements.Where(x => x.NoteID == noteId).Count();
            string notesattachementpath = "~/Members/" + userId + "/" + noteId + "/Attachements/";
            if (count > 1)
            {
                var noteattachement = db.SellerNotesAttachements.Where(x => x.NoteID == note.ID).ToList();
            }
            
            //full path for download file
            string filename = db.SellerNotesAttachements.FirstOrDefault(x => x.NoteID == noteId).FileName;
            string attachmentspath = notesattachementpath + filename;
            string fullpath = System.IO.Path.Combine(notesattachementpath, filename);
            
            //when user first time download
            if (!db.Downloads.Any(x => x.NoteID == noteId && x.Downloader == user.ID))
            {                 
                //Save data in database 
                SellerNotes obj = new SellerNotes();
                NoteCategories cat = new NoteCategories();
                int category = db.SellerNotes.FirstOrDefault(x => x.ID == noteId).Category;
                string title = db.SellerNotes.FirstOrDefault(x => x.ID == noteId).Title;

                Downloads downloadnotedetail = new Downloads();

                downloadnotedetail.NoteID = noteId;
                downloadnotedetail.Seller = userId;
                downloadnotedetail.Downloader = user.ID;
                downloadnotedetail.AttachmentPath = notesattachementpath;
                downloadnotedetail.NoteTitle = title;
                downloadnotedetail.NoteCategory = db.NoteCategories.FirstOrDefault(x => x.ID == category).Name;
                downloadnotedetail.CreatedDate = DateTime.Now;
                downloadnotedetail.CreatedBy = user.ID;
                downloadnotedetail.AttachmentDownloadedDate = DateTime.Now;
                downloadnotedetail.IsActive = true;


                var notes = db.SellerNotes.FirstOrDefault(x => x.ID == noteId);
                if (notes.IsPaid)
                {
                    downloadnotedetail.IsSellerHasAllowedDownload = false;
                    downloadnotedetail.IsAttachmentDownloaded = false;
                    downloadnotedetail.IsPaid = true;
                    downloadnotedetail.PurchasedPrice = db.SellerNotes.FirstOrDefault(x => x.ID == noteId).SellingPrice;

                    db.Downloads.Add(downloadnotedetail);
                    db.SaveChanges();

                    return RedirectToAction("NoteDetails", new { id = noteId });
                }

                downloadnotedetail.IsSellerHasAllowedDownload = true;
                downloadnotedetail.IsAttachmentDownloaded = true;
                downloadnotedetail.PurchasedPrice = 0;
                downloadnotedetail.IsPaid = false;

                db.Downloads.Add(downloadnotedetail);
                db.SaveChanges();
            }
                        
            //check allow download
            var res = db.Downloads.FirstOrDefault(x => x.NoteID == noteId && x.Downloader == user.ID);
            if (res.IsSellerHasAllowedDownload == false && res.IsPaid == true)
            {
                int userOfNote = db.SellerNotes.FirstOrDefault(x => x.ID == noteId).SellerID;
                string name = db.Users.FirstOrDefault(x => x.ID == userOfNote).FirstName;
                string fullName = user.FirstName + " " + user.LastName;
                forallowdownload(name, fullName);

                //set temp data for popup box
                TempData["Requested"] = "Requested"; 

                return RedirectToAction("NoteDetails", new { id = noteId });
            }
                                              
            Downloads forModifydata = new Downloads();
            forModifydata.IsAttachmentDownloaded = true;
            forModifydata.ModifiedDate = DateTime.Now;
            forModifydata.ModifiedBy = user.ID;
            forModifydata.AttachmentDownloadedDate = DateTime.Now;
            db.SaveChanges();

            //for multiple file
            if (count > 1)
            {
                string path = Server.MapPath(notesattachementpath);

                DirectoryInfo dir = new DirectoryInfo(path);

                using (var memoryStream = new MemoryStream())
                {
                    using (var ziparchive = new ZipArchive(memoryStream, System.IO.Compression.ZipArchiveMode.Create, true))
                    {
                        foreach (var item in dir.GetFiles())
                        {
                            string filepath = path + item.ToString();
                            ziparchive.CreateEntryFromFile(filepath, item.ToString());
                        }
                    }
                    return File(memoryStream.ToArray(), "application/zip", note.Title + ".zip");
                }
            }        
            
            //for only one file
            return File(fullpath, "application/pdf", filename);       
        }

        //email template
        public void forallowdownload(string userName, string buyerName)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "RequestForAllowDownload" + ".cshtml");
            using (var context = new NotesMarketPlaceEntities())
            {
                body = body.Replace("@ViewBag.userName", userName);
                body = body.Replace("@ViewBag.buyerName", buyerName);
                body = body.ToString();

                BuildEmailVerifyTemplate(body, "rmpipaliya308@gmail.com", buyerName); //usr email address
            }
        }

        public static void BuildEmailVerifyTemplate(string bodyText, string sendTo, string fullBuyerName)
        {
            string from, to, bcc, cc, subject, body;

            from = "rutvikpipaliya33@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = fullBuyerName + "wants to purchase your notes";

            StringBuilder sb = new StringBuilder();
            sb.Append(bodyText);
            body = sb.ToString();

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));

            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendEmail(mail);
        }

        //Send mail function  
        [HandleError]
        public static void SendEmail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            string email = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"];
            string password = System.Configuration.ConfigurationManager.AppSettings["Password"];
            client.Credentials = new System.Net.NetworkCredential(email, password);
            try
            {
                client.Send(mail);                
            }
            catch (Exception e)
            {
                Debug.WriteLine("------------- " + e.ToString());
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("DeleteReview/{rid}")]
        public ActionResult DeleteReview(int rid)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            var review = db.SellerNotesReviews.Where(x => x.ID == rid && x.IsActive == true).FirstOrDefault();

            int noteID = review.NoteID;

            review.IsActive = false;
            review.ModifiedDate = DateTime.Now;
            review.ModifiedBy = user.ID;

            db.SaveChanges();

            return RedirectToAction("NoteDetails", new { id = noteID});
        }
    }
}