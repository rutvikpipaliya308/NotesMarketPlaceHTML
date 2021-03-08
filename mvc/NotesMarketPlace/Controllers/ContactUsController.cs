using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Models;
using System.Web.Hosting;
using System.Diagnostics;
using System.Text;
using System.Net.Mail;

namespace NotesMarketPlace.Controllers
{
    public class ContactUsController : Controller
    {
        //GET : Contactus/Contactus
        [HttpGet]
        public ActionResult ContactUs()
        {
            ViewBag.navclass = "white-nav";
            return View();
        }

        // POST: ContactUs/ContactUs
        [HttpPost]
        public ActionResult ContactUs(ContactUsViewModel commentdetails)
        {
            BuildContactUsMail(commentdetails.FirstName, commentdetails.Comments);

            return View();
        }

        public void BuildContactUsMail(String firstname, String comment)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "UserComments" + ".cshtml");

            body = body.Replace("@ViewBag.FirstName", firstname);
            body = body.Replace("@ViewBag.Comment", comment);
            body = body.ToString();
            ContactUsEmail(body, "rutvikpipaliya33@gmail.com", firstname);
        }

        public static void ContactUsEmail(string bodyText, string sendTo, string username)
        {
            string from, to, bcc, cc, subject, body;
            from = "rutvikpipaliya33@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = username +"- Query";
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
            client.Credentials = new System.Net.NetworkCredential("email", "password");
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