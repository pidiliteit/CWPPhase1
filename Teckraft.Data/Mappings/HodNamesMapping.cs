using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.Settings;
using Teckraft.Data.Sql;

namespace Teckraft.Data.Mappings
{
    public class HodNamesMapping : IMappingProvider<Core.Domian.Settings.HodNames, Sql.spHodName_Result>
    {
        public spHodName_Result Map(HodNames dbItem)
        {
            throw new NotImplementedException();
        }

        public HodNames Map(spHodName_Result item)
        {
            if (item == null)
                return null;
            return new Core.Domian.Settings.HodNames()
            {
                SAPDivisionCode=item.SAPDivisionCode,
                SAPDivisionName=item.SAPDivisionName,
                 count=item.count,
            };
        }
    }
}
