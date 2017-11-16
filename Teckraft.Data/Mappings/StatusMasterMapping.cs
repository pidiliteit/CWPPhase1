using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teckraft.Data.Mappings
{
    public class StatusMasterMapping : IMappingProvider<Core.Domian.Settings.StatusMaster, Sql.StatusMaster>
    {
        public Core.Domian.Settings.StatusMaster Map(Sql.StatusMaster item)
        {
            if (item == null)
                return null;

            return new Core.Domian.Settings.StatusMaster()
            {
                Id=item.Id,
                StatusDesc = item.StatusDesc,
            };
        }

        public Sql.StatusMaster Map(Core.Domian.Settings.StatusMaster dbItem)
        {
            throw new NotImplementedException();
        }
    }
}
