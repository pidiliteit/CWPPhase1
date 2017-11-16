using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.Settings;
using Teckraft.Data.Sql;

namespace Teckraft.Data.Mappings
{
    public class HODWiseCountMapping : IMappingProvider<Core.Domian.Settings.HODWiseCount, Sql.spHodCount_Result>
    {
        public spHodCount_Result Map(HODWiseCount dbItem)
        {
            throw new NotImplementedException();
        }

        public HODWiseCount Map(spHodCount_Result item)
        {
            if (item == null)
                return null;
            return new Core.Domian.Settings.HODWiseCount()
            {
                
                SAPDivisionName = item.SAPDivisionName,
                count = item.count,
            };
        }
    }
}
