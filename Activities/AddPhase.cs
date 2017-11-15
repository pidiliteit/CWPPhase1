using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Activities;
using Teckraft.Core.Domian.Transactions;
using Teckraft.Services;
using Microsoft.Practices.Unity;
using Teckraft.Core.Workflow;
using Teckraft.Data;
using Teckraft.Services.Extention;
using Teckraft.Core.Domian.Settings;


namespace TeckraftBuilds.Workflow.Activities
{

    public sealed class AddPhase : CodeActivity
    {
        public InArgument<string> StageCode { get; set; }
        public InArgument<string> Text { get; set; }
        public InArgument<CRPhase> Phase { get; set; }
        public InArgument<CR> WorkItem { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
           // _service = CustomUnityContainer.Container.Resolve<IService<CR>>();
           // _taskService = CustomUnityContainer.Container.Resolve<IService<Task>>();
            var _stageService = CustomUnityContainer.Container.Resolve<IService<CRStage>>();
            var _phaseService = CustomUnityContainer.Container.Resolve<IService<CRPhase>>();
            CR workitem = context.GetValue(this.WorkItem);
             var phase= context.GetValue(this.Phase);
            // var user = context.GetValue(this.AssignedTo);
            //var task = context.GetValue(this.Task);
            var strStageCode = context.GetValue(this.StageCode);
            if (workitem.Logs == null) workitem.Logs = new List<CRLog>();
            CRStage stage = _stageService.GetCRStageByCode(workitem.Id, strStageCode, 1, "", true);
            phase.RCB = workitem.RUB;
            
            phase.Stage = stage;
            //if (task == null)
            //{


            //    //  workitem.Logs.Add(new CRLog() { RCB = new Teckraft.Core.Domian.Settings.User() { Id = 1 }, StatusCode = workitem.StatusCode, LogType = "Info" });
            //    if (user == null) user = new Teckraft.Core.Domian.Settings.User() { Id = 1 };
            //    task = new Teckraft.Core.Workflow.Task() { Subject = "", RCB = user, CR = workitem, Stage = stage, StatusCode = 1, AssignedTo = user, Title = "new task", UniqueId = Guid.NewGuid(), TeamId = 1, TaskType = new TaskType() { TaskTypeCode = context.GetValue(TaskTypeCode) } };
            //}
            //else
            //{
            //    task.TaskType = new TaskType() { TaskTypeCode = context.GetValue(TaskTypeCode) };
            //    task.Stage = stage;
            //    if (task.UniqueId == Guid.Empty) task.UniqueId = Guid.NewGuid();
            //}
            _phaseService.Add(phase);

            //_taskService.Add(task);
            // workitem = _service.GetById(workitem.Id);
            // CR.GetCurrentPhase(workitem).Tasks.Add(new Teckraft.Core.Workflow.Task() {StatusCode=1, AssignedTo = workitem.ITFHUser, Title = "new task", UniqueId = Guid.NewGuid(), TeamId = 1, TaskType = new TaskType() {TaskTypeCode=context.GetValue(TaskTypeCode)} });
            //  workitem= _service.Update(workitem);

            context.SetValue(this.WorkItem, workitem);

        }
    }
}
