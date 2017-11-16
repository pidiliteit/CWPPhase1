using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teckraft.Core;
using Teckraft.Core.Domian;

namespace Teckraft.Data.Mappings
{
    public interface IMappingProvider<T,T1>
        where T:BaseEntity
    {
        T Map(T1 item);

        T1 Map(T dbItem);

       // Core.Domian.Settings.Rlam Map(Sql.Reason dbitem);

        //Core.Domian.Transactions.Modules module(Sql.Module item);
    }
}
