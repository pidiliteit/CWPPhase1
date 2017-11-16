using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
//using System.Web.Mvc;
using System.Web.Script.Serialization;
using Teckraft.Core.Domian.Settings;
using Teckraft.Data;
using Teckraft.Services;
using Teckraft.Web.Framework;

namespace Teckraft.Web.API
{
    
    public class UserHelperController : System.Web.Http.ApiController
    {
        private IService<User> _service;
        //IService<Teckraft.Core.Domian.Settings.User> _service;

        public UserHelperController(IService<User> service)
        {
            this._service = service;
            //this._service = _service;
        }


        [HttpGet]
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

            return this._service.GetByQuery(query);
        }


        [HttpPost]
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public Object Post(ApiRequestType requestType)
        {
            var curUser = _service.GetCurrentUser();
            if (requestType.RequestType == "currentUserDetail")
            {
                return _service.GetCurrentUser();
              
            }
           
            else if (requestType.RequestType == "getDepartmentHead")
            {
                ListQuery<User> query = new ListQuery<User>();
                query.PageSize = 999;
                query.Parameters = new List<QueryParameter>();
                query.Parameters.Add(new QueryParameter() { Name = "DHDivisionId", Value = requestType.Parameters.ElementAt(0).Value });
                //query.Parameters.Add(new QueryParameter() { Name = "DepartmentHead", Value = "yes" });
                var list = _service.GetByQuery(query);
                return list;
            }
            else if (requestType.RequestType == "getDeparmtmentOwner")
            {
                ListQuery<User> query = new ListQuery<User>();
                query.PageSize = 999;
                query.Parameters = new List<QueryParameter>();
                query.Parameters.Add(new QueryParameter() { Name = "DODivisionId", Value = requestType.Parameters.ElementAt(0).Value });
                //query.Parameters.Add(new QueryParameter() { Name = "DeparmtmentOwner", Value = "yes" });
                var list = _service.GetByQuery(query);
                return list;
            }
            else if (requestType.RequestType == "ResponsibleUsers")
            {
                ListQuery<User> query = new ListQuery<User>();
                if(requestType.Parameters!= null){
                    foreach(var p in requestType.Parameters){
                       
                        query.PageSize = 999;
                        query.Parameters = new List<QueryParameter>();
                        query.Parameters.Add(new QueryParameter() { Name = "ResponsibleUsers", Value = requestType.Parameters.ElementAt(0).Value });

                        if(p.Name=="filterText"){
                             query.Parameters.Add(new QueryParameter() { Name = "filterText", Value = p.Value });
                        }
                    }
                }
                 
                var list = _service.GetByQuery(query);
                return list;
            }
            else if (requestType.RequestType == "User")
            {
                ListQuery<User> query = new ListQuery<User>();
                if (requestType.Parameters != null)
                {
                    foreach (var p in requestType.Parameters)
                    {

                        query.PageSize = 999;
                        query.Parameters = new List<QueryParameter>();
                        query.Parameters.Add(new QueryParameter() { Name = "User", Value = requestType.Parameters.ElementAt(0).Value });

                        if (p.Name == "filterText")
                        {
                            query.Parameters.Add(new QueryParameter() { Name = "filterText", Value = p.Value });
                        }
                    }
                }

                var list = _service.GetByQuery(query);
                return list;
            }
            else if (requestType.RequestType == "DependentUsers")
            {
                ListQuery<User> query = new ListQuery<User>();
                if (requestType.Parameters != null)
                {
                    foreach (var p in requestType.Parameters)
                    {
                        
                        query.PageSize = 999;
                        query.Parameters = new List<QueryParameter>();
                        query.Parameters.Add(new QueryParameter() { Name = "DependentUsers", Value = requestType.Parameters.ElementAt(0).Value });

                        if (p.Name == "filterText")
                        {
                            query.Parameters.Add(new QueryParameter() { Name = "filterText", Value = p.Value });
                        }

                    }
                }
                            var list = _service.GetByQuery(query);
                        return list;
            }
            return null;
        }

        public class ApiRequestType
        {
            public List<QueryParameter> Parameters { get; set; }
            public string RequestType { get; set; }
        }
        
    }
}