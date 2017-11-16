using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using Teckraft.Core.Domian.Settings;
using Teckraft.Data;
using Teckraft.Services;
using Teckraft.Web.App_Start;
using Teckraft.Web.Framework;

namespace Teckraft.Web.API
{
    public class ApprovalUserController : ApiController
    {
        private IService<User> _service;
        IService<Teckraft.Core.Domian.Settings.User> _userservice;

        public ApprovalUserController(IService<User> _service, IService<Teckraft.Core.Domian.Settings.User> _userservice)
        {
            this._service = _service;
            this._userservice = _userservice;
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ListQueryResult<User> Get(ListQuery<User> query)
        {
            var curuser = _userservice.GetCurrentUser();
            if (query == null) query = new ListQuery<User>() { CurrentPage = 1, PageSize = 200 };
            //query.
            var list = _service.GetByQuery(query);
            list.Items.ForEach(it => { if (it.Id == curuser.Id) it.Title = "Me"; });
            list.Items = list.Items.OrderBy(it => it.Title).ToList();
            return list;

        }
        public static string GetCombinedAntiForgeryTokens(string username)
        {
            string cookieToken, formToken;
            AntiForgery.GetTokens(null, out cookieToken, out formToken);
            HttpContext.Current.Cache[username + "_formToken"] = formToken;
            return cookieToken + ":" + formToken;
        }
        public HttpResponseMessage Get([FromUri]string requestType)
        {
            if (requestType == "currentUserDetail")
            {
                var user = _userservice.GetCurrentUser();
                user.CSFRToken = GetCombinedAntiForgeryTokens(user.UserName);
                return getMessge(user);
            }

            return getMessge(new User());
        }
        private HttpResponseMessage getMessge(object result)
        {
            var serializer = new JsonMediaTypeFormatter() { SerializerSettings = WebApiConfig.JsonSerializerSettings };
            System.IO.TextWriter writer = new System.IO.StringWriter();

            serializer.CreateJsonSerializer().Serialize(writer, result);

            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(writer.ToString(), Encoding.UTF8, "application/json");
            return response;

        }
    }
}