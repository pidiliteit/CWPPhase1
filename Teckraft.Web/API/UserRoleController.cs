using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Script.Serialization;
using Teckraft.Core.Domian.Settings;
using Teckraft.Data;

namespace Teckraft.Web.API
{
    public class UserRoleController : ApiController
    {
        private readonly Teckraft.Data.Mappings.IMappingProvider<webpages_Roles, Teckraft.Data.Sql.webpages_Roles> mapper;

        public UserRoleController(Teckraft.Data.Mappings.IMappingProvider<webpages_Roles, Teckraft.Data.Sql.webpages_Roles> mapper)
        {
            this.mapper = mapper;
        }


        public ListQueryResult<webpages_Roles> Get(string q)
        {
            ListQuery<webpages_Roles> query = new ListQuery<webpages_Roles>();
            if (!string.IsNullOrEmpty(q))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                query = js.Deserialize<ListQuery<webpages_Roles>>(q);
            }

            var list = new List<webpages_Roles >();
            using (Teckraft.Data.Sql.InitiativeHubFinalEntities dbcontext = new Teckraft.Data.Sql.InitiativeHubFinalEntities())
            {
               if (query.Parameters != null)
               {
                   if (query.Parameters.Count > 0)
                   {
                       foreach (var item in query.Parameters)
                       {
                           if(item.Name=="ExistingRole"){
                              var role=item.Value;
                              var dbquery = dbcontext.webpages_Roles.Where(it => it.RoleId > 0 && it.RoleName!=role);
                              foreach (var dbitem in dbquery.ToList())
                              {
                                    list.Add(mapper.Map(dbitem));
                              }
                           }
                           else{
                                var dbquery = dbcontext.webpages_Roles.Where(it => it.RoleId > 0 );
                                foreach (var dbitem in dbquery.ToList())
                                {
                                    list.Add(mapper.Map(dbitem));
                                }
                           }
                         }
                      }
                       }
                   }

            return new ListQueryResult<webpages_Roles>() { Items = list };
               }

                
            }
        }
 