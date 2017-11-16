using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Teckraft.Core.Domian.Transactions;
using Teckraft.Core.Workflow;
using Teckraft.Data;
using TeckraftBuilds.WFActivities;
using Microsoft.Practices.Unity;
using Teckraft.Services;

namespace CRWFServiceApplication
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CRCustomWFService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CRCustomWFService.svc or CRCustomWFService.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]

    public class CRCustomWFService : ICRCustomWFService
    {

        private readonly IRepository<TaskWorkflow> taskWorkflowRepository;
        //private readonly IService<CR> crService;
        public CRCustomWFService()
        {
            //            userservice = CustomUnityContainer.Container.Resolve<IService<User>>();

            this.taskWorkflowRepository = CustomUnityContainer.Container.Resolve<IRepository<TaskWorkflow>>();
            // crService = CustomUnityContainer.Container.Resolve<IService<CR>>();
        }

        //public Teckraft.Core.Domian.Transactions.CR Update(Teckraft.Core.Domian.Transactions.CR workItem)
        //{
        //    WFContext context = new WFContext();
        //   // Create create = new Create();
        //    //create.WorkItem = new TeckraftBuilds.WFActivities.Argument<Teckraft.Core.Domian.Transactions.CR>();
        //  //  create.WorkItem.Set(workItem);
        //  //  create.Execute(context); //create record
        //  //  workItem = context.GetValue(create.WorkItem);
        //    if(workItem.StatusCode!=0){
        //        foreach (TaskWorkflow wfstep in getTaskWf(workItem))
        //        {
        //            if (wfstep != null)
        //            {

        //                //update status & stage
        //                workItem.StatusCode = wfstep.NewStatusCode.Value;
        //                workItem.ProjectStage = wfstep.NewStageText;
        //                workItem.StatusText = wfstep.NewStatusText;
        //                workItem.CurrentStageText = workItem.ProjectStage;

        //                //add log
        //                string logDetail = wfstep.LogDetail;
        //                string comments = workItem.Comments;
        //                if (workItem.Logs == null) workItem.Logs = new List<CRLog>();
        //                var assignedDate = workItem.RCT;
        //                workItem.Logs.Add(new CRLog()
        //                {
        //                    RCB = workItem.RUB,
        //                    StatusCode = workItem.StatusCode,
        //                    StageName = workItem.CurrentStageText,
        //                    StatusText = workItem.StatusText,
        //                    LogDetail = logDetail,
        //                    RCT = DateTime.Now,
        //                    Comments = comments,
        //                    LogType = "Info",
        //                    AssignedDate = assignedDate
        //                });

        //                //assign task
        //                AssignTaskToUser assigntask = new AssignTaskToUser();
        //                context = new WFContext();
        //                assigntask.WorkItem = new TeckraftBuilds.WFActivities.Argument<CR>();
        //                context.SetValue(assigntask.WorkItem, workItem);

        //                assigntask.StageCode = new TeckraftBuilds.WFActivities.Argument<string>();
        //                context.SetValue(assigntask.StageCode, wfstep.NewStageCode);

        //                assigntask.TaskTypeCode = new TeckraftBuilds.WFActivities.Argument<string>();
        //                context.SetValue(assigntask.TaskTypeCode, wfstep.NewTaskType.TaskTypeCode);

        //                assigntask.AssignedTo = new TeckraftBuilds.WFActivities.Argument<Teckraft.Core.Domian.Settings.User>();
        //                context.SetValue(assigntask.AssignedTo, workItem.PendingWithUser);
        //                assigntask.Task = new TeckraftBuilds.WFActivities.Argument<Task>();
        //                assigntask.Execute(context);
        //                workItem = context.GetValue(assigntask.WorkItem);
        //                //update
        //                Update update = new Update();
        //                update.WorkItem = new TeckraftBuilds.WFActivities.Argument<CR>();
        //                context = new WFContext();
        //                context.SetValue(update.WorkItem, workItem);
        //                update.Execute(context);
        //                workItem = context.GetValue(assigntask.WorkItem);
        //            }
        //        }
        //    }
        //    return workItem;

        //}

        //private List<TaskWorkflow> getTaskWf(Teckraft.Core.Domian.Transactions.CR cR)
        //{
        //    var qry = new ListQuery<TaskWorkflow>();
        //    qry.Parameters = new List<QueryParameter>();
        //    qry.Parameters.Add(new QueryParameter() { Name = "CRTypeId", Value = cR.CRTypeId<=3 ?"1":cR.CRTypeId.ToString() });
        //    qry.Parameters.Add(new QueryParameter() { Name = "TaskTypeId", Value = "0" });
        //    qry.Parameters.Add(new QueryParameter() { Name = "TaskStatusCode", Value = "1" });

        //    var list=taskWorkflowRepository.GetByQuery(qry);
        //    return list.Items;
        //    //if (cR.StatusCode == 1) return new TaskWorkflow()
        //    //{
        //    //    NewTaskType = new TaskType() { TaskTypeCode = "PRJCAT" },
        //    //    NewStageText = "Recording and Approval",
        //    //    NewStatusText = "Pending with ITFH",
        //    //    NewStatusCode = 1,
        //    //    NewStageCode = "ProjectCategorization",
        //    //    LogDetail = "Submitted by Initiator"
        //    //};
        //    //return null;
        //    ////throw new NotImplementedException();
        //}

        //        public Teckraft.Core.Workflow.Task UpdateTask(Teckraft.Core.Workflow.Task workItem)
        //        {
        //            List<TaskWorkflow> wfsteps = getTaskWf(workItem);
        //            WFContext context = new WFContext();
        //            UpdateTask updatetask = new UpdateTask();
        //            updatetask.WorkItem = new TeckraftBuilds.WFActivities.Argument<Task>();
        //            workItem.Completed = wfsteps.FirstOrDefault().IsCompleted;
        //            if (workItem.TaskType.TaskTypeCode == "FAAPPROVAL") {
        //                workItem.CR.FAApprovalFlag = true;
        //                if (workItem.Completed)
        //                {
        //                    workItem.CR.HODApprover = workItem.AssignedTo;
        //                    workItem.CR.ProjectCreated = true;
        //                }
        //            }
        //            else if (workItem.TaskType.TaskTypeCode == "PRJCAT")
        //            {
        //                workItem.CR.ITFHFlag = true;

        //            }
        //            else if (workItem.TaskType.TaskTypeCode == "ITEVAL" || workItem.TaskType.TaskTypeCode == "MDCEVAL" || workItem.TaskType.TaskTypeCode=="GRCEVAL")
        //            {
        //                workItem.CR.ITEvaluatorFlag = true;
        //                workItem.CR.ITEvaluatorApprovedOn = DateTime.Now;
        //                workItem.CR.ITEvaluatorUser = workItem.AssignedTo;

        //            }

        //            else if (workItem.TaskType.TaskTypeCode == "ITAT")
        //            {
        //                workItem.CR.ITATFlag= true;

        //            }
        //            else if (workItem.TaskType.TaskTypeCode == "INFRA")
        //            {
        //                workItem.CR.InfraFlag = true;
        //            }
        //            else if (workItem.TaskType.TaskTypeCode == "SECURITY")
        //            {
        //                workItem.CR.SecurityCheckFlag = true;
        //            }
        //            else if (workItem.TaskType.TaskTypeCode == "PAAPPROVAL")
        //            {
        //                workItem.CR.ProcessApproverFlag = true;
        //            }
        //            //else if (workItem.TaskType.TaskTypeCode == "NONSAPEXE" || workItem.TaskType.TaskTypeCode == "GRCEXE" || workItem.TaskType.TaskTypeCode == "SAPEXE" || workItem.TaskType.TaskTypeCode == "MDCEXE")
        //           // {
        //            //    workItem.CR.ExecutionFlag = true;
        //            //}
        //            else if (workItem.TaskType.TaskTypeCode == "MDCEXE") {
        //                workItem.CR.ExecutionFlag = true;
        //            }
        //            else if (workItem.TaskType.TaskTypeCode == "UAT")
        //            {
        //                workItem.CR.UATFlag = true;
        //            }
        //            else if (workItem.TaskType.TaskTypeCode == "UATSO")
        //            {
        //                workItem.CR.UATSignOffFlag = true;
        //            }
        //            else if (workItem.TaskType.TaskTypeCode == "SDD")
        //            {
        //                workItem.CR.WireframeFlag = true;
        //            }

        ////            else if(workItem.)
        //            updatetask.WorkItem.Set(workItem);
        //            updatetask.Execute(context); //create record
        //            workItem = context.GetValue(updatetask.WorkItem);
        //           // if(workItem.TaskType.


        //            if (workItem.TaskType.TaskTypeCode == "NONSAPEXE") {
        //                 var cr1 = crService.GetById(workItem.CR.Id);
        //            workItem.CR.Phases = cr1.Phases;
        //            wfsteps = getTaskWf(workItem);
        //            }

        //            foreach (var wfstep in wfsteps)
        //            {
        //                if (wfstep != null)
        //                {
        //                    if (wfstep.UpdateStatus.HasValue && wfstep.UpdateStatus.Value)
        //                    {
        //                        //update status & stage
        //                        workItem.CR.StatusCode = wfstep.NewStatusCode.Value;
        //                        workItem.CR.ProjectStage = wfstep.NewStageText;
        //                        workItem.CR.StatusText = wfstep.NewStatusText;
        //                        workItem.CR.CurrentStageText = workItem.CR.ProjectStage;
        //                        string logDetail = wfstep.LogDetail;
        //                        string comments = workItem.Comments;

        //                        if (workItem.CR.Logs == null) workItem.CR.Logs = new List<CRLog>();
        //                        var assignedDate = workItem.RCT;
        //                        workItem.CR.Logs.Add(new CRLog()
        //                        {
        //                            RCB = workItem.CR.RUB,
        //                            StatusCode = workItem.CR.StatusCode,
        //                            StageName = workItem.CR.CurrentStageText,
        //                            StatusText = workItem.CR.StatusText,
        //                            LogDetail = logDetail,
        //                            RCT = DateTime.Now,
        //                            Comments = comments,
        //                            LogType = "Info",
        //                            AssignedDate = assignedDate
        //                        });

        //                    }
        //                    //add log


        //                    //assign task
        //                    if (wfstep.NewTaskType != null)
        //                    {
        //                        if (wfstep.NewTaskType.TaskTypeCode == "NONSAPEXE")
        //                        {
        //                            workItem.CR.StartDate = workItem.CR.Phases.Min(it => it.StartDate);
        //                            workItem.CR.EndDate = workItem.CR.Phases.Max(it => it.EndDate);
        //                            workItem.CR.ITATDate = workItem.CR.Phases.Max(it => it.ITATDate);
        //                            workItem.CR.UATDate = workItem.CR.Phases.Max(it => it.UATDate);

        //                            foreach (var ph in workItem.CR.Phases)
        //                            {
        //                                AddPhase addphase = new AddPhase();
        //                                addphase.Phase = new TeckraftBuilds.WFActivities.Argument<CRPhase>();
        //                                addphase.StageCode = new TeckraftBuilds.WFActivities.Argument<string>();
        //                                addphase.WorkItem = new TeckraftBuilds.WFActivities.Argument<CR>();

        //                                var phcontext = new WFContext();
        //                                phcontext.SetValue(addphase.WorkItem, workItem.CR);
        //                                phcontext.SetValue(addphase.StageCode, wfstep.NewStageCode);
        //                                phcontext.SetValue(addphase.Phase, ph);
        //                                addphase.Execute(phcontext);

        //                                foreach (var tk in ph.Tasks) {
        //                                    tk.CRPhaseId = ph.Id;
        //                                    AssignTaskToUser assigntask = new AssignTaskToUser();
        //                                    context = new WFContext();
        //                                    assigntask.WorkItem = new TeckraftBuilds.WFActivities.Argument<CR>();
        //                                    context.SetValue(assigntask.WorkItem, workItem.CR);

        //                                    assigntask.StageCode = new TeckraftBuilds.WFActivities.Argument<string>();
        //                                    context.SetValue(assigntask.StageCode, wfstep.NewStageCode);

        //                                    assigntask.TaskTypeCode = new TeckraftBuilds.WFActivities.Argument<string>();
        //                                    context.SetValue(assigntask.TaskTypeCode, wfstep.NewTaskType.TaskTypeCode);

        //                                    assigntask.AssignedTo = new TeckraftBuilds.WFActivities.Argument<Teckraft.Core.Domian.Settings.User>();

        //                                    assigntask.Task = new TeckraftBuilds.WFActivities.Argument<Task>();
        //                                    context.SetValue(assigntask.Task, tk);
        //                                    assigntask.Execute(context);
        //                                    workItem.CR = context.GetValue(assigntask.WorkItem);

        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            if (wfstep.NewTaskType.TaskTypeCode == "ITAT" || wfstep.NewTaskType.TaskTypeCode == "ROLLREQ") workItem.CR.ExecutionFlag = true;
        //                            AssignTaskToUser assigntask = new AssignTaskToUser();
        //                            context = new WFContext();
        //                            assigntask.WorkItem = new TeckraftBuilds.WFActivities.Argument<CR>();
        //                            context.SetValue(assigntask.WorkItem, workItem.CR);

        //                            assigntask.StageCode = new TeckraftBuilds.WFActivities.Argument<string>();
        //                            context.SetValue(assigntask.StageCode, wfstep.NewStageCode);

        //                            assigntask.TaskTypeCode = new TeckraftBuilds.WFActivities.Argument<string>();
        //                            context.SetValue(assigntask.TaskTypeCode, wfstep.NewTaskType.TaskTypeCode);

        //                            assigntask.AssignedTo = new TeckraftBuilds.WFActivities.Argument<Teckraft.Core.Domian.Settings.User>();
        //                            if (wfstep.NewTaskType.TaskTypeCode != "INFRA")
        //                                context.SetValue(assigntask.AssignedTo, workItem.CR.PendingWithUser);
        //                            assigntask.Task = new TeckraftBuilds.WFActivities.Argument<Task>();
        //                            assigntask.Execute(context);
        //                            workItem.CR = context.GetValue(assigntask.WorkItem);
        //                        }
        //                    }
        //                    //update
        //                    Update update = new Update();
        //                    update.WorkItem = new TeckraftBuilds.WFActivities.Argument<CR>();
        //                    context = new WFContext();
        //                    context.SetValue(update.WorkItem, workItem.CR);
        //                    update.Execute(context);
        //                    workItem.CR = workItem.CR;
        //                }
        //            }
        //            return workItem;
        //        }

        //private List<TaskWorkflow> getTaskWf(Task workItem)
        //{
        //    var hasOpenTasks = false;
        //    if (workItem.CR.Phases != null && workItem.CR.Phases.Count(it => it.Tasks.Count(t => t.Completed == false) > 0) > 0) hasOpenTasks = true;
        //    var qry = new ListQuery<TaskWorkflow>();
        //    qry.Parameters = new List<QueryParameter>();
        //    qry.Parameters.Add(new QueryParameter() { Name = "CRTypeId", Value = workItem.CR.CRTypeId <= 3 ? "1" : workItem.CR.CRTypeId.ToString() });
        //    qry.Parameters.Add(new QueryParameter() { Name = "TaskTypeId", Value = workItem.TaskType.Id.ToString() });
        //    qry.Parameters.Add(new QueryParameter() { Name = "TaskStatusCode", Value = workItem.StatusCode.ToString() });
        //    qry.Parameters.Add(new QueryParameter() { Name = "TaskSubStatusCode", Value = workItem.SubStatusCode });
        //    qry.Parameters.Add(new QueryParameter() { Name = "InfraCosting", Value = workItem.CR.InfraCosting });
        //    qry.Parameters.Add(new QueryParameter() { Name = "HardwareCostReceived", Value = workItem.CR.HardwareCostReceived.HasValue ? workItem.CR.HardwareCostReceived.Value.ToString() : false.ToString() });
        //    qry.Parameters.Add(new QueryParameter() { Name = "DesignDocRequired", Value = workItem.CR.DesignDocumentRequired.ToString() });
        //    qry.Parameters.Add(new QueryParameter() { Name = "UATRequired", Value = workItem.CR.UatRequired.ToString() });
        //    qry.Parameters.Add(new QueryParameter() { Name = "SAPTransportRequired", Value = workItem.CR.TransportSAPRequired.HasValue ? workItem.CR.TransportSAPRequired.Value.ToString() : false.ToString() });
        //    qry.Parameters.Add(new QueryParameter() { Name = "PIRequired", Value = workItem.CR.PIRequired.ToString() });
        //    qry.Parameters.Add(new QueryParameter() { Name = "HasOpenTask", Value = hasOpenTasks.ToString() });
        //    qry.Parameters.Add(new QueryParameter() { Name = "PARequired", Value = workItem.CR.ProcessApproverFlag.HasValue ? workItem.CR.ProcessApproverFlag.Value.ToString() : "false" });

        //    var list = taskWorkflowRepository.GetByQuery(qry);
        //    return list.Items;
        //    //return new TaskWorkflow()
        //    //{
        //    //    NewTaskType = new TaskType() { TaskTypeCode="ITEVAL" },
        //    //    NewStatusText = "Pending for Evaluation",
        //    //    NewStageCode = "Approval",
        //    //    NewStageText = "Recording and Approval",
        //    //    NewStatusCode = 2,
        //    //    LogDetail = "ITFH Completes categorization", 
        //    //    IsCompleted=true
        //    //};
        //}

        //public Teckraft.Core.Domian.Settings.Requisition Create(Teckraft.Core.Domian.Settings.Requisition workItem)
        //{
        //    WFContext context = new WFContext();
        //    Create create = new Create();
        //    create.WorkItem = new TeckraftBuilds.WFActivities.Argument<Teckraft.Core.Domian.Settings.Requisition>();
        //    create.WorkItem.Set(workItem);
        //    create.Execute(context); //create record
        //    workItem = context.GetValue(create.WorkItem);
        //    // if (workItem.statusCode != null)
        //    //{
        //    // foreach (TaskWorkflow wfstep in getTaskWf(workItem))
        //    //{
        //    // if (wfstep != null)
        //    //{

        //    //update status & stage
        //    //workItem.statusCode = wfstep.NewStatusCode.Value;
        //    //workItem.ProjectStage = wfstep.NewStageText;
        //    //workItem.StatusText = wfstep.NewStatusText;
        //    //workItem.CurrentStageText = workItem.ProjectStage;

        //    //add log
        //    /// string logDetail = wfstep.LogDetail;
        //    //string comments = workItem.Comments;
        //    // if (workItem.Logs == null) workItem.Logs = new List<CRLog>();
        //    //  var assignedDate = workItem.RCT;
        //    //workItem.Logs.Add(new CRLog()
        //    //{
        //    //    RCB = workItem.RUB,
        //    //    StatusCode = workItem.StatusCode,
        //    //    StageName = workItem.CurrentStageText,
        //    //    StatusText = workItem.StatusText,
        //    //    LogDetail = logDetail,
        //    //    RCT = DateTime.Now,
        //    //    Comments = comments,
        //    //    LogType = "Info",
        //    //    AssignedDate = assignedDate
        //    //});

        //    //assign task
        //    //  AssignTaskToUser assigntask = new AssignTaskToUser();
        //    //  context = new WFContext();
        //    //  assigntask.WorkItem = new TeckraftBuilds.WFActivities.Argument<CR>();
        //    //  context.SetValue(assigntask.WorkItem, workItem);

        //    //assigntask.StageCode = new TeckraftBuilds.WFActivities.Argument<string>();
        //    //context.SetValue(assigntask.StageCode, wfstep.NewStageCode);

        //    //assigntask.TaskTypeCode = new TeckraftBuilds.WFActivities.Argument<string>();
        //    //context.SetValue(assigntask.TaskTypeCode, wfstep.NewTaskType.TaskTypeCode);

        //    //assigntask.AssignedTo = new TeckraftBuilds.WFActivities.Argument<Teckraft.Core.Domian.Settings.User>();
        //    //context.SetValue(assigntask.AssignedTo, workItem.PendingWithUser);
        //    //assigntask.Task = new TeckraftBuilds.WFActivities.Argument<Task>();
        //    //assigntask.Execute(context);
        //    //workItem = context.GetValue(assigntask.WorkItem);
        //    ////update
        //    //Update update = new Update();
        //    //update.WorkItem = new TeckraftBuilds.WFActivities.Argument<CR>();
        //    //context = new WFContext();
        //    //context.SetValue(update.WorkItem, workItem);
        //    //update.Execute(context);
        //    //workItem = context.GetValue(assigntask.WorkItem);
        //    //}
        //    //}
        //    //}
        //    return workItem;
        //    //}
        //}
    }
}
