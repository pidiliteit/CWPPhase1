using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Teckraft.Core.Domian.Settings;
using Teckraft.Web.Models;
using WebMatrix.WebData;
namespace Teckraft.Web.Controllers
{

    public class AccountController : Controller
    {


        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            //if (User.Identity.IsAuthenticated)
            //    return View("Unauthorized");
            //else Response.Redirect("http://localhost:8052/account/login?wa=wsignin1.0&wtrealm=http://localhost:6080/&wctx=rm%3d0%26id%3dpassive%26ru%3d%252f&wct=2014-08-13T09%3a13%3a35Z");

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            ViewBag.multipleLogin = "false";
            if (Settings.EnableOTP)
            {
                TestOTPWebService.WebServiceSoapClient objOTP = new TestOTPWebService.WebServiceSoapClient();
                string uname = model.UserName;
                string pass = model.Password;
                string[] emailRegex = { "-", "<", ">", ";", "'" };
                for (var i = 0; i < emailRegex.Length; i++)
                {
                    if (model.UserName != null)
                        model.UserName = model.UserName.Replace(emailRegex[i], " ");

                    if (model.Password != null)
                        model.Password = model.Password.Replace(emailRegex[i], " ");

                }
                

                string appName = "SSOEMP";
                string appCode = "SSOEMP";

                var result = objOTP.Login(Encryptdata(uname), Encryptdata(pass), Encryptdata(appName), Encryptdata(appCode));
                
                if (WebSecurity.IsAccountLockedOut(model.UserName, 5, 100))
                {
                    ModelState.AddModelError("", "Account is locked due to multiple failed login attempts.");
                }
                else if (WebSecurity.IsConfirmed(model.UserName) == false) {
                    ModelState.AddModelError("", "Account disabled");
                }
                else if (ModelState.IsValid && result)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    var usr = WebSecurity.GetUserId(model.UserName);
                    

                    

                    Teckraft.Data.Sql.InitiativeHubFinalEntities DbContext = new Teckraft.Data.Sql.InitiativeHubFinalEntities();
                    var dt = DateTime.Now.AddMinutes(-22);
                    foreach (var item in DbContext.CurrentUserSessions.Where(it => it.UserId == usr && it.RCT < dt))
                    {
                        DbContext.CurrentUserSessions.Remove(item);
                    }
                    DbContext.SaveChanges();

                    var curSession = DbContext.CurrentUserSessions.FirstOrDefault(it => it.UserId == usr);
                    if (curSession != null && !model.ForcefullLogout)
                    {
                        ViewBag.multipleLogin = "true";
                        ModelState.AddModelError("", "Mutiple login not allowed");
                        WebSecurity.Logout();
                    }
                    else
                    {
                        if (curSession != null)
                        {
                            System.Web.HttpContext.Current.Application[curSession.SessionId] = null;
                            DbContext.CurrentUserSessions.Remove(curSession);
                        }
                        DbContext.CurrentUserSessions.Add(new Teckraft.Data.Sql.CurrentUserSession() { UserId = usr, RCT = DateTime.Now, SessionId = Session.SessionID });
                        DbContext.SaveChanges();
                        Session["sessionid"] = Session.SessionID;
                        System.Web.HttpContext.Current.Application[Session.SessionID.ToString()] = Session.SessionID;
                        return RedirectToLocal(returnUrl);
                    }

                }
                else
                {
                    if (WebSecurity.UserExists(model.UserName) && !WebSecurity.IsConfirmed(model.UserName))
                    {
                        ModelState.AddModelError("", "Your account has been blocked. Please contact SSO admin.");
                    }
                    else
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }

            }
            else
            {

                string[] emailRegex = { "-", "<", ">", ";", "'" };
                for (var i = 0; i < emailRegex.Length; i++)
                {
                    if (model.UserName != null)
                        model.UserName = model.UserName.Replace(emailRegex[i], " ");

                    if (model.Password != null)
                        model.Password = model.Password.Replace(emailRegex[i], " ");

                }


                if (WebSecurity.IsAccountLockedOut(model.UserName, 5, 100))
                {
                    ModelState.AddModelError("", "Account is locked due to multiple failed login attempts.");
                }

                else if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
                {
                    if (String.IsNullOrEmpty(returnUrl))
                    {
                      //  returnUrl = "/";
                    }

                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    var usr = WebSecurity.GetUserId(model.UserName);
                    Teckraft.Data.Sql.InitiativeHubFinalEntities DbContext = new Teckraft.Data.Sql.InitiativeHubFinalEntities();
                    var dt = DateTime.Now.AddMinutes(-22);
                    foreach (var item in DbContext.CurrentUserSessions.Where(it => it.UserId == usr && it.RCT < dt))
                    {
                        DbContext.CurrentUserSessions.Remove(item);
                    }
                    DbContext.SaveChanges();
                   
                    var curSession = DbContext.CurrentUserSessions.FirstOrDefault(it => it.UserId == usr);
                    if (curSession != null && !model.ForcefullLogout)
                    {

                        ModelState.AddModelError("", "Mutiple login not allowed");
                        ViewBag.multipleLogin = "true";
                        WebSecurity.Logout();
                    }
                    else
                    {
                        if (curSession != null)
                        {
                            System.Web.HttpContext.Current.Application[curSession.SessionId] = null;
                            DbContext.CurrentUserSessions.Remove(curSession);
                        }
                        DbContext.CurrentUserSessions.Add(new Teckraft.Data.Sql.CurrentUserSession() { UserId = usr, RCT = DateTime.Now, SessionId = Session.SessionID });
                        DbContext.SaveChanges();
                        Session["sessionid"] = Session.SessionID;
                        System.Web.HttpContext.Current.Application[Session.SessionID.ToString()] = Session.SessionID;

                        Session["ASP.NET_SessionId"] = Session.SessionID;
                        Response.Cookies["ASP.NET_SessionId"].Value = Session.SessionID;
                        Response.Cookies["LogOutState"].Value = "false";

                        return RedirectToLocal(returnUrl);
                    }
                }
                else
                {
                    if (WebSecurity.UserExists(model.UserName) && !WebSecurity.IsConfirmed(model.UserName))
                    {
                        ModelState.AddModelError("", "Your account has been blocked. Please contact SSO admin.");
                    }
                    else
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        
        [AllowAnonymous]
        public ActionResult Loginnew(string UserName)
        {
            //string decUserName = Decryptdata(UserName);
            LoginModel model = new LoginModel();
            //model.UserName = decUserName;
            model.UserName = UserName;
            // model.Password = Password;
            string returnUrl = null;

            ViewBag.multipleLogin = "false";
            //if (Settings.EnableOTP)
            //{
            //    string constrSSO = ConfigurationManager.ConnectionStrings["OTPConnectionString"].ConnectionString;
            //    string Password = "";
            //    using (SqlConnection con1 = new SqlConnection(constrSSO))
            //    {
            //        using (SqlCommand cmd1 = new SqlCommand())
            //        {
            //            cmd1.CommandText = "GetPassword";
            //            cmd1.CommandType = CommandType.StoredProcedure;
            //            cmd1.Parameters.AddWithValue("@UserName", UserName);
            //            cmd1.Parameters.AddWithValue("@ApplnName", "SSOEMP");
            //            cmd1.Parameters.AddWithValue("@ApplnCode", "SSOEMP");
            //            con1.Open();
            //            cmd1.Connection = con1;
            //            DataTable dt = new DataTable();
            //            SqlDataAdapter da = new SqlDataAdapter();
            //            da.SelectCommand = cmd1;
            //            da.Fill(dt);
            //            foreach (DataRow row in dt.Rows)
            //            {
            //                Password = row["password"].ToString();
            //            }
            //         }
            //    }
            //    if (Password != "")
            //    {
            //        model.Password = Password;
            //    }

            //    TestOTPWebService.WebServiceSoapClient objOTP = new TestOTPWebService.WebServiceSoapClient();
            //    string uname = model.UserName;
            //    string pass = model.Password;
            //    string[] emailRegex = { "-", "<", ">", ";", "'" };
            //    for (var i = 0; i < emailRegex.Length; i++)
            //    {
            //        if (model.UserName != null)
            //            model.UserName = model.UserName.Replace(emailRegex[i], " ");

            //        if (model.Password != null)
            //            model.Password = model.Password.Replace(emailRegex[i], " ");

            //    }


            //    string appName = "SSOEMP";
            //    string appCode = "SSOEMP";

            //    var result = objOTP.Login(Encryptdata(uname), Encryptdata(pass), Encryptdata(appName), Encryptdata(appCode));

            //    if (WebSecurity.IsAccountLockedOut(model.UserName, 5, 100))
            //    {
            //        ModelState.AddModelError("", "Account is locked due to multiple failed login attempts.");
            //    }
            //    else if (WebSecurity.IsConfirmed(model.UserName) == false)
            //    {
            //        ModelState.AddModelError("", "Account disabled");
            //    }
            //    else if (ModelState.IsValid && result)
            //    {
            //        FormsAuthentication.SetAuthCookie(model.UserName, false);
            //        var usr = WebSecurity.GetUserId(model.UserName);




            //        Teckraft.Data.Sql.InitiativeHubFinalEntities DbContext = new Teckraft.Data.Sql.InitiativeHubFinalEntities();
            //        var dt = DateTime.Now.AddMinutes(-22);
            //        foreach (var item in DbContext.CurrentUserSessions.Where(it => it.UserId == usr && it.RCT < dt))
            //        {
            //            DbContext.CurrentUserSessions.Remove(item);
            //        }
            //        DbContext.SaveChanges();

            //        var curSession = DbContext.CurrentUserSessions.FirstOrDefault(it => it.UserId == usr);
            //        if (curSession != null && !model.ForcefullLogout)
            //        {
            //            ViewBag.multipleLogin = "true";
            //            ModelState.AddModelError("", "Mutiple login not allowed");
            //            WebSecurity.Logout();
            //        }
            //        else
            //        {
            //            if (curSession != null)
            //            {
            //                System.Web.HttpContext.Current.Application[curSession.SessionId] = null;
            //                DbContext.CurrentUserSessions.Remove(curSession);
            //            }
            //            DbContext.CurrentUserSessions.Add(new Teckraft.Data.Sql.CurrentUserSession() { UserId = usr, RCT = DateTime.Now, SessionId = Session.SessionID });
            //            DbContext.SaveChanges();
            //            Session["sessionid"] = Session.SessionID;
            //            System.Web.HttpContext.Current.Application[Session.SessionID.ToString()] = Session.SessionID;
            //            return RedirectToLocal(returnUrl);
            //        }

            //    }
            //    else
            //    {
            //        if (WebSecurity.UserExists(model.UserName) && !WebSecurity.IsConfirmed(model.UserName))
            //        {
            //            ModelState.AddModelError("", "Your account has been blocked. Please contact SSO admin.");
            //        }
            //        else
            //            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            //    }

            //}
           // else
           // {
                model.Password = "abc@1234";
          
                string[] emailRegex = { "-", "<", ">", ";", "'" };
                for (var i = 0; i < emailRegex.Length; i++)
                {
                    if (model.UserName != null)
                        model.UserName = model.UserName.Replace(emailRegex[i], " ");

                    if (model.Password != null)
                        model.Password = model.Password.Replace(emailRegex[i], " ");

                }


                if (WebSecurity.IsAccountLockedOut(model.UserName, 5, 100))
                {
                    ModelState.AddModelError("", "Account is locked due to multiple failed login attempts.");
                }

                else if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
                {
                    if (String.IsNullOrEmpty(returnUrl))
                    {
                        //  returnUrl = "/";
                    }

                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    var usr = WebSecurity.GetUserId(model.UserName);
                    Teckraft.Data.Sql.InitiativeHubFinalEntities DbContext = new Teckraft.Data.Sql.InitiativeHubFinalEntities();
                    var dt = DateTime.Now.AddMinutes(-22);
                    foreach (var item in DbContext.CurrentUserSessions.Where(it => it.UserId == usr && it.RCT < dt))
                    {
                        DbContext.CurrentUserSessions.Remove(item);
                    }
                    DbContext.SaveChanges();

                    var curSession = DbContext.CurrentUserSessions.FirstOrDefault(it => it.UserId == usr);
                    //if (curSession != null && !model.ForcefullLogout)
                    //{

                    //    ModelState.AddModelError("", "Mutiple login not allowed");
                    //    ViewBag.multipleLogin = "true";
                    //    WebSecurity.Logout();
                    //}
                    //else
                    //{
                        if (curSession != null)
                        {
                            System.Web.HttpContext.Current.Application[curSession.SessionId] = null;
                            DbContext.CurrentUserSessions.Remove(curSession);
                        }
                        DbContext.CurrentUserSessions.Add(new Teckraft.Data.Sql.CurrentUserSession() { UserId = usr, RCT = DateTime.Now, SessionId = Session.SessionID });
                        DbContext.SaveChanges();
                        Session["sessionid"] = Session.SessionID;
                        System.Web.HttpContext.Current.Application[Session.SessionID.ToString()] = Session.SessionID;

                        Session["ASP.NET_SessionId"] = Session.SessionID;
                        Response.Cookies["ASP.NET_SessionId"].Value = Session.SessionID;
                        Response.Cookies["LogOutState"].Value = "false";

                        return RedirectToLocal(returnUrl);
                    //}
                }
                else
                {
                    if (WebSecurity.UserExists(model.UserName) && !WebSecurity.IsConfirmed(model.UserName))
                    {
                        ModelState.AddModelError("", "Your account has been blocked. Please contact SSO admin.");
                    }
                    else
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            //}
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult LogOff()
        {
            var test = User.Identity.Name;
            Teckraft.Data.Sql.InitiativeHubFinalEntities DbContext = new Teckraft.Data.Sql.InitiativeHubFinalEntities();
            var userprofile = DbContext.UserProfiles.FirstOrDefault(it => it.UserName.ToLower() == test.ToLower());
            var curSession = DbContext.CurrentUserSessions.FirstOrDefault(it => it.UserId == userprofile.UserId);
            if (curSession != null)
            {
                DbContext.CurrentUserSessions.Remove(curSession);
            }
            DbContext.SaveChanges();
            Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
            Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddDays(-2); //Delete the cookie
            //return Redirect("http://localhost:8052/account/LogOff?wa=wsignout1.0");
            WebSecurity.Logout();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult GenerateOTP(string UserName)
        {
            //if(
            string[] emailRegex = { "-", "<", ">", ";", "'" };
            for (var i = 0; i < emailRegex.Length; i++)
            {
                if (UserName != null)
                   UserName = UserName.Replace(emailRegex[i], " ");
            }

            Teckraft.Web.Models.DbActionResult<string> result = new Teckraft.Web.Models.DbActionResult<string>();
            Teckraft.Data.Sql.InitiativeHubFinalEntities DbContext = new Teckraft.Data.Sql.InitiativeHubFinalEntities();
            var UserExist = DbContext.UserProfiles.FirstOrDefault(it => it.UserName.ToLower() == UserName.ToLower());
            if (UserExist != null && WebSecurity.IsConfirmed(UserName))
            {
                var User = DbContext.UserDetails.FirstOrDefault(it => it.UserName.ToLower() == UserName.ToLower());
                if (string.IsNullOrEmpty(User.MobileNo) && string.IsNullOrEmpty(User.Email))
                {
                    result.Message = "User's Email address and Mobile number are not updated";
                    result.Success = false;
                }
                else
                {
                    TestOTPWebService.WebServiceSoapClient pilcrsOTP = new TestOTPWebService.WebServiceSoapClient();
                    var res = false;
                    if (string.IsNullOrEmpty(User.MobileNo)) User.MobileNo = "2222222222";
                    if (!(string.IsNullOrEmpty(User.MobileNo)))
                    {
                        try
                        {

                            res = pilcrsOTP.Password(Encryptdata(UserName), Encryptdata(User.MobileNo), Encryptdata("SSOEMP"), Encryptdata("SSOEMP"), Encryptdata("0"));
                            result.Message = "An OTP message has been sent to your number or email";
                            result.Success = true;
                        }
                        catch(Exception ex) {
                            result.Message = "Error sending OTP please try later";
                        }
                        
                    }
                    if (!(string.IsNullOrEmpty(User.Email)))
                    {
                        try
                        {
                            //Models.OTP.OTPEntities dbcontext = new Models.OTP.OTPEntities();
                            //var otp = dbcontext.OTPs.OrderByDescending(it => it.Time).FirstOrDefault(it => it.UserName == User.UserName && it.ApplnCode == "ASF");
                            //if (otp != null)
                            //{
                            //    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                            //    client.Send("CRSytem@pidilite.com", User.Email, "OTP for Pidilite ASF System", "Dear user\n Your OTP for Pidilite ASF System is " + otp.Password + "\nRegards\nASF System Team");
                            //    result.Message = "An OTP message has been sent to your number or email";
                            //}
                        }
                        catch (Exception ex) {
                            result.Message = "Error sending OTP please try later";
                        }
                        
                    }
                }
            }
            else
            {
                result.Message = "Invalid Username";
                result.Success = false;
            }
            return Json(result);
        }


        private string Encryptdata(string retUsrename)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[retUsrename.Length];
            encode = Encoding.UTF8.GetBytes(retUsrename);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }

        public static string Decryptdata(string retUsrename) //Decode    
        {
            byte[] encoded = Convert.FromBase64String(retUsrename);
            return System.Text.Encoding.UTF8.GetString(encoded);

        }
        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }




        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}