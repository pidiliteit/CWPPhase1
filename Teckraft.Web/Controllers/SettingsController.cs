using System.Web.Mvc;
//using Teckraft.Core.Domian.SAP;

namespace Teckraft.Web.Controllers
{
    public class SettingsController : Controller
    {
        //
        // GET: /Settings/
        //private readonly IService<Reason> service;

        //public ReasonController(IService<Reason> service)
        //{
        //    this.service = service; 
        //}
        //public SettingsController(Logger logger,IService<Reason> service)
        //{ 
        
        //}
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ReasonForm()
        {
            return PartialView();
        }
        public ActionResult DetailReason()
        {
            return PartialView();
        }
        public ActionResult EditReason()
        {
            return PartialView();
        }
        public ActionResult Reason()
        {
            return PartialView();
        }
        public ActionResult rlam()
        {
            return PartialView();

        }
        public ActionResult rlamform()
        {
            return PartialView();
        }
        public ActionResult DetailRLAM()
        {
            return PartialView();
        }
        public ActionResult EditRLAM()
        {
            return PartialView();
        }
        public ActionResult AddApprovalHierarchy()
        {
            return PartialView();
        }
        public ActionResult ApprovalHierarchy()
        {
            return PartialView();
        }

        public ActionResult ApprovalMatrix()
        {
            return PartialView();
        }
        public ActionResult ApprovalMatrixForm()
        {
            return PartialView();
        }
        
	}
}