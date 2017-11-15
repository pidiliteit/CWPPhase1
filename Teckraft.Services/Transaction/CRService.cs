using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.Settings;
using Teckraft.Core.Domian.Transactions;
using Teckraft.Data;

namespace Teckraft.Services.Transaction
{
    public class CRService : IService<CR>
    {
        private readonly IRepository<CR> crRepository;
        private readonly IRepository<User> userRepository;

        public CRService(IRepository<CR> crRepository, IRepository<User>  userRepository)
        {
            this.userRepository = userRepository;
            this.crRepository = crRepository;
        }
        public List<CR> ToList
        {
            get {return this.crRepository.ToList; }
        }

        public Data.ListQueryResult<CR> GetByQuery(Data.ListQuery<CR> query)
        {
            return this.crRepository.GetByQuery(query);
        }

        public CR GetById(int Id)
        {
            return crRepository.GetById(Id);
        }

        public CR Add(CR entity)
        {
            if (entity.ITFHUser == null) {
                entity.ITFHUser = getItFHUser(entity);

            }
            return this.crRepository.Add(entity);
            //throw new NotImplementedException();
        }

        private Core.Domian.Settings.User getItFHUser(CR entity)
        {
            var query = new ListQuery<User>() { PageSize=1, CurrentPage=1 };
            query.Parameters = new List<QueryParameter>();
           // query.Parameters.Add(new QueryParameter() { Name = "isitfh", Value = "true", Operator = CompareOperator.Equals });
            query.Parameters.Add(new QueryParameter() { Name = "crtypeid", Value = entity.CRTypeId.ToString(), Operator = CompareOperator.Equals });
         //   query.Parameters.Add(new QueryParameter() { Name = "submoduleid", Value = entity.SubModule.Id.ToString(), Operator = CompareOperator.Equals });
            return userRepository.GetByQuery(query).Items.FirstOrDefault();
        }

        public CR Update(CR entity)
        {
            return crRepository.Update(entity);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
