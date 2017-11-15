using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Workflow;

namespace Teckraft.Services.Notification
{
    public class TemplateService:IService<EmailTemplate>
    {
        public List<EmailTemplate> ToList
        {
            get { throw new NotImplementedException(); }
        }

        public Data.ListQueryResult<EmailTemplate> GetByQuery(Data.ListQuery<EmailTemplate> query)
        {
            throw new NotImplementedException();
        }

        public EmailTemplate GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public EmailTemplate Add(EmailTemplate entity)
        {
            throw new NotImplementedException();
        }

        public EmailTemplate Update(EmailTemplate entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
