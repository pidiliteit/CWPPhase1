using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.Settings;
using Teckraft.Core.Domian.Transactions;
using Teckraft.Data;
using Teckraft.Services;
using Microsoft.Practices.Unity;

namespace TeckraftBuilds.WFActivities
{
    public class Create:IWFActivity
    {
        //private IService<Requisition> _service;
        private IService<User> userservice;

      //  public Argument<Requisition> WorkItem;
       

        public void Execute(WFContext context)
        {
            userservice = CustomUnityContainer.Container.Resolve<IService<User>>();
           // _service = CustomUnityContainer.Container.Resolve<IService<Requisition>>();
            // Argument initialization (if any).
         //   Requisition workitem = context.GetValue(this.WorkItem);
            //workitem.WFInstianceId = context.WorkflowInstanceId;

            // Instantiate business components.

           // if (workitem.ITFHUser == null)
          //  {
                //workitem.ITFHUser = getItFHUser(workitem);

           // }

            // Call business component methods.
           // workitem.PendingWithUser = Teckraft.Services.Workflow.WorkflowHelper.GetAssignedToUser(workitem, null);
         //   if (workitem.Id != 0)
          //  {
               // if (workitem.Logs == null) workitem.Logs = new List<CRLog>();
                //workitem.Logs.Add(new CRLog() { RCB = new Teckraft.Core.Domian.Settings.User() { Id = 1 }, StatusCode = workitem.StatusCode, LogType = "Info" });
              //  _service.Update(workitem);
          //  }
           // else
            //{
              //  if (workitem.Logs == null) workitem.Logs = new List<CRLog>();
                // workitem.Logs.Add(new CRLog() { RCB = new Teckraft.Core.Domian.Settings.User() { Id = 1 }, StatusCode = workitem.StatusCode, LogType = "Info" });
              //  workitem = _service.Add(workitem);
        //    }
            // workitem.Message = Teckraft.Core.Workflow.WorkflowHelper.GetResponseMessage(workitem, workitem.StatusCode);

            // Set value to Out arguments (if any).
           // context.SetValue(this.WorkItem, workitem);
        }
    }
}
