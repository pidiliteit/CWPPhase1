﻿using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teckraft.Core.Workflow;
using Teckraft.Services;
using Microsoft.Practices.Unity;
using Teckraft.Core.Domian.Transactions;
using Teckraft.Services.Workflow;
using Teckraft.Core.Domian.Loging;
namespace TeckraftBuilds.Workflow.Activities
{
   public class UpdateTask:CodeActivity
    {
       public InOutArgument<Task> WorkItem { get; set; }
       public InArgument<string> Text { get; set; }
       private IService<Task> _service;

       protected override void Execute(CodeActivityContext context)
       {
           try
           {
               var _emailservice = CustomUnityContainer.Container.Resolve<IEmailService>();
               var templateSerivce = CustomUnityContainer.Container.Resolve<IService<EmailTemplate>>();

               _service = CustomUnityContainer.Container.Resolve<IService<Task>>();
               // Argument initialization (if any).
               Task workitem = context.GetValue(this.WorkItem);
               //if(workitem.StatusCode==3)
               if (workitem.StatusCode > 1)
               {
                   workitem.Completed = true;
                   workitem.CompletedDate = DateTime.Now;
               }
               // Instantiate business components.
               //if (workitem.Logs == null) workitem.Logs = new List<CRLog>();
               //workitem.Logs.Add(new CRLog() { RCB = new Teckraft.Core.Domian.Settings.User() { Id = 1 }, StatusCode = workitem.StatusCode, LogType = "Info" });
               // Call business component methods.
               // workitem = _service.Update(workitem);

               workitem.CR.PendingWithUser = Teckraft.Services.Workflow.WorkflowHelper.GetAssignedToUser(workitem.CR, workitem);
               workitem.CR.Message = Teckraft.Services.Workflow.WorkflowHelper.GetMessage(workitem.CR, workitem, _emailservice, templateSerivce);
               //    if (workitem.CR.Phases != null) workitem.CR.OpenPhases = workitem.CR.Phases.Count(it => it.Tasks.Count(t => t.Completed == false) > 0);
               if (workitem.RevisedEndDate.HasValue && workitem.CR.CRTypeId == 4) workitem.CR.RevisedEndDate = workitem.RevisedEndDate;
               _service.Update(workitem);
               // Set value to Out arguments (if any).
               if (workitem.TaskType.TaskTypeCode == "GRCEXE" && workitem.StatusCode == 1)
               {
                   workitem.CR.RevisedEndDate = workitem.RevisedEndDate;
               }
               if (workitem.TaskType.TaskTypeCode == "PRJCAT" && (workitem.StatusCode == 6 || workitem.StatusCode == 3))
               {
                   //     _emailservice.SendEmail(templateSerivce.GetTemplate("taskCompeleted", workitem.TaskType.TaskTypeCode), "", workitem.CR.RCB.Email, "", workitem.CR, workitem);    
               }
               //else if()
               context.SetValue(this.WorkItem, workitem);
           }
           catch (System.Exception ex) {
               try
               {
                   Task workitem = context.GetValue(this.WorkItem);
                   var eventlogservice = CustomUnityContainer.Container.Resolve<Teckraft.Services.IService<EventLog>>();
                   var msg = ex.Message;
                   if (ex.InnerException != null)
                   {
                       msg += "\n" + ex.InnerException.Message;
                       if (ex.InnerException.InnerException != null) msg += "\n" + ex.InnerException.InnerException.Message;
                   }

                   eventlogservice.Add(new EventLog()
                   {
                       EventType = "Error while updating task " + workitem.Id,
                       ApplicationName = "CR Workflow",
                       Details = msg + "\n" + ex.StackTrace,
                       RCB = workitem.CR.RUB ,
                       RCT = DateTime.Now,
                       RUT = DateTime.Now,
                       Url = "wf/updatetask"
                   });
               }
               catch
               {
               }
           }


       }
    }
}
