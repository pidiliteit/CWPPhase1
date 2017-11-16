using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian;

namespace Teckraft.Data
{
    public interface IWritableRepository<T> where T : BaseEntity
    {
        T Add(T item);
        T Update(T item);
        void Delete(int id);


      //  ListQueryResult<Core.Domian.Settings.Reason> GetByQuery(ListQuery<Core.Domian.Settings.Reason> query);
    }
}
