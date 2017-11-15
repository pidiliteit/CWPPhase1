using System.Web.Mvc;

namespace Teckraft.Web.Controllers
{
    public class ProjectTaskController : Controller
    {
        //
        // GET: /Requisition/
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult Form()
        {
            return PartialView();
        }
	}
}