using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Teckraft.Web.App_Start;
using WebMatrix.WebData;
//using System.Web.Optimization;

namespace Teckraft.Web
{
    public class Global2 : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
            MvcHandler.DisableMvcResponseHeader = true;
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Teckraft.Core.Infrastructure.EngineContext.Initialize(true);
            AuthConfig.RegisterAuth();
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Teckraft.Data.Sql.InitiativeHubFinalEntities DbContext = new Teckraft.Data.Sql.InitiativeHubFinalEntities();
            var curSession = DbContext.CurrentUserSessions.FirstOrDefault(it => it.SessionId == Session.SessionID);
            if (curSession != null)
            {
                Application[Session.SessionID.ToString()] = null;
                DbContext.CurrentUserSessions.Remove(curSession);
                DbContext.SaveChanges();
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

            try
            {
                Response.AddHeader("X-Frame-Options", "SAMEORIGIN");
            }
            catch
            {

                // throw;
            }
            //need to uncomment
            if (Request.Cookies["ASP.NET_SessionId"] != null && Request.Cookies["ASP.NET_SessionId"].Value != null)
            {
                //string newSessionID = Request.Cookies["ASP.NET_SessionID"].Value;
                ////Check the valid length of your Generated Session ID
                //if (newSessionID.Length <= 24)
                //{
                //    //Log the attack details here
                //    Response.Cookies["TriedTohack"].Value = "True";
                //    throw new HttpException("Invalid Request");
                //}

                ////Genrate Hash key for this User,Browser and machine and match with the Entered NewSessionID
                //if (GenerateHashKey() != newSessionID.Substring(24))
                //{
                //    //Log the attack details here
                //    Response.Cookies["TriedTohack"].Value = "True";
                //    throw new HttpException("Invalid Request");
                //}

                //Use the default one so application will work as usual//ASP.NET_SessionId
                Request.Cookies["ASP.NET_SessionId"].Value = Request.Cookies["ASP.NET_SessionId"].Value.Substring(0, 24);
            }

            if (Request.Cookies["LogOutState"] == null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Request.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
                Response.Cookies.Remove("ASP.NET_SessionId");
                Request.Cookies.Remove("ASP.NET_SessionId");
            }
            //need to uncomment

        }
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            //return;
            //Pass the custom Session ID to the browser.
            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = Request.Cookies["ASP.NET_SessionId"].Value + GenerateHashKey();
                //var cookie = Request.Cookies["ASP.NET_SessionId"].Value;
                //HttpContext.Current.Session["ASP.NET_SessionId"] = cookie;
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            if (Request.Cookies["ASP.NET_SessionId"] != null && Request.Cookies["ASP.NET_SessionId"].Value != null)
            {
                if (Request.Cookies["LogOutState"] == null)
                {
                    SHA1 sha = new SHA1CryptoServiceProvider();
                    byte[] hashData = sha.ComputeHash(Encoding.UTF8.GetBytes(Session.SessionID));
                    string encrypted = Convert.ToBase64String(hashData);
                    //Session.Add("Auth", encrypted);
                    Response.Cookies["Auth"].Value = encrypted;
                }
                else if (Request.Cookies["LogOutState"] != null && Request.Cookies["LogOutState"].Value != null)
                {
                    var request = Request.Cookies["ASP.NET_SessionId"].Value;
                    //var auth = Session["Auth"].ToString();
                    var auth = Request.Cookies["Auth"].Value;
                    SHA1 sha = new SHA1CryptoServiceProvider();
                    byte[] hashData = sha.ComputeHash(Encoding.UTF8.GetBytes(request));
                    string chk = Convert.ToBase64String(hashData);
                    if (auth != chk)
                    {
                        System.Web.SessionState.SessionIDManager Manager = new System.Web.SessionState.SessionIDManager();
                        string NewID = Manager.CreateSessionID(Context);
                        bool IsAdded = false;
                        bool redirected = false;
                        Manager.SaveSessionID(Context, NewID, out redirected, out IsAdded);
                        Request.Cookies["ASP.NET_SessionId"].Value = NewID;
                        Response.Cookies["ASP.NET_SessionId"].Value = NewID;
                        SHA1 sha1 = new SHA1CryptoServiceProvider();
                        byte[] hashData1 = sha.ComputeHash(Encoding.UTF8.GetBytes(NewID));
                        string encrypted = Convert.ToBase64String(hashData1);
                        Response.Cookies["Auth"].Value = encrypted;
                    }
                }
                if (Request.Cookies["LogOutState"] != null && Request.Cookies["LogOutState"].Value == "true")
                {
                    Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                    Request.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                    Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
                    Response.Cookies.Remove("ASP.NET_SessionId");
                    Request.Cookies.Remove("ASP.NET_SessionId");
                    Request.Cookies["LogOutState"].Value = "";
                    Request.Cookies.Remove("LogOutState");
                    Response.Cookies["LogOutState"].Expires = DateTime.Now.AddMonths(-20);
                }
            }
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            if (Request.Cookies["ASP.NET_SessionId"] != null && Request.Cookies["ASP.NET_SessionId"].Value != null)
            {
                if (Request.Cookies["LogOutState"] == null)
                {
                    SHA1 sha = new SHA1CryptoServiceProvider();
                    byte[] hashData = sha.ComputeHash(Encoding.UTF8.GetBytes(Session.SessionID));
                    string encrypted = Convert.ToBase64String(hashData);
                    //Session.Add("Auth", encrypted);
                    Response.Cookies["Auth"].Value = encrypted;
                }
                else if (Request.Cookies["LogOutState"] != null && Request.Cookies["LogOutState"].Value != null)
                {
                    var request = Request.Cookies["ASP.NET_SessionId"].Value;
                    //var auth = Session["Auth"].ToString();
                    var auth = Request.Cookies["Auth"].Value;
                    SHA1 sha = new SHA1CryptoServiceProvider();
                    byte[] hashData = sha.ComputeHash(Encoding.UTF8.GetBytes(request));
                    string chk = Convert.ToBase64String(hashData);
                    if (auth != chk)
                    {
                        System.Web.SessionState.SessionIDManager Manager = new System.Web.SessionState.SessionIDManager();
                        string NewID = Manager.CreateSessionID(Context);
                        bool IsAdded = false;
                        bool redirected = false;
                        Manager.SaveSessionID(Context, NewID, out redirected, out IsAdded);
                        Request.Cookies["ASP.NET_SessionId"].Value = NewID;
                        Response.Cookies["ASP.NET_SessionId"].Value = NewID;
                        SHA1 sha1 = new SHA1CryptoServiceProvider();
                        byte[] hashData1 = sha.ComputeHash(Encoding.UTF8.GetBytes(NewID));
                        string encrypted = Convert.ToBase64String(hashData1);
                        Response.Cookies["Auth"].Value = encrypted;
                    }
                }
            }
        }

        private string GenerateHashKey()
        {
            StringBuilder myStr = new StringBuilder();
            myStr.Append(Request.Browser.Browser);
            myStr.Append(Request.Browser.Platform);
            myStr.Append(Request.Browser.MajorVersion);
            myStr.Append(Request.Browser.MinorVersion);

            myStr.Append(Request.UserHostAddress);
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] hashdata = sha.ComputeHash(Encoding.UTF8.GetBytes(myStr.ToString()));
            return Convert.ToBase64String(hashdata);
        }
        protected void Application_PreSendRequestHeaders(Object source, EventArgs e)
        {
            try
            {
                if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("api/"))
                {
                    HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
                    HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
                    HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                    HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    HttpContext.Current.Response.Cache.SetNoStore();
                }
            }
            catch
            {
            }
        }

        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    Server.ClearError();
        //    Response.Redirect("/error/error");
        //}
    }
}
