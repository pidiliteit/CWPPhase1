using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian;
using Teckraft.Data;

namespace Teckraft.Services
{
    public interface IService<T>:IReadService<T>, IWriteService<T> where T : BaseEntity
    {

       // void SendRemidners(IService<Core.Domian.Settings.MilestoneReport> reminderService, IService<Core.Workflow.EmailTemplate> templateService, IEmailService emailService);
    }
}
