using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Teckraft.Core.Domian.Settings;
using Teckraft.Data;

namespace Teckraft.Web.API
{
    public class StatusMasterController:ApiController
    {
        private readonly Teckraft.Data.Mappings.IMappingProvider<Teckraft.Core.Domian.Settings.StatusMaster, Teckraft.Data.Sql.StatusMaster> _StatusMapper;
        public StatusMasterController(Teckraft.Data.Mappings.IMappingProvider<Teckraft.Core.Domian.Settings.StatusMaster, Teckraft.Data.Sql.StatusMaster> _StatusMapper)
        {
            this._StatusMapper = _StatusMapper;
        }

        public ListQueryResult<StatusMaster> Get(string q)
        {
            var result = new ListQueryResult<Teckraft.Core.Domian.Settings.StatusMaster>();
            using (Teckraft.Data.Sql.InitiativeHubFinalEntities dbcontext = new Teckraft.Data.Sql.InitiativeHubFinalEntities())
            {
                var dbquery = dbcontext.StatusMasters.Where(it => it.Id > 0);
                foreach (var item in dbquery.OrderByDescending(it => it.StatusDesc))
                {
                    if (result.Items == null) result.Items = new List<Teckraft.Core.Domian.Settings.StatusMaster>();
                    result.Items.Add(_StatusMapper.Map(item));
                }
            }

            return result;
        }

    }
}