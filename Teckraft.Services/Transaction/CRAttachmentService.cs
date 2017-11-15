using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.Transactions;
using Teckraft.Data;

namespace Teckraft.Services.Transaction
{
    public class CRAttachmentService:IReadService<Attachment>
    {
        private IReadableRepository<Attachment> repository;

        public CRAttachmentService(IReadableRepository<Attachment> repository) {
            this.repository = repository;
        }

        public List<Attachment> ToList
        {
            get { throw new NotImplementedException(); }
        }

        public Data.ListQueryResult<Attachment> GetByQuery(Data.ListQuery<Attachment> query)
        {
            return repository.GetByQuery(query);
        }

        public Attachment GetById(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
