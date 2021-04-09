using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using NotesMarketPlace.Models;

namespace NotesMarketPlace.Controllers
{
    [RoutePrefix("Admin")]
    public class AdminDashboardController : Controller
    {
        readonly NotesMarketPlaceEntities db;
        public AdminDashboardController()
        {
            db = new NotesMarketPlaceEntities();
        }
                     
        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("Dashboard")]
        public ActionResult AdminDashboard(string Apub_search, String SortOrder, string month, int Apun_page = 1)
        {
            ViewBag.Dashboard = "active";

            //Stats section

            //total inreview note
            ViewBag.inreviewNote = db.SellerNotes.Where(x => x.Status == 8 && x.IsActive == true).Count();

            //total downloaded note (last 7 days)
            DateTime startdate = DateTime.Now.AddDays(-7);
            ViewBag.totalDownload = db.Downloads.Where(x => x.IsAttachmentDownloaded == true &&
                                                            (x.AttachmentDownloadedDate >= startdate && x.AttachmentDownloadedDate <= DateTime.Now) &&                                                            
                                                            x.IsSellerHasAllowedDownload == true).Count();

            //total registerd user (last 7 days)
            ViewBag.TotalRegistered = db.Users.Where(x => x.CreatedDate >= startdate && x.CreatedDate <= DateTime.Now && x.RoleID == 2 && x.IsEmailVerified == true).Count();

            //sortorder
            ViewBag.TitleSortParm = SortOrder == "Title" ? "title_desc" : "Title";
            ViewBag.CategorySortParm = SortOrder == "Category" ? "category_desc" : "Category";
            ViewBag.SellTypeSortParm = SortOrder == "SellType" ? "sellType_desc" : "Selltype";
            ViewBag.PriceSortParm = SortOrder == "Price" ? "Price_desc" : "Price";
            ViewBag.NameSortParm = SortOrder == "Name" ? "Name_desc" : "Name";
            ViewBag.DateSortParm = SortOrder == "Date" ? "Date_desc" : "Date";
            ViewBag.TotalDownloadSortParm = SortOrder == "Total" ? "Total_desc" : "Total";
            ViewBag.SizeSortParm = SortOrder == "Size" ? "Size_desc" : "Size";

            DateTime prevDate = DateTime.Now.AddMonths(-6);

            //Query string
            var note = from sellnote in db.SellerNotes
                        join usr in db.Users on sellnote.SellerID equals usr.ID
                        where sellnote.Status == 9 &&
                        sellnote.IsActive == true &&
                        usr.IsActive == true && 
                        (sellnote.PublishedDate >= prevDate && sellnote.PublishedDate <= DateTime.Now)
                        orderby sellnote.PublishedDate descending
                        select new publishednotes { notes = sellnote, users = usr };

            //fetch last 5 month data for dropdown            
            ViewBag.lastSixMonths = Enumerable.Range(0, 6)
                              .Select(i => DateTime.Now.AddMonths(-i))
                              .Select(date => date.ToString("MMMM"));


            //filter based on month
            if (month != null)
            {
                //get month number from month name
                int monthNo = Convert.ToDateTime("01-" + month + "-2011").Month; 
                note = note.Where(x => x.notes.PublishedDate.Value.Month == monthNo).OrderByDescending(s => s.notes.PublishedDate);                
            }
            else
            {
                note = note.Where(x => x.notes.PublishedDate.Value.Month == DateTime.Now.Month).OrderByDescending(s => s.notes.PublishedDate);
            }            

            //searching
            if(Apub_search != null)
            {
                note = note.Where(x => x.notes.Title.Contains(Apub_search) ||
                                    x.notes.NoteCategories.Name.Contains(Apub_search) ||
                                    x.notes.ReferenceData.Value.Contains(Apub_search) ||
                                    x.users.FirstName.Contains(Apub_search) ||
                                    x.notes.PublishedDate.ToString().Contains(Apub_search) ||
                                    x.notes.SellingPrice.ToString().Contains(Apub_search));
            }            

            //sorting
            switch (SortOrder)
            {
                case "title_desc":
                    note = note.OrderByDescending(s => s.notes.Title);
                    break;
                case "Title":
                    note = note.OrderBy(s => s.notes.Title);
                    break;
                case "category_desc":
                    note =note.OrderByDescending(s => s.notes.NoteCategories.Name);
                    break;
                case "Category":
                    note = note.OrderBy(s => s.notes.NoteCategories.Name);
                    break;
                case "sellType_desc":
                    note = note.OrderByDescending(s => s.notes.ReferenceData.Value);
                    break;
                case "Selltype":
                    note = note.OrderBy(s => s.notes.ReferenceData.Value);
                    break;
                case "Price_desc":
                    note = note.OrderByDescending(s => s.notes.SellingPrice);
                    break;
                case "Price":
                    note = note.OrderBy(s => s.notes.SellingPrice);
                    break;
                case "Name_desc":
                    note = note.OrderByDescending(s => s.users.FirstName);
                    break;
                case "Name":
                    note = note.OrderBy(s => s.users.FirstName);
                    break;
                case "Date_desc":
                    note = note.OrderByDescending(s => s.notes.PublishedDate);
                    break;
                case "Date":
                    note = note.OrderBy(s => s.notes.PublishedDate);
                    break;                
                default:
                    note = note.OrderByDescending(s => s.notes.PublishedDate);
                    break;
            }

            List<AdminDashboardViewModel> noteList = new List<AdminDashboardViewModel>();

            //calculating total downloaded and total size
            foreach (var total in note)
            {
                int totalcount = db.Downloads.Where(x => x.NoteID == total.notes.ID && x.IsSellerHasAllowedDownload == true).Count();

                string notesattachementpath = "~/Members/" + total.notes.SellerID + "/" + total.notes.ID + "/Attachements/";
                string path = Server.MapPath(notesattachementpath);
                long size = GetDirectorySize(path);
                if (size > 1048576)
                {
                    size = size / 1048576;
                }
                else
                {
                    //byte to KB
                    if (size < 1024 && size > 0)
                    {
                        size = 1;
                    }
                    else
                    {
                        //MB
                        size = size / 1024;
                    }
                }

                //add data in model
                AdminDashboardViewModel dash = new AdminDashboardViewModel()
                {
                    Notes = total,
                    totalDownload = totalcount,
                    totalSize = size
                };

                noteList.Add(dash);
            }

            //sorting for 
            if(SortOrder == "Total_desc")
            {
                noteList = noteList.OrderByDescending(x => x.totalDownload).ToList();
            }
            if(SortOrder == "Total")
            {
                noteList = noteList.OrderBy(x => x.totalDownload).ToList();
            }
            if(SortOrder == "Size_desc")
            {
                noteList = noteList.OrderByDescending(x => x.totalSize).ToList();
            }
            if(SortOrder == "Size")
            {
                noteList = noteList.OrderBy(x => x.totalSize).ToList();
            }

            //pagination
            var pager = new Pager(note.Count(), Apun_page, 10);
            ViewBag.currentPage = pager.CurrentPage;
            ViewBag.endPage = pager.EndPage;
            ViewBag.startpage = pager.StartPage;
            ViewBag.totalPage = note.Count();
            
            IEnumerable<AdminDashboardViewModel> result = noteList.AsEnumerable().Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);

