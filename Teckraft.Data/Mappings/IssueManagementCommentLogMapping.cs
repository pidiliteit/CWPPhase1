using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.Settings;

namespace Teckraft.Data.Mappings
{
    public class IssueManagementCommentLogMapping : IMappingProvider<Core.Domian.Settings.IssueManagementCommentLog, Sql.IssueManagementCommentLog>
    {
        Mappings.IMappingProvider<User, Sql.UserDetail> _rcbMapping;
        Mappings.IMappingProvider<User, Sql.UserDetail> _rubMapping;
        Mappings.IMappingProvider<StatusMaster, Sql.StatusMaster> _statusMapping;

        public IssueManagementCommentLogMapping(Mappings.IMappingProvider<User, Sql.UserDetail> _rcbMapping, Mappings.IMappingProvider<User, Sql.UserDetail> _rubMapping,
        Mappings.IMappingProvider<StatusMaster, Sql.StatusMaster> _statusMapping)
        {
            this._rcbMapping = _rcbMapping;
            this._rubMapping = _rubMapping;
            this._statusMapping = _statusMapping;
        }
        public Core.Domian.Settings.IssueManagementCommentLog Map(Sql.IssueManagementCommentLog item)
        {
            if (item == null)
                return null;
            return new Core.Domian.Settings.IssueManagementCommentLog()
            {
                Id = item.Id,
                Active=item.Active,
                ID_Comments=item.ID_Comments,
                ID_Request=item.ID_Request,
                ID_Status=item.ID_Status,
                id_System_Name=item.id_System_Name,
                RCT=item.RCT,
                RUT=item.RUT,
                RCB=_rcbMapping.Map(item.UserDetail),
                RUB=_rubMapping.Map(item.UserDetail1),
                ID_Pending_With_Email=item.ID_Pending_With_Email,
                UploadFlag=item.UploadFlag,
                ID_Status_Id=_statusMapping.Map(item.StatusMaster),
                RCBEmail=item.RCBEmail,
                RCBEmpCode=item.RCBEmpCode,
                RCBEmpName=item.RCBEmpName,
                RUBEmail=item.RUBEmail,
                RUBEmpCode=item.RUBEmpCode,
                RUBEmpName=item.RUBEmpName,
                Target_Date=item.Target_Date,
            };
        }

        public Sql.IssueManagementCommentLog Map(Core.Domian.Settings.IssueManagementCommentLog dbItem)
        {
            throw new NotImplementedException();
        }
    }
}
