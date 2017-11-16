using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teckraft.Core.Domian.Settings;
using Teckraft.Core.Domian.Transactions;
using Teckraft.Core.Workflow;
using Teckraft.Data;

namespace Teckraft.Services.Transaction
{
    public class PhaseTaskService : IService<Task>
    {
        private readonly IRepository<Task> TaskRepository;
        private readonly IRepository<User> userRepository;

        public PhaseTaskService(IRepository<Task> TaskRepository, IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
            this.TaskRepository = TaskRepository;
        }
        
        public List<Task> ToList
        {
            get { throw new NotImplementedException(); }
        }

        public Data.ListQueryResult<Task> GetByQuery(Data.ListQuery<Task> query)
        {
            return this.TaskRepository.GetByQuery(query);
        }

        public Task GetById(int Id)
        {
            return TaskRepository.GetById(Id);
        }

        public Task Add(Task entity)
        {
            if (entity.RCB == null)
            {
                entity.NoOfDays = 10;

            }
            return this.TaskRepository.Add(entity);
            //throw new NotImplementedException();
        }

        public Task Update(Task entity)
        {
            return TaskRepository.Update(entity);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
