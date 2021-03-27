using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using NotesMarketPlace.Models;

namespace NotesMarketPlace.Controllers
{
    public class BuyerRequestController : Controller
    {
        readonly NotesMarketPlaceEntities db;
        public BuyerRequestController(){ db = new NotesMarketPlaceEntities(); }

        [HttpGet]
        [Authorize]
        [Route("BuyerRequest")]
        // GET: BuyerRequest
        public ActionResult BuyerRequest(string BR_search, string sortOrder, int BuyerRequest_page = 1, int noteid = 0, string buyer_email = null)
        {           
            ViewBag.navClass = "white-nav";
            ViewBag.BuyerRequest = "active";

            ViewBag.TitleSortParm = sortOrder == "Title" ? "title_desc" : "Title";
            ViewBag.CategorySortParm = sortOrder == "Category" ? "category_desc" : "Category";
            ViewBag.BuyerNameSortParm = sortOrder == "BuyerName" ? "BuyerName_desc" : "BuyerName";
            ViewBag.PhoneSortParm = sortOrder == "Phone" ? "Phone_desc" : "Phone";
            ViewBag.SellTypeSortParm = sortOrder == "SellType" ? "SellType_desc" : "SellType";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";            

            var user = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();

            //Query string
            var buyerrequest = from dwl in db.Downloads
                                join up in db.UserProfile on dwl.Downloader equals up.UserID
                                join ur in db.Users on dwl.Downloader equals ur.ID
                                where dwl.Seller == user.ID && dwl.IsSellerHasAllowedDownload == false && dwl.IsPaid == true
                                select new BuyerRequestViewModel { Downloadstbl = dwl, UserProfiletbl = up, Userstbl = ur };

            //Search
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

            //Sorting
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

            //pagination
            var pager = new Pager(buyerrequest.Count(), BuyerRequest_page, 5);
            ViewBag.currentPage = pager.CurrentPage;
            ViewBag.endPage = pager.EndPage;
            ViewBag.startpage = pager.StartPage;

            ViewBag.pageNumber = BuyerRequest_page;
            ViewBag.srno = BuyerRequest_page;

            ViewBag.TotalBuyerRequestPage = Math.Ceiling(buyerrequest.Count() / 5.0);
            buyerrequest = buyerrequest.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);

            //seller allow download mail
            if (buyer_email != null && noteid != 0)
            {
                BuildEmailVerifyTemplate(buyer_email, noteid);
            }

            return View(buyerrequest);
        }


        public void BuildEmailVerifyTemplate(string buyeremail, int buyernoteid)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "AllowDownload" + ".cshtml");

            var user = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            var buyer = db.Users.FirstOrDefault(x => x.Email == buyeremail);

            var noterow = db.Downloads.FirstOrDefault(x => x.NoteID == buyernoteid && x.Downloader == buyer.ID);
            noterow.IsSellerHasAllowedDownload = true;
            noterow.ModifiedDate = DateTime.Now;
            db.SaveChanges();

            body = body.Replace("@ViewBag.FirstName", buyer.FirstName);
            body = body.Replace("@ViewBag.sellerName", user.FirstName);
            body = body.ToString();
            BuildEmailVerifyTemplate(body, "rmpipaliya308@gmail.com", buyer.FirstName);
        }

        public static void BuildEmailVerifyTemplate(string bodyText, string sendTo, String subjectname)
        {
            string from, to, bcc, cc, subject, body;

            from = "rutvikpipaliya33@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subjectname + " Allows you to download a note";

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
            client.Credentials = new System.Net.NetworkCredential("rutvikpipaliya33@gmail.com", "3oo82ooo");
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