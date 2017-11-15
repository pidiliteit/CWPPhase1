using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teckraft.Data.Mappings
{
    public class DivisionMapping
    {
        public Core.Domian.SAP.Division Map(Sql.Division division)
        {
            return new Core.Domian.SAP.Division() { Id = division.Id, DivisionCode = division.DivisionCode, DivisionName = division.DivisionName };
        }
    }
    public class DivisionMappingProvider : IMappingProvider<Core.Domian.SAP.Division, Sql.Division>
    {
        public Core.Domian.SAP.Division Map(Sql.Division item)
        {
            if (item == null) return null;
            return new Core.Domian.SAP.Division() { DivisionCode = item.DivisionCode, DivisionName = item.DivisionName, Id = item.Id };
        }

        public Sql.Division Map(Core.Domian.SAP.Division dbItem)
        {
            throw new NotImplementedException();
        }
    }
}
