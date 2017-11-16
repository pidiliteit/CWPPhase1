using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.Settings;
using Teckraft.Data.Sql;

namespace Teckraft.Data.Mappings
{
    public class IssueManagementAnchorCommentMapping : IMappingProvider<Core.Domian.Settings.IssueManagementAnchorComment, Sql.IssueManagementAnchorComment>
    {
        Mappings.IMappingProvider<User, Sql.UserDetail> _rcbMapping;
        Mappings.IMappingProvider<User, Sql.UserDetail> _rubMapping;
       

        public IssueManagementAnchorCommentMapping(Mappings.IMappingProvider<User, Sql.UserDetail> _rcbMapping, Mappings.IMappingProvider<User, Sql.UserDetail> _rubMapping)
        {
            this._rcbMapping = _rcbMapping;
            this._rubMapping = _rubMapping;
        }

        public Sql.IssueManagementAnchorComment Map(Core.Domian.Settings.IssueManagementAnchorComment dbItem)
        {
            throw new NotImplementedException();
        }

        public Core.Domian.Settings.IssueManagementAnchorComment Map(Sql.IssueManagementAnchorComment item)
        {
            if (item == null)
                return null;
            return new Core.Domian.Settings.IssueManagementAnchorComment()
            {
                Id = item.Id,
                Active = item.Active,
                ID_Comments = item.ID_Comments,
                ID_Request = item.ID_Request,
                id_System_Name = item.id_System_Name,
                RCT = item.RCT,
                RUT = item.RUT,
                RCB = _rcbMapping.Map(item.UserDetail),
                RUB = _rubMapping.Map(item.UserDetail1),                
                ID_Pending_With_Email=item.ID_Pending_With_Email                
            };
        }
    }
}
