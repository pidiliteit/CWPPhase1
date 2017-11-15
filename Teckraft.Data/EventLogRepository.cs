using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.Loging;

namespace Teckraft.Data
{
    public class EventLogRepository:IRepository<EventLog>
    {
        public List<EventLog> ToList
        {
            get { throw new NotImplementedException(); }
        }

        public ListQueryResult<EventLog> GetByQuery(ListQuery<EventLog> query)
        {
            throw new NotImplementedException();
        }

        public EventLog GetById(int id)
        {
            throw new NotImplementedException();
        }

        public EventLog Add(EventLog item)
        {
            using (Sql.InitiativeHubFinalEntities context=new Sql.InitiativeHubFinalEntities()) {
                var dbitem = new Sql.EventLog() { 
                EventType=item.EventType, Details=item.Details, ApplicationName=item.ApplicationName, IP=item.IP, RCB=item.RCB.Id, RCT=item.RCT, Url=item.Url
                };

               context.EventLogs.Add(dbitem);
                context.SaveChanges();
                item.Id = dbitem.ID;
            };
            return item;
        }

        public EventLog Update(EventLog item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
