using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teckraft.Core.Caching;
using Teckraft.Core.Domian;
using Teckraft.Core.Domian.SAP;
using Teckraft.Core.Domian.Settings;
using Teckraft.Core.Infrastructure;
using Teckraft.Data;
using Teckraft.Services;
using Teckraft.Services.Settings;


namespace Teckraft.Web.Framework.Caching
{
    public static class ReasonServiceExtention
    {

        public static List<Reason> ToListCached<T>(this T service)
          where T : IService <Reason>
        {
            var attrs = service.GetType().GetProperty("ToList").GetCustomAttributes(false);
            foreach (CacheAttribute attr in attrs)
            {
                if (attr != null)
                {
                    if (attr.Enable)
                    {
                        return EngineContext.Current.CacheManager.Get(attr.Key, 60, () =>
                        {
                            return service.ToList;
                        });
                    }

                }

            }

            return service.ToList;
        }


        public static ListQueryResult<Reason> GetByCachedQuery<T>(this T service, ListQuery<Reason> query)
         where T : IService<Reason>
        {
            var attrs = service.GetType().GetMethod("GetByQuery").GetCustomAttributes(false);
            foreach (CacheSource attr in attrs)
        {
            if (attr != null)
            {
                if (attr.Enable)
                {
                    
                    var listquery = from o in service.ToListCached()
                                    where o.IsActive = true
                                    select o;
                    var result = new ListQueryResult<Reason>();
                    result.CurrentPage = query.CurrentPage;
                    result.Items = listquery.ToList();
                    result.PageSize = listquery.Count();
                    result.TotalRecords = listquery.Count();
                    return result;
                }
            }

        }
            return service.GetByQuery(query);
        }
    }
}