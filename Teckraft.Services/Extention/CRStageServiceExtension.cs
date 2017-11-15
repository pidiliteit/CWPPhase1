using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.Transactions;
using Teckraft.Data;

namespace Teckraft.Services.Extention
{
    public static class CRStageServiceExtension
    {
        public static CRStage GetCRStageByCode(this IService<CRStage> service,int crId, string stageCode,int teamId,string remarks, bool createIfNew)
        {
            var query = new ListQuery<CRStage>();
            query.PageSize = 1;
            query.CurrentPage = 1;
            query.AddParameter("StageCode", stageCode);
            query.AddParameter("crId", crId.ToString());
            var list = service.GetByQuery(query).Items;
            if (list != null && list.Count > 0) return list.FirstOrDefault();
            if (createIfNew) return service.Add(new CRStage() { RCB = new Core.Domian.Settings.User() { Id = 1 }, RCT = DateTime.Now, SrNo = 1, RUT = DateTime.Now, Title = stageCode, StageCode = stageCode, CRId = crId, TeamId = teamId, Remarks=remarks });
            return null;
            
        }
    }
}
