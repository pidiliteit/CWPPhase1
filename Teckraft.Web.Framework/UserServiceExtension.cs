using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using Teckraft.Core.Domian.Settings;
using Teckraft.Data;
using Teckraft.Services;
using Teckraft.Services.Settings;
using System.Web.Security;

namespace Teckraft.Web.Framework
{
    public static class UserServiceExtension
    {
        public static User GetCurrentUser(this IService<User> userService)
        {
            var userName = HttpContext.Current.User.Identity.Name;
           // string userRole = Roles.GetRolesForUser(userName)[0].ToString();

            if (System.Web.HttpContext.Current.Cache["User_Context_" + HttpContext.Current.User.Identity.Name] == null)
            {
                ListQuery<User> query = new ListQuery<User>();
                query.CurrentPage = 1; query.PageSize = 1;
                query.Parameters = new List<QueryParameter>();

                query.Parameters.Add(new QueryParameter() { Name = "username", Value = userName, Operator = CompareOperator.Equals });
                User user = userService.GetByQuery(query).Items.ElementAt(0);
               /// User.Current.Role = userRole;
                System.Web.HttpContext.Current.Cache.Add("User_Context_" + HttpContext.Current.User.Identity.Name, user, null, DateTime.Now.AddMinutes(3), TimeSpan.Zero, CacheItemPriority.Default, null);

            }
            return System.Web.HttpContext.Current.Cache["User_Context_" + HttpContext.Current.User.Identity.Name] as User;
        }
    }
}