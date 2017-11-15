using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Teckraft.Core.Domian.Transactions;
using Teckraft.Services;
using Microsoft.Practices.Unity;
using Teckraft.Core.Workflow;
using Teckraft.Core.Domian.Loging;


namespace TeckraftBuilds.Workflow.Activities
{

    public sealed class Update : CodeActivity
    {
        private IService<CR> _service;
        private IService<SLA> slaService;

        public InOutArgument<CR> WorkItem { get; set; }

        // Define an activity input argument of type string
        public InArgument<string> Text { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                _service = CustomUnityContainer.Container.Resolve<IService<CR>>();
                slaService = CustomUnityContainer.Container.Resolve<IService<SLA>>();
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
                        EventType = "Error while adding starting cr workflow"+workitem.Id,
                        ApplicationName = "CR Workflow",
                        Details = msg + "\n" + ex.StackTrace,
                        RCB = workitem.RUB,
                        RCT = DateTime.Now,
                        RUT = DateTime.Now,
                        Url = "wf/update"
                    });
                }
                catch { 
                }
            }
            // Set value to Out arguments (if any).
            

        }
    }
}
