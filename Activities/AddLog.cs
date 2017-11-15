using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Teckraft.Core.Domian.Transactions;
using Teckraft.Services;
using Microsoft.Practices.Unity;
using Teckraft.Core.Workflow;
using Teckraft.Services.Extention;

namespace TeckraftBuilds.Workflow.Activities
{
    public class AddLog : CodeActivity
    {
        private IService<CR> _service;

        public InOutArgument<CR> WorkItem { get; set; }
        public InArgument<string> Text { get; set; }
        public InArgument<string> NotificationType { get; set; }
        public InArgument<string> Comments { get; set; }

        public InArgument<DateTime?> AssignedDate { get; set; }
        public InArgument<string> LogDetail { get; set; }
        public InArgument<string> TaskTypeCode { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            var templateSerivce = CustomUnityContainer.Container.Resolve<IService<EmailTemplate>>();
            var tasktypecode = context.GetValue(this.TaskTypeCode);
            var _emailservice = CustomUnityContainer.Container.Resolve<IEmailService>();
            //_service = CustomUnityContainer.Container.Resolve<IService<CR>>();
            CR workitem = context.GetValue(this.WorkItem);
            var logDetail= context.GetValue(this.LogDetail);
            var comments = context.GetValue(this.Comments);
            if (workitem.Logs == null) workitem.Logs = new List<CRLog>();
            var assignedDate = context.GetValue(this.AssignedDate);
            workitem.Logs.Add(new CRLog()
            {
                RCB = workitem.RUB,
                StatusCode = workitem.StatusCode,
                StageName = workitem.CurrentStageText,
                StatusText = workitem.StatusText,
                LogDetail= logDetail,
                RCT=DateTime.Now,
                Comments=comments,
                LogType = "Info",
                AssignedDate=assignedDate
            });
            //var notificationType = context.GetValue(this.NotificationType);

            //if (string.IsNullOrEmpty(notificationType) == false)
            //{
            //    _emailservice.SendEmail(templateSerivce.GetTemplate(notificationType, tasktypecode), workitem.RCB.Email, workitem.PendingWithUser.Email, "", workitem, null);
            //}


           // _taskService.Add(new Teckraft.Core.Workflow.Task() { Subject = "", RCB = new Teckraft.Core.Domian.Settings.User() { Id = 1 }, CR = workitem, Stage = stage, StatusCode = 1, AssignedTo = workitem.ITFHUser, Title = "new task", UniqueId = Guid.NewGuid(), TeamId = 1, TaskType = new TaskType() { TaskTypeCode = context.GetValue(TaskTypeCode) } });
            // workitem = _service.GetById(workitem.Id);
            // CR.GetCurrentPhase(workitem).Tasks.Add(new Teckraft.Core.Workflow.Task() {StatusCode=1, AssignedTo = workitem.ITFHUser, Title = "new task", UniqueId = Guid.NewGuid(), TeamId = 1, TaskType = new TaskType() {TaskTypeCode=context.GetValue(TaskTypeCode)} });
            //  workitem= _service.Update(workitem);

            context.SetValue(this.WorkItem, workitem);

        }
    }
}
