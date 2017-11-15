using System.Web.Mvc;
//using Teckraft.Core.Domian.SAP;

namespace Teckraft.Web.Controllers
{
    public class SAPController : Controller
    {

        public SAPController(Logger logger)
        { 
            
        }
        public ActionResult Division() {

            return PartialView();
        }
        public ActionResult Customer()
        {
            return PartialView();
        }
        public ActionResult Plant()
        {
            return PartialView();
        }
        public ActionResult SalesGroup()
        {
            return PartialView();

        }
    }
}