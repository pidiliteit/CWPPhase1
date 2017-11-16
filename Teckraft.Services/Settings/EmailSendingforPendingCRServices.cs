using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.Settings;
using Teckraft.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.Reports;
    
namespace Teckraft.Services.Settings
{
    public class EmailSendingforPendingCRServices : IReadService<EmailSendingforPendingCR>
    {
        private readonly IReadableRepository<EmailSendingforPendingCR> repository;

        public EmailSendingforPendingCRServices(IReadableRepository<EmailSendingforPendingCR> repository)
         {
            this.repository = repository;
        }

        public System.Collections.Generic.List<EmailSendingforPendingCR> ToList
        {
            get { throw new System.NotImplementedException(); }
        }

        public ListQueryResult<EmailSendingforPendingCR> GetByQuery(ListQuery<EmailSendingforPendingCR> query)
        {
            return this.repository.GetByQuery(query);
        }

        public EmailSendingforPendingCR GetById(int Id)
        {
            throw new System.NotImplementedException();
        }
    }
}
