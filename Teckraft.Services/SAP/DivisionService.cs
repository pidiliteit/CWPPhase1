using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Caching;
using Teckraft.Core.Domian.SAP;
using Teckraft.Data;

namespace Teckraft.Services.SAP
{
    public class DivisionService : IReadService<Division>
    {
        private readonly IReadableRepository<Division> _repository;
        public DivisionService(IReadableRepository<Division> repository)
        {
            this._repository = repository;
        }
        [Cache(key: "DivisionMaster", enable: true)]
        [CacheSource(cacheSourceKey: "DivisionMaster", enable: true)]
        public List<Division> ToList
        {
            get { return _repository.ToList; }
        }

        public Data.ListQueryResult<Division> GetByQuery(Data.ListQuery<Division> query)
        {

            if (query != null && query.Parameters == null)
            {
                query.Parameters = new List<QueryParameter>() { new QueryParameter() { Name = "Active", Value = "true", Operator = CompareOperator.Equals } };
            }
            return _repository.GetByQuery(query);
        }
        [RemoveCache(keys: new string[] { "DivisionMaster" })]
        public Division Add(Division entity)
        {
            throw new NotImplementedException();
        }
        public Division Update(Division entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }


        public Division GetById(int Id)
        {
            return _repository.GetById(Id);

        }
    }
}
