using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian;
using Teckraft.Data;

namespace Teckraft.Services
{
    public interface IReadService<T> where T : BaseEntity
    {
        List<T> ToList { get; }
        ListQueryResult<T> GetByQuery(Teckraft.Data.ListQuery<T> query);
        T GetById(int Id);
       
    }
}
