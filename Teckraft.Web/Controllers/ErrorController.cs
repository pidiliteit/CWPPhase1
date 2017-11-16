using System.Web.Mvc;

namespace ASF.Controllers
{
    public class ErrorController : Controller
    {
        [HandleError]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]

        public ActionResult Error()
        {
            //ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View("Error");
        }
        public ActionResult NotFound()
        {
            return View("Not Found");
        }
    }
}