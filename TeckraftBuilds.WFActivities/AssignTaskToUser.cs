using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using Teckraft.Core.Domian.Transactions;
using Teckraft.Core.Workflow;
using Teckraft.Services;
using Microsoft.Practices.Unity;
using Teckraft.Core.Domian.Settings;
using Teckraft.Services.Extention;
using Teckraft.Services.Workflow;
using Teckraft.Core.Domian;
using Teckraft.Core.Domian.Loging;

namespace TeckraftBuilds.WFActivities
{
    public class AssignTaskToUser:IWFActivity
    {
        public Argument<string> TaskTypeCode;
        public Argument<CR> WorkItem;
        public Argument<User> AssignedTo;
        public Argument<Task> Task;
        public Argument<string> StageCode;
        public void Execute(WFContext context)
        {

            try
            {
                var tasktypecode = context.GetValue(this.TaskTypeCode);
                var _userService = CustomUnityContainer.Container.Resolve<IService<User>>();
                var _emailservice = CustomUnityContainer.Container.Resolve<IEmailService>();
                var templateSerivce = CustomUnityContainer.Container.Resolve<IService<EmailTemplate>>();
                var _service = CustomUnityContainer.Container.Resolve<IService<CR>>();
                var _taskService = CustomUnityContainer.Container.Resolve<IService<Task>>();
                var _stageService = CustomUnityContainer.Container.Resolve<IService<CRStage>>();

                var slaService = CustomUnityContainer.Container.Resolve<IService<SLA>>();
                CR workitem = context.GetValue(this.WorkItem);
                var user = context.GetValue(this.AssignedTo);
                var task = context.GetValue(this.Task);
                var strStageCode = context.GetValue(this.StageCode);
                if (workitem.Logs == null) workitem.Logs = new List<CRLog>();
                CRStage stage = _stageService.GetCRStageByCode(workitem.Id, strStageCode, 1, "", true);
                if (task == null)
                {


                    //  workitem.Logs.Add(new CRLog() { RCB = new Teckraft.Core.Domian.Settings.User() { Id = 1 }, StatusCode = workitem.StatusCode, LogType = "Info" });
                    if (user == null)
                    {
                        user = new User() { Id = 1 };
                        if (context.GetValue(TaskTypeCode) == "FAAPPROVAL")
                        {
                            user = Teckraft.Data.DataHelper.GetFAUser(workitem.Division.Id, workitem.BusinessFunction.Id, workitem.ProjectCategory);
                        }
                        else if (context.GetValue(TaskTypeCode) == "ITEVAL")
                        {
                            user = new Teckraft.Core.Domian.Settings.User() { Id = 11 };
                        }
                        else if (context.GetValue(TaskTypeCode) == "INFRA")
                        {
                            user = WorkflowHelper.GetAssignedToUser("INFRA");
                        }
                    }
                    Team team = null;
                    if (tasktypecode == "MDCEVAL")
                    {
                        team = new Team() { TeamCode = "MDCEVALTEAM" };
                    }
                    else if (tasktypecode == "GRCEVAL")
                    {
                        team = new Team() { TeamCode = "GRCTEAM" };
                    }
                    else if (tasktypecode == "GRCEVAL")
                    {
                        team = new Team() { TeamCode = "GRCTEAM" };
                    }
                    else if (tasktypecode == "GRCEXE")
                    {
                        team = new Team() { TeamCode = "GRCTEAM" };
                    }
                    else if (tasktypecode == "MDCEXE")
                    {
                        team = new Team() { TeamCode = "MDCEXETEAM" };
                    }
                    task = new Teckraft.Core.Workflow.Task() { Subject = "", StartDate = DateTime.Now, RCB = user, CR = workitem, Stage = stage, StatusCode = 1, AssignedTo = user, Title = "new task", UniqueId = Guid.NewGuid(), Team = team, TaskType = new TaskType() { TaskTypeCode = context.GetValue(TaskTypeCode) } };
                }
                else
                {
                    task.RCB = workitem.RUB;
                    task.RUB = workitem.RUB.Id;
                    task.TaskType = new TaskType() { TaskTypeCode = context.GetValue(TaskTypeCode) };
                    task.Stage = stage;
                    if (task.UniqueId == Guid.Empty) task.UniqueId = Guid.NewGuid();
                }
                if (workitem.StatusCode == 4)
                {
                    var query = new Teckraft.Data.ListQuery<SLA>();
                    var submodules = workitem.SubModule == null ? "" : workitem.SubModule.Id.ToString();
                    //workitem.MDCSubModules.ForEach(it =>
                    //{
                    //    submodules += it.Id.ToString() + ",";
                    //});
                    if (submodules.Contains(",")) submodules = submodules.Substring(0, submodules.Length - 1);
                    query.AddParameter(new Teckraft.Data.QueryParameter() { Name = "submodules", Value = submodules });
                    var obj1 = slaService.GetByQuery(query);
                    if (obj1.Items != null)
                    {
                        workitem.Sla = obj1.Items.OrderByDescending(it => it.SLA1Days).FirstOrDefault();

                    }

                    if (workitem.Sla != null)
                    {
                        task.TargetDate = DateTime.Now.AddDays(workitem.Sla.SLA1Days);
                        workitem.EndDate = task.TargetDate;
                        task.Level2 = _userService.GetById(11);
                    }
                }


                _taskService.Add(task);
                context.SetValue(this.Task, task);
                if (user == null) user = task.AssignedTo;
                // workitem = _service.GetById(workitem.Id);
                // CR.GetCurrentPhase(workitem).Tasks.Add(new Teckraft.Core.Workflow.Task() {StatusCode=1, AssignedTo = workitem.ITFHUser, Title = "new task", UniqueId = Guid.NewGuid(), TeamId = 1, TaskType = new TaskType() {TaskTypeCode=context.GetValue(TaskTypeCode)} });
                //  workitem= _service.Update(workitem);
                _emailservice.SendEmail(templateSerivce.GetTemplate("TASKASSIGNED", tasktypecode), "", user.Email, "", workitem, task);
                if (task.TaskType.TaskTypeCode == "FAAPPROVAL" && workitem.HODApprover == null) workitem.HODApprover = user;

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
                        EventType = "Error while assigning task for " + workitem.Id,
                        ApplicationName = "CR workflow",
                        Details = msg + "\n" + ex.StackTrace,
                        RCB = workitem.RUB,
                        RCT = DateTime.Now,
                        RUT = DateTime.Now,
                        Url = "wf/assigntask"
                    });
                }
                catch
                {
                }
            }
        }
    }
}
