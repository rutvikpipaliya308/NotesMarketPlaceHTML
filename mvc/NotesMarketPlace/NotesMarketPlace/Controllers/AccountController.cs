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
        readonly NotesMarketPlaceEntities db;
        public AccountController()
        {
            db = new NotesMarketPlaceEntities();
        }

        // GET: Account/Login
        [HttpGet]
        [AllowAnonymous]
        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        //POST : Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [Route("Login")]
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
                    
                    var isUser = db.Users.Where(x => x.Email.Equals(userlogin.Email) && x.Password.Equals(userlogin.Password) && x.IsActive == true).FirstOrDefault();
                                       
                    //Wrong Password
                    if (isUser == null)
                    {
                        ModelState.AddModelError("Password", "Incorrect Password");
                        return View("Login");
                    }

                    //Account is Not Active
                    if (!isUser.IsActive)
                    {
                        ModelState.AddModelError("IsActive", "Your account is not activated.");
                        return RedirectToAction("Index", "Home");
                    }

                    //Email not Varified
                    if (!isUser.IsEmailVerified)
                    {
                        ViewBag.EmailNotVerified = "Your email is not verified. check your email to verify.";
                        return View("Login");
                    }

                    if (isUser != null && isUser.IsActive == true && isUser.IsEmailVerified == true)
                    {
                        //Remember me check box
                        FormsAuthentication.SetAuthCookie(userlogin.Email, true);

                        Session["ID"] = isUser.ID;
                        Session["Email"] = isUser.Email;

                        //For Member
                        if (isUser.RoleID == 2)
                        {
                            int userId = db.Users.FirstOrDefault(x => x.Email == isUser.Email && x.IsActive == true).ID;
                            bool updatedProfile = db.UserProfile.Any(x => x.UserID == userId && x.IsActive == true);

                            if (updatedProfile)
                            {
                                return RedirectToAction("Index", "Home");                                                               
                            }
                            else
                            {
                                return RedirectToAction("MyProfile", "UserProfile");
                            }
                            
                        }
                        else if(isUser.RoleID == 3 || isUser.RoleID == 4)
                        {
                            return RedirectToAction("AdminDashboard", "AdminDashboard");
                        }                        
                        else
                        {
                            return RedirectToAction("Login", "Account");
                        }
                    }
                }
            }
            return View("Login");
        }

        //Logout
        [HttpGet]
        public ActionResult Logout()
        {
            int admin = 0;
            if(Session["ID"] != null)
            {
                admin = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name).RoleID;
            }            
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            Session["ID"] = null;
            Session["Email"] = null;
            Session.RemoveAll();

            if(admin == 3 || admin == 4)
            {
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("Index", "Home");
        }

        //SignUp
        // GET: Account/SignUp
        [HttpGet]
        [AllowAnonymous]
        [Route("Signup")]
        public ActionResult SignUp()
        {            
            return View();
        }

        //POST : Account/SignUp
        [HttpPost]
        [AllowAnonymous]
        [Route("Signup")]
        public ActionResult SignUp([Bind(Include = "FirstName,LastName,Email,Password,ConfirmPassword")] UsersViewModel signupviewmodel)
        {
            if (ModelState.IsValid)
            {
                using (var db = new NotesMarketPlaceEntities())
                {
                    bool emailalreadyexists = db.Users.Any(x => x.Email == signupviewmodel.Email);

                    if (emailalreadyexists)
                    {
                        ModelState.AddModelError("Email", "Email already registered");
                        return View();
                    }
                    else
                    {
                        //add user data 
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

                        db.Users.Add(user);
                        db.SaveChanges();
                        BuildEmailVerifyTemplate(user.ID); //sending email to user  
                        db.Dispose();                                                                      
                        ViewBag.sucess = " your account has been sucessfully created.";
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
        [HttpGet]
        public ActionResult VerifyEmail(int regID)
        {
            ViewBag.regID = regID;
            return View();
        }
        
        [HttpGet]
        public ActionResult RegisterConfirm(int regID)
        {
            using (var db = new NotesMarketPlaceEntities())
            {
                Users user = db.Users.FirstOrDefault(x => x.ID == regID);
                user.IsEmailVerified = true;
                user.CreatedBy = user.ID;
                user.ModifiedDate = DateTime.Now;
                user.ModifiedBy = user.ID;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        public void BuildEmailVerifyTemplate(int RegID)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "EmailVerification" + ".cshtml");
            using (var context = new NotesMarketPlaceEntities())
            {
                var regInfo = context.Users.FirstOrDefault(x => x.ID == RegID);
                var url = "https://localhost:44300/" + "Account/VerifyEmail?regID=" + RegID; //email verify link
                body = body.Replace("@ViewBag.ConfirmationLink", url);
                body = body.Replace("@ViewBag.FirstName", regInfo.FirstName);
                body = body.ToString();
                BuildEmailVerifyTemplate(body, "rmpipaliya308@gmail.com"); //user email address
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
                using(var db = new NotesMarketPlaceEntities())
                {
                    bool isValid = db.Users.Any(x => x.Email == forgotpassword.Email);

                    if(isValid)
                    {
                        //Generate random password
                        string pwd = GenerateAlphaNumericPwd();
                        TempData["randompass"] = pwd;
                       
                        //Update record
                        var record = db.Users.FirstOrDefault(x => x.Email == forgotpassword.Email);

                        if(record != null)
                        {
                            record.Password = pwd;
                            record.ModifiedDate = DateTime.Now;
                        }
                        db.SaveChanges();

                        //sending new password to user
                        ForgotPasswordCredential(forgotpassword.Email, pwd);                        
                        ViewBag.sucess = "New Password sent to you";
                    }
                    else
                    { 
                        ModelState.AddModelError("Email", "Email not found");
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
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "TempPassword" + ".cshtml");            
            body = body.Replace("@ViewBag.UserEmail", userEmail);
            body = body.Replace("@ViewBag.NewPassword", newpassword);
            body = body.ToString();
            ForgotPasswordCredentials(body, "rmpipaliya308@gmail.com");
        }

        [HandleError]
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
            SendEmail(mail); //calling send mail function
        }
    }
}