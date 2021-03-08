using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotesMarketPlace.Models;
using System.Web.Security;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Web.Hosting;
using System.Diagnostics;

namespace NotesMarketPlace.Controllers
{
    public class AccountController : Controller
    {
        //Login
        // GET: Account/Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        //POST : Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel userlogin)
        {
            if(ModelState.IsValid)
            { 
                using (var db = new NotesMarketPlaceEntities())
                {
                    //Email not registered
                    if(!db.Users.Any(x => x.Email == userlogin.Email))
                    {
                        ModelState.AddModelError("Email", "Email is not registered");
                        return View("Login");
                    }

                    
                    var isUser = db.Users.Where(x => x.Email.Equals(userlogin.Email) && x.Password.Equals(userlogin.Password)).FirstOrDefault();

                    //Wrong Password
                    if (isUser == null)
                    {
                        ModelState.AddModelError("Password", "Wrong Password");
                        return View("Login");
                    }

                    //Account is Not Active
                    if (!isUser.IsActive)
                    {
                        return RedirectToAction("Index");
                    }

                    //Email not Varified
                    if (!isUser.IsEmailVerified)
                    {
                        return RedirectToAction("VerifyEmail", "Account", new { regID = isUser.ID });
                    }

                    if (isUser != null)
                    {
                        //Remember me check box
                        FormsAuthentication.SetAuthCookie(userlogin.Email, true);

                        Session["ID"] = isUser.ID;
                        Session["Email"] = isUser.Email;


                        //For Member
                        if (isUser.RoleID == 1)
                        {
                            return RedirectToAction("Login", "Account");
                        }
                        //For Admin and SuperAdmin
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }

            return RedirectToAction("Login", "Account");
        }


        //Logout
        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            Session["ID"] = null;
            Session["Email"] = null;
            Session.RemoveAll();
            return RedirectToAction("Login", "Account");
        }


        //SignUp
        // GET: Account/SignUp
        [HttpGet]
        public ActionResult SignUp()
        {
            ViewBag.status = 0;
            return View();
        }

        //POST : Account/SignUp
        [HttpPost]
        public ActionResult SignUp([Bind(Include = "FirstName,LastName,Email,Password,ConfirmPassword")] UsersViewModel signupviewmodel)
        {
            if (ModelState.IsValid)
            {
                using (var context = new NotesMarketPlaceEntities())
                {
                    bool emailalreadyexists = context.Users.Any(x => x.Email == signupviewmodel.Email);

                    if (emailalreadyexists)
                    {
                        ModelState.AddModelError("Email", "Email already registered");
                        return View();
                    }
                    else
                    {
                        Users user = new Users
                        {
                            RoleID = 2,
                            FirstName = signupviewmodel.FirstName,
                            LastName = signupviewmodel.LastName,
                            Email = signupviewmodel.Email,
                            Password = signupviewmodel.Password,
                            CreatedDate = DateTime.Now,
                            IsActive = true
                        };

                        context.Users.Add(user);
                        context.SaveChanges();
                        BuildEmailVerifyTemplate(user.ID);
                        context.Dispose();
                        ViewBag.status = 1;
                        return View();
                    }
                }
            }
            else
            {
                return View();
            }
        }


        //EmailVerification
        //GET : Account/VerifyEmail
        public ActionResult VerifyEmail(int regID)
        {
            ViewBag.regID = regID;
            return View();
        }

        public ActionResult RegisterConfirm(int regID)
        {
            using (var context = new NotesMarketPlaceEntities())
            {
                Users user = context.Users.FirstOrDefault(x => x.ID == regID);
                user.IsEmailVerified = true;
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        public void BuildEmailVerifyTemplate(int RegID)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "EmailVerification" + ".cshtml");
            using (var context = new NotesMarketPlaceEntities())
            {
                var regInfo = context.Users.FirstOrDefault(x => x.ID == RegID);
                var url = "https://localhost:44300/" + "Account/VerifyEmail?regID=" + RegID;
                body = body.Replace("@ViewBag.ConfirmationLink", url);
                body = body.Replace("@ViewBag.FirstName", regInfo.FirstName);
                body = body.ToString();
                BuildEmailVerifyTemplate(body, "rutvikpipaliya33@gmail.com");
            }
        }

        public static void BuildEmailVerifyTemplate(string bodyText, string sendTo)
        {
            string from, to, bcc, cc, subject, body;

            from = "rutvikpipaliya33@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = "Note Marketplace - Email Verification";

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

        //ForgotPassword
        //GET : Account/ForgotPassword
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //POST : Account/ForgotPassword
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordViewModel forgotpassword)
        {
            if(ModelState.IsValid)
            { 
                using(var context = new NotesMarketPlaceEntities())
                {
                    bool isValid = context.Users.Any(x => x.Email == forgotpassword.Email);

                    if(isValid)
                    {
                        //Generate random password
                        string pwd = GenerateAlphaNumericPwd();
                        TempData["randompass"] = pwd;
                       
                        //Update record
                        var record = context.Users.FirstOrDefault(x => x.Email == forgotpassword.Email);

                        if(record != null)
                        {
                            record.Password = pwd;
                        }
                        context.SaveChanges();

                        ForgotPasswordCredential(forgotpassword.Email, pwd);
                    }
                    else
                    { 
                        ViewBag.emailnotfound = "Email not match of found";
                    }
                }
            }

            return View();
        }

        //Generate random password
        public String GenerateAlphaNumericPwd()
        {
            string numbers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz!@#$%^&*()-=";
            Random objrandom = new Random();
            string passwordString = "";
            string strrandom = string.Empty;
            for (int i = 0; i < 8; i++)
            {
                int temp = objrandom.Next(0, numbers.Length);
                passwordString = numbers.ToCharArray()[temp].ToString();
                strrandom += passwordString;
            }
            return strrandom; 
        }

        public void ForgotPasswordCredential(String userEmail, String newpassword)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "UserCredential" + ".cshtml");
            
                body = body.Replace("@ViewBag.UserEmail", userEmail);
                body = body.Replace("@ViewBag.NewPassword", newpassword);
                body = body.ToString();
            ForgotPasswordCredentials(body, "rutvikpipaliya33@gmail.com");
        }

        public static void ForgotPasswordCredentials(string bodyText, string sendTo)
        {
            string from, to, bcc, cc, subject, body;
            from = "rutvikpipaliya33@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = "New Temporary Password has been created for you";
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
    }
}