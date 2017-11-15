using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Teckraft.Services;
using Teckraft.Core.Domian.Transactions;
using Teckraft.Core.Workflow;
using Teckraft.Core.Domian.Loging;

namespace TeckraftBuilds.WFActivities
{
    public class Update:IWFActivity
    {
        public Argument<CR> WorkItem { get; set; }
        public void Execute(WFContext context)
        {
            try
            {
               var  _service = CustomUnityContainer.Container.Resolve<IService<CR>>();
                var slaService = CustomUnityContainer.Container.Resolve<IService<SLA>>();
                // Argument initialization (if any).
                CR workitem = context.GetValue(this.WorkItem);
                // Instantiate business components.
                if (workitem.Logs == null) workitem.Logs = new List<CRLog>();
                // workitem.Logs.Add(new CRLog() { RCB = new Teckraft.Core.Domian.Settings.User() { Id=1} , StatusCode=workitem.StatusCode, LogType="Info"});
                // Call business component methods.
                //workitem.PendingWithUser = Teckraft.Services.Workflow.WorkflowHelper.GetAssignedToUser(workitem, null);
                workitem = _service.Update(workitem);
                // if (workitem.Phases != null) workitem.OpenPhases = workitem.Phases.Count(it => it.Tasks.Count(t => t.Completed == false) > 0);
                context.SetValue(this.WorkItem, workitem);
            }
            catch (System.Exception ex)
            {
                try
                {
                    CR workitem = context.GetValue(this.WorkItem);
                    var eventlogservice = CustomUnityContainer.Container.Resolve<Teckraft.Services.IService<EventLog>>();
                    var msg = ex.Message;
                    if (ex.InnerException != null)
                    {
                        msg += "\n" + ex.InnerException.Message;
                        if (ex.InnerException.InnerException != null) msg += "\n" + ex.InnerException.InnerException.Message;
                    }

                    eventlogservice.Add(new EventLog()
                    {
                        EventType = "Error while adding starting cr workflow" + workitem.Id,
                        ApplicationName = "CR Workflow",
                        Details = msg + "\n" + ex.StackTrace,
                        RCB = workitem.RUB,
                        RCT = DateTime.Now,
                        RUT = DateTime.Now,
                        Url = "wf/update"
                    });
                }
                catch
                {
                }
            }
        }
    }
}
