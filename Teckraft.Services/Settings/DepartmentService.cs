using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.Settings;
using Teckraft.Data;

namespace Teckraft.Services.Settings
{
    public class DepartmentService : IReadService<Department>
    {
        private readonly IReadableRepository<Department> _repository;
        public DepartmentService(IReadableRepository<Department> repository)
        {
            this._repository = repository;
        }

        public List<Department> ToList
        {
            get { throw new NotImplementedException(); }
        }

        public ListQueryResult<Department> GetByQuery(ListQuery<Department> query)
        {
            if (query != null && query.Parameters == null)
            {
                query.CurrentPage = 1;
                query.PageSize = 20;
            }
            return _repository.GetByQuery(query);
        }

        public Department GetById(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
