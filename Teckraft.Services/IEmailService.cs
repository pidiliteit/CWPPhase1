using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Teckraft.Core.Domian.Transactions;
using Teckraft.Core.Workflow;

namespace Teckraft.Services
{
   public interface IEmailService
    {
        //void SendEmail(Teckraft.Core.Workflow.EmailTemplate EmailTemplate, string FromEmail, String ToEmail, string CC, CR cr, Task task);
       void SendEmail(Teckraft.Core.Workflow.EmailTemplate EmailTemplate, string FromEmail, String ToEmail, string CC, String body);
    }
}
