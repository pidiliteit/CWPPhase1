using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.Transactions;
using Teckraft.Data;

namespace Teckraft.Services.Transaction
{
   public class CRPhaseService:IService<CRPhase>
    {
       private readonly IRepository<CRPhase> repository;

       public CRPhaseService(IRepository<CRPhase> repository) {
           this.repository = repository;
       }
        public List<CRPhase> ToList
        {
            get { throw new NotImplementedException(); }
        }

        public Data.ListQueryResult<CRPhase> GetByQuery(Data.ListQuery<CRPhase> query)
        {
            throw new NotImplementedException();
        }

        public CRPhase GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public CRPhase Add(CRPhase entity)
        {
            return repository.Add(entity);
        }

        public CRPhase Update(CRPhase entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
