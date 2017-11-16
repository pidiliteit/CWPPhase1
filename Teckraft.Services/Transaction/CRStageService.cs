using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.Transactions;
using Teckraft.Data;

namespace Teckraft.Services.Transaction
{
    public class CRStageService:IService<CRStage>
    {
        private readonly IRepository<CRStage> repository;
        public CRStageService(IRepository<CRStage> repository) {
            this.repository = repository;
        }
        public List<CRStage> ToList
        {
            get { return repository.ToList; }
        }

        public Data.ListQueryResult<CRStage> GetByQuery(Data.ListQuery<CRStage> query)
        {
            return repository.GetByQuery(query);
          //  throw new NotImplementedException();
        }

        public CRStage GetById(int Id)
        {
            return repository.GetById(Id);
            //throw new NotImplementedException();
        }

        public CRStage Add(CRStage entity)
        {
            return repository.Add(entity);
           // throw new NotImplementedException();
        }

        public CRStage Update(CRStage entity)
        {
            return repository.Update(entity);

         //   throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
