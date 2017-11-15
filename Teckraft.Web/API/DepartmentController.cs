using System.Collections.Generic;
using System.Web.Http;
using System.Web.Script.Serialization;
using Teckraft.Core.Domian.Settings;
using Teckraft.Data;
using Teckraft.Services;

namespace Teckraft.Web.API
{
    public class DepartmentController : ApiController
    {
        private readonly IReadService<Department> _service;
        public DepartmentController(IReadService<Department> service)
        {
            this._service = service; 
        }
        public ListQueryResult<Department> Get(string q)
        {
            ListQuery<Department> query = new ListQuery<Department>();
            if (!string.IsNullOrEmpty(q))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                query = js.Deserialize<ListQuery<Department>>(q);
            }
            if (query == null) query = new ListQuery<Department>() { CurrentPage = 1, PageSize = 200000 };
            if (query.Parameters == null) query.Parameters = new List<QueryParameter>();

            return this._service.GetByQuery(query);
        }
    }
}