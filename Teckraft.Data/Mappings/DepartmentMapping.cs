using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teckraft.Data.Mappings
{
    public class DepartmentMappingProvider : IMappingProvider<Core.Domian.Settings.Department, Sql.Division>
    {
        public Core.Domian.Settings.Department Map(Sql.Division item)
        {
            if (item == null)
                return null;
            return new Core.Domian.Settings.Department() { 
                Id = item.Id , 
                DivisionCode=item.DivisionCode,
                DivisionName=item.DivisionName
            };
        }

        public Sql.Division Map(Core.Domian.Settings.Department dbItem)
        {
            throw new NotImplementedException();
        }
    }
}