            return View(result);
        }

        //get folder size & calculate the size
        public long GetDirectorySize(string p)
        {
            string[] a = Directory.GetFiles(p, "*.*");

            long b = 0;
            foreach (string name in a)
            {
                FileInfo info = new FileInfo(name);
                b = b + info.Length;
            }
            
            return b;
        }

        [HttpPost]
        [HandleError]
        [Authorize(Roles = "Admin, SuperAdmin")]        
        public ActionResult UnpublishedNote(FormCollection form)
        {
            var user = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            int noteid = Convert.ToInt32(form["noteid"]);

            var note = db.SellerNotes.Where(x => x.ID == noteid && x.IsActive == true).FirstOrDefault();
            var attachment = db.SellerNotesAttachements.Where(x => x.NoteID == noteid && x.IsActive == true);

            note.Status = 11;
            note.IsActive = false;
            note.ActionedBy = user.ID;
            note.AdminRemarks = form["remarkUnPublished"];
            note.ModifiedDate = DateTime.Now;
            note.ModifiedBy = user.ID;

            //deactive all notes
            foreach(var item in attachment)
            {
                item.IsActive = false;
                item.ModifiedDate = DateTime.Now;
                item.ModifiedBy = user.ID;
            }
            db.SaveChanges();

            //string for email
            string remark = form["remarkUnPublished"];
            string title = note.Title;
            string firstname = user.FirstName;

            //sending mail function
            BuildUnPublishedNoteMail(firstname,title,remark);

            db.SaveChanges();
            return RedirectToAction("AdminDashboard");
        }

        public void BuildUnPublishedNoteMail(String firstname, String title, string remark)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "UnPublishedNote" + ".cshtml");

            body = body.Replace("@ViewBag.FirstName", firstname);
            body = body.Replace("@ViewBag.noteTitle", title);
            body = body.Replace("@ViewBag.adminRemark", remark);
            body = body.ToString();
            UnPublishedEmail(body, "rmpipaliya308@gmail.com");
        }

        public static void UnPublishedEmail(string bodyText, string sendTo)
        {
            string from, to, bcc, cc, subject, body;
            from = "rutvikpipaliya33@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = "Sorry! We need to remove your notes from our portal";
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
    }
}