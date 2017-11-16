using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.Loging;
using Teckraft.Data;

namespace Teckraft.Services.Loging
{
    public class EventLogService:IService<EventLog>
    {
        private IRepository<EventLog> repository;
        public EventLogService(IRepository<EventLog> repository)

        {
            this.repository = repository;
        
        }

        public List<EventLog> ToList
        {
            get { throw new NotImplementedException(); }
        }

        public Data.ListQueryResult<EventLog> GetByQuery(Data.ListQuery<EventLog> query)
        {
            throw new NotImplementedException();
        }

        public EventLog GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public EventLog Add(EventLog entity)
        {
            return repository.Add(entity);
        }

        public EventLog Update(EventLog entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
