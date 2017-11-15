using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.Settings;
using Teckraft.Data;

namespace Teckraft.Services.Settings
{
    public   class UserService:IService<User>
    {
        private IRepository<User> repository;
        public UserService(IRepository<User> repository) {
            this.repository = repository;
        }
        public List<User> ToList
        {
            get { return repository.ToList; }
        }

        public Data.ListQueryResult<User> GetByQuery(Data.ListQuery<User> query)
        {
            return repository.GetByQuery(query);
        }

        public User GetById(int Id)
        {
            return repository.GetById(Id);
        }

        public User Add(User entity)
        {
            return repository.Add(entity);
        }

        public User Update(User entity)
        {
            return repository.Update(entity);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
