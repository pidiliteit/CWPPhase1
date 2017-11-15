using System;
using System.Linq;
using System.Web;

namespace Teckraft.Web.Security
{
    public class VendorDataSecurityModule : IHttpModule
    {
        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.PostAuthorizeRequest += context_PostAuthorizeRequest;
        }

        void context_PostAuthorizeRequest(object sender, EventArgs e)
        {
            try
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated && HttpContext.Current.User.IsInRole("vendor"))
                {
                    if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("/po/"))
                    {
                        string[] splitter = new string[0];
                        splitter[0] = "/po/";
                        var pono = HttpContext.Current.Request.Url.ToString().ToLower().Split(splitter, StringSplitOptions.None).Last().Replace(".pdf", "");
                      //  var poservice = DomainServices.CustomUnityContainer.Container.Resolve<IService<PurchaseOrder>>();
                       // var query = new ListQuery<PurchaseOrder>();
                       // query.AddParameter("po", pono);
                       // poservice.GetByQuery(query);
                    }

                }
            }
            catch
            {
            }
            //throw new NotImplementedException();
        }
    }
}