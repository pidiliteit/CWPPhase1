using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian;

namespace Teckraft.Data
{
    public interface IReadableRepository<T> where T : BaseEntity
    {
        List<T> ToList { get; }
        ListQueryResult<T> GetByQuery(ListQuery<T> query);
        T GetById(int id);


      //  ListQueryResult<Core.Domian.Settings.Reason> GetByQuery(ListQuery<Core.Domian.Settings.Reason> query);

        //ListQueryResult<Core.Domian.SAP.SubModule> GetByModuleMapping(int moduleId);
    }
}
