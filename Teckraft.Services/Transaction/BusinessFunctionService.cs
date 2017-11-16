using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Caching;
using Teckraft.Core.Domian.SAP;
using Teckraft.Core.Domian.Settings;
using Teckraft.Core.Domian.Transactions;
using Teckraft.Data;
namespace Teckraft.Services.Transaction
{
    public class BusinessFunctionService : IReadService<BusinessFunction>
    {
        private readonly IReadableRepository<BusinessFunction> _repository;
        public BusinessFunctionService(IReadableRepository<BusinessFunction> repository)
        {
            this._repository = repository;
        }
        [Cache(key: "BusinessFunctionMaster", enable: true)]
        [CacheSource(cacheSourceKey: "BusinessFunctionMaster", enable: true)]
        public List<BusinessFunction> ToList
        {
            get { return _repository.ToList; }
        }

        public Data.ListQueryResult<BusinessFunction> GetByQuery(Data.ListQuery<BusinessFunction> query)
        {

            if (query != null && query.Parameters == null)
            {
                //query.Parameters = new List<QueryParameter>() { new QueryParameter() { Name = null, Value = null, Operator = CompareOperator.Equals } };
                query.CurrentPage = 1;
                query.PageSize = 20;
            }
            return _repository.GetByQuery(query);
        }
        [RemoveCache(keys: new string[] { "BusinessFunctionMaster" })]
        public BusinessFunction Add(BusinessFunction entity)
        {
            throw new NotImplementedException();
        }
        public BusinessFunction Update(BusinessFunction entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }


        public BusinessFunction GetById(int Id)
        {
            return _repository.GetById(Id);

        }
    }
}
