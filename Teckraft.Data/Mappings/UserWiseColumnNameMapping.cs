using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.Settings;
using Teckraft.Data.Sql;

namespace Teckraft.Data.Mappings
{
    public class UserWiseColumnNameMapping : IMappingProvider<Core.Domian.Settings.UserWiseColumnName, Sql.UserWiseColumnName>
    {
        public Sql.UserWiseColumnName Map(Core.Domian.Settings.UserWiseColumnName dbItem)
        {
            throw new NotImplementedException();
        }

        public Core.Domian.Settings.UserWiseColumnName Map(Sql.UserWiseColumnName item)
        {
            if (item == null)
                return null;
            return new Core.Domian.Settings.UserWiseColumnName()
            {
                Id = item.Id,
                ChapterId=item.ChapterId,
                ID_Issue_Detail1=item.ID_Issue_Detail1,
                
            chapternamedisplay=item.chapternamedisplay,
         id_data_load_date =item.id_data_load_date,
          title =item.title,
          chapterOwner  =item.chapterOwner,
         id_request  =item.id_request,
        id_status =item.id_status,
         id_reported_by_name  =item.id_reported_by_name,
          id_reported_by_email  =item.id_reported_by_email,
         id_dept =item.id_dept,
         id_location =item.id_location,
         id_pending_with_name =item.id_pending_with_name,
         id_pending_with_email =item.id_pending_with_email,                
        ID_Logged_Date  =item.ID_Logged_Date,
         pendingSince  =item.pendingSince,
          id_tat_status =item.id_tat_status,
         id_comments =item.id_comments,
          ID_Target_Date  =item.ID_Target_Date,
         ID_Category =item.ID_Category,
          UserId  =item.UserId,
         RCB  =item.RCB,
         RCT  =item.RCT,
          RUT =item.RUT,
          RUB  =item.RUB,
    };
        }
    }
}
