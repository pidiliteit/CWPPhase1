using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using Teckraft.Core.Domian.Settings;
using Teckraft.Data;
using Teckraft.Services;

namespace Teckraft.Web.API
{
    public class PendingEmpController:ApiController
    {
        IService<User> _userservice;
        public PendingEmpController(IService<User> _userservice)
        {
            this._userservice=_userservice;
        }
        public ListQueryResult<User> Get(string q)
        {
            ListQuery<User> query = new ListQuery<User>();
            if (!string.IsNullOrEmpty(q))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                query = js.Deserialize<ListQuery<User>>(q);
            }
            if (query == null) query = new ListQuery<User>() { CurrentPage = 1, PageSize = 200000 };
            if (query.Parameters == null) query.Parameters = new List<QueryParameter>();

            return this._userservice.GetByQuery(query);
        }
        [HttpPost]
        public ListQueryResult<User> Post(string q)
        {
            ListQuery<User> query = new ListQuery<User>();
            if (!string.IsNullOrEmpty(q))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                query = js.Deserialize<ListQuery<User>>(q);
            }
            if (query == null) query = new ListQuery<User>() { CurrentPage = 1, PageSize = 200000 };
            if (query.Parameters == null) query.Parameters = new List<QueryParameter>();

            return this._userservice.GetByQuery(query);
        }
    }
}