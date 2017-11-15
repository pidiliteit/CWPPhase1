using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Workflow;
using Teckraft.Data;

namespace Teckraft.Services.Transaction
{
    public class SLAService:IService<SLA>
    {
        private IRepository<SLA> repository;
        public SLAService(IRepository<SLA> repository) {
            this.repository = repository;
        }
        public List<SLA> ToList
        {
            get { throw new NotImplementedException(); }
        }

        public Data.ListQueryResult<SLA> GetByQuery(Data.ListQuery<SLA> query)
        {
            return repository.GetByQuery(query);
        }

        public SLA GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public SLA Add(SLA entity)
        {
            throw new NotImplementedException();
        }

        public SLA Update(SLA entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
