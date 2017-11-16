using System.Web.Mvc;
using Teckraft.Services;
using Teckraft.Web.Framework;


namespace Teckraft.Web.Controllers
{
    public class HomeController : Controller
    {
        IService<Teckraft.Core.Domian.Settings.User> _userservice;

      // IReadService<Teckraft.Core.Domian.Transactions.Attachment> attachmentService;
       
        public HomeController(IService<Teckraft.Core.Domian.Settings.User> _userservice)
        {
            this._userservice = _userservice;

        }
        
        // GET: /Home/
       [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
            ViewBag.currentUserTitle = _userservice
                .GetCurrentUser().Title;

            //ViewBag.isApproval = _userservice
            //    .GetCurrentUser().IsApproval;
            
            //if (Session["sessionid"] == null) {
            //    Session.Abandon();
            //    return Redirect("/account/login");
            //}
            ////ViewBag.currentUserDivision = _userservice.GetCurrentUser().Division.DivisionCode;
            //if (System.Web.HttpContext.Current.Application[Session["sessionid"].ToString()] == null)
            //{
            //    Session.Abandon();
            //    return Redirect("/account/login");
            //}
            return View();
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Dashboard() {
            return PartialView();
        }

       
	}
}