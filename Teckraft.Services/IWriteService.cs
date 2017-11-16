using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian;
using Teckraft.Data;

namespace Teckraft.Services
{
    public interface IWriteService<T> where T : BaseEntity
    {
       
        T Add(T entity);
        T Update(T entity);
        void Delete(int id);
    }
}
