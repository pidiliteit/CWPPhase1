using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teckraft.Data.Mappings
{
    public class ChapterMasterMapping : IMappingProvider<Core.Domian.Settings.ChapterMaster, Sql.ChapterMaster>
    {
        public Core.Domian.Settings.ChapterMaster Map(Sql.ChapterMaster item)
        {
            if (item == null)
                return null;
            return new Core.Domian.Settings.ChapterMaster()
            {
                Id = item.Id,
                ChapterNameDisplay=item.ChapterNameDisplay,
                ChapterNameFromSystem=item.ChapterNameFromSystem,
                EmailFlag=item.EmailFlag,
                EmailTo=item.EmailTo,
            };
        }

        public Sql.ChapterMaster Map(Core.Domian.Settings.ChapterMaster dbItem)
        {
            throw new NotImplementedException();
        }
    }
}
