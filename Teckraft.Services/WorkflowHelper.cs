using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using Teckraft.Core.Domian.Settings;
using Teckraft.Core.Domian.Transactions;
using Teckraft.Core.Workflow;
using Teckraft.Data;
using Teckraft.Services.Settings;
namespace Teckraft.Services.Workflow
{
    public class WorkflowHelper
    {
//        public static User GetAssignedToUser(CR cr, Task task)
//        {
//            var tasktype = task == null ? "" : task.TaskType.TaskTypeCode;
//            if (cr.CRTypeId <= 3)
//            {

//                if (cr.StatusCode == 0 && task == null) return cr.ITFHUser;
//                else if (cr.StatusCode == 1 && task == null) return cr.ITFHUser;
//                else if (tasktype == "INFRA" && task.StatusCode == 2 && (cr.InfraCosting == "Y")) return cr.ITFHUser;

//                else if (tasktype == "PRJCAT" && task.StatusCode == 2 && (cr.InfraCosting != "Y" || cr.HardwareCostReceived.HasValue))
//                {
//                    var totalcost = 0;
//                    if (cr.VendorDetails != null && cr.VendorDetails.Count(it => it.TotalCost.HasValue) > 0) totalcost = cr.VendorDetails.Where(it => it.TotalCost.HasValue).Max(it => it.TotalCost.Value);
//                    return Teckraft.Data.DataHelper.GetITEvaluator(cr.ProjectCategory, totalcost); //_workItem.CRTypeId != 5 && _workItem.CRTypeId != 4 && StatusCode == 2 && (_workItem.InfraCosting != "Y" || _workItem.HardwareCostReceived.HasValue)
//                }
//                else if (tasktype == "PRJCAT" && task.StatusCode == 6 && (cr.InfraCosting == "Y" && cr.HardwareCostReceived.HasValue == false)) return Teckraft.Data.DataHelper.GetInfraUser(); //infra

//                else if (tasktype == "PRJCAT" && task.StatusCode == 5) return cr.ITFHUser;
//                else if (tasktype == "PRJCAT" && task.StatusCode == 3) return cr.RCB;

//                else if (tasktype == "ITEVAL" && task.StatusCode == 2)
//                    return Teckraft.Data.DataHelper.GetFAUser(cr.Division.Id, cr.BusinessFunction.Id, cr.ProjectCategory);
//                else if (tasktype == "ITEVAL" && task.StatusCode == 3) return cr.ITFHUser;
//                else if (tasktype == "ITEVAL" && task.StatusCode == 4) return null;

//                else if (tasktype == "FAAPPROVAL" && task.StatusCode == 2) return cr.ITFHUser;
//                else if (tasktype == "FAAPPROVAL" && task.StatusCode == 4) return null;
//                else if (tasktype == "FAAPPROVAL" && task.StatusCode == 3) return cr.RCB;

//                else if (tasktype == "SDD" && task.StatusCode == 2) return cr.ITFHUser;
//                else if (tasktype == "ASSIGNMENT" && task.StatusCode == 2) return cr.ITFHUser;

//                else if (tasktype == "NONSAPEXE" && task.StatusCode == 2) return cr.ITFHUser;
//                else if (tasktype == "ITAT" && task.StatusCode == 2) return cr.RCB;
//                else if (tasktype == "UAT" && task.StatusCode == 2) return cr.ITFHUser;
//                else if (tasktype == "UAT" && task.StatusCode == 3) return cr.ITFHUser;
//                else if (tasktype == "UAT") return cr.RCB;
//                else if (tasktype == "UATSO" && task.StatusCode == 2 && (cr.SecurityCheckRequired.HasValue == false || cr.SecurityCheckRequired.Value == false) && cr.InfraCosting == "L" && (cr.HardwareCostReceived.HasValue == false || cr.HardwareCostReceived.Value == false)) return DataHelper.GetInfraUser();
//                else if (tasktype == "UATSO" && task.StatusCode == 2 && cr.SecurityCheckRequired.HasValue && cr.SecurityCheckRequired.Value) return cr.ITFHUser; //return DataHelper.GetInfraUser();
//                else if (tasktype == "UATSO" && task.StatusCode == 2) return cr.ITFHUser;
//                else if (tasktype == "UATSO" && task.StatusCode == 3) return null;

//                else if (tasktype == "SECURITY" && task.StatusCode == 2 && (cr.InfraCosting == "L" && (cr.HardwareCostReceived.HasValue == false || cr.HardwareCostReceived.Value == false))) return Teckraft.Data.DataHelper.GetInfraUser(); //return cr.ITFHUser;
//                else if (tasktype == "SECURITY" && task.StatusCode == 2) return cr.ITFHUser;

//                else if (tasktype == "INFRA" && task.StatusCode == 2) return cr.ITFHUser;

//                else if (tasktype == "ROLLREQ" && task.StatusCode == 2 && cr.TransportSAPRequired.HasValue && cr.TransportSAPRequired.Value == true) return DataHelper.GetITEvaluator(cr.ProjectCategory, 0);
//                else if (tasktype == "ROLLAPP" && task.StatusCode == 4) return cr.ITFHUser;
//                else if (tasktype == "ROLLAPP" && task.StatusCode == 2) return cr.ITFHUser;
//                else if (tasktype == "ROLLAPP" && task.StatusCode == 3) return null;

//                else if (tasktype == "ROLLCONF" && task.StatusCode == 2) return null;

//            }
//            else if (cr.CRTypeId == 4)
//            {
//                if (cr.StatusCode == 1 && task == null) return cr.ITFHUser;
//                else if (cr.StatusCode == 1 && task.StatusCode == 2) return Teckraft.Data.DataHelper.GetFAUser(cr.Division.Id, cr.BusinessFunction.Id, cr.ProjectCategory);
//                else if (cr.StatusCode == 1 && task.StatusCode == 3) return cr.RCB;
//                else if (tasktype == "FAAPPROVAL" && task.StatusCode == 2 && cr.ProcessApproverFlag.HasValue && cr.ProcessApproverFlag.Value) return cr.ProcessApprover;
//                else if (tasktype == "FAAPPROVAL" && task.StatusCode == 3) return cr.RCB;
//                else if (tasktype == "FAAPPROVAL" && task.StatusCode == 2) return cr.ITFHUser;
//                else if (tasktype == "MDCEXE" && task.StatusCode == 3) return cr.RCB;
//                else if (tasktype == "MDCCHANGE" && task.StatusCode == 2) return cr.ITFHUser;
//                else if (tasktype == "PAAPPROVAL" && task.StatusCode == 2) return cr.ITFHUser;
//                else if (tasktype == "PAAPPROVAL" && task.StatusCode == 5) return null;

//                else if (tasktype == "MDCEVAL" && task.StatusCode == 5) return null;


//                else return null;

//            }
//            else if (cr.CRTypeId == 5)
//            {
//                if (cr.StatusCode == 1 && task == null) return cr.ITFHUser;
//                else if (tasktype == "GRCEVAL" && task.StatusCode == 1) return cr.ITFHUser; //new Teckraft.Core.Domian.Settings.User() { Id = Teckraft.Data.DataHelper.getFA(cr.Division.Id, cr.BusinessFunction.Id, cr.ProjectCategory) };
//                else if (tasktype == "GRCEVAL" && task.StatusCode == 3) return cr.RCB;
//                else if (tasktype == "GRCEVAL" && task.StatusCode == 2) return cr.HODApprover;
//                else if (tasktype == "GRCEVAL" && task.StatusCode == 4) return null;

//                else if (tasktype == "SAPHODAPP" && task.StatusCode == 3) return cr.RCB;
//                else if (tasktype == "SAPHODAPP" && task.StatusCode == 2) return cr.ITFHUser;
//                else if (tasktype == "SAPHODAPP" && task.StatusCode == 4) return null;

//                else if (tasktype == "GRCEXE" && task.StatusCode == 3) return cr.ITFHUser;
//                else if (tasktype == "GRCEXE" && task.StatusCode == 2) return null;
//                else return null;
//            }

//            return cr.ITFHUser;
//        }


//        public static string GetMessage(CR cr, Task task, IEmailService emailService, IService<EmailTemplate> templateService)
//        {
//            var tasktype = task == null ? "" : task.TaskType.TaskTypeCode;



//            //var ReqNo = cr.ChangeRequestNo.ToString();
//            // return string.Format(ReqNo, cr.RCB.Title);

//            if (cr.CRTypeId <= 3)
//            {
//                //if(task==null && cr.StatusCode==0) 
//                if (task.StatusCode == 2 && task.TaskType.TaskTypeCode == "INFRA" && (cr.InfraCosting == "Y"))
//                {
//                    var tmplt = templateService.GetTemplate("INFRACOSTRECEIVED", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.ITFHUser.Email, "", cr, task);
//                    return string.Format("Costing recived for CR " + cr.ChangeRequestCode.ToString() + " from INFRA; mail sent to ITFH {0}", cr.PendingWithUser.Title);
//                }
//                if (task.StatusCode == 3 && task.TaskType.TaskTypeCode == "INFRA")
//                {
//                    var tmplt = templateService.GetTemplate("CRSENTBACK", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.ITFHUser.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " Sent back by Infra; mail sent to ITFH - {0}", cr.PendingWithUser.Title);
//                }

//                else if (tasktype == "PRJCAT" && task.StatusCode == 1 && cr.RevisedMeetingDate.HasValue)
//                {
//                    var tmplt = templateService.GetTemplate("MEETINGDATEREVISED", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, "", cr, task);

//                    return string.Format("Meeting date revised for CR " + cr.ChangeRequestCode.ToString() + " by ITFH; mail sent to Initiator {0}", cr.RCB.Title);
//                }
//                else if (tasktype == "PRJCAT" && task.StatusCode == 2 && (cr.InfraCosting == "L" && cr.HardwareCostReceived.HasValue == false))
//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + "  updated successfully mail sent to IT-Evaluator - {0} and Infra team for hardware costing - {1}", cr.PendingWithUser.Title, GetAssignedToUser("INFRA").Title);
//                else if (tasktype == "PRJCAT" && task.StatusCode == 2 && (cr.InfraCosting != "Y" || cr.HardwareCostReceived.HasValue))
//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + "  updated successfully mail sent to IT-Evaluator - {0}", cr.PendingWithUser.Title);
//                //Teckraft.Data.DataHelper.GetITEvaluator(); //_workItem.CRTypeId != 5 && _workItem.CRTypeId != 4 && StatusCode == 2 && (_workItem.InfraCosting != "Y" || _workItem.HardwareCostReceived.HasValue)
//                else if (tasktype == "PRJCAT" && task.StatusCode == 6 && (cr.InfraCosting == "Y" && cr.HardwareCostReceived.HasValue == false)) return string.Format("CR " + cr.ChangeRequestCode.ToString() + " sent to INFRA-{0}", cr.PendingWithUser.Title); //return Teckraft.Data.DataHelper.GetInfraUser(); //infra
//                else if (tasktype == "PRJCAT" && task.StatusCode == 3)
//                {
//                    var tmplt = templateService.GetTemplate("CRSENTBACK", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, "", cr, task);
//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " sent back to Initiator mail sent to Initiator - {0}", cr.RCB.Title);
//                }
//                else if (tasktype == "PRJCAT" && task.StatusCode == 5)
//                {
//                    var tmplt = templateService.GetTemplate("REASSIGNED", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.ITFHUser.Email, "", cr, task);


//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " re-assigned mail sent to ITFH - {0}", cr.ITFHUser.Title);
//                }


//                else if (tasktype == "ITEVAL" && task.StatusCode == 2) return string.Format("CR " + cr.ChangeRequestCode.ToString() + " updated successfully mail sent to Functional Approver- {0}", cr.PendingWithUser.Title);
//                else if (tasktype == "ITEVAL" && task.StatusCode == 3)
//                {
//                    var tmplt = templateService.GetTemplate("CRSENTBACK", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.ITFHUser.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " updated successfully mail sent to ITFH - {0}", cr.PendingWithUser);//return cr.ITFHUser;
//                }
//                else if (tasktype == "ITEVAL" && task.StatusCode == 4)
//                {

//                    var tmplt = templateService.GetTemplate("CRREJECTED", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, cr.ITFHUser.Email, cr, task);
//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " rejected by IT Evaluator mail sent to Initiator- {0}", cr.RCB.Title);
//                }


//                else if (tasktype == "FAAPPROVAL" && task.StatusCode == 2) return string.Format("CR " + cr.ChangeRequestCode.ToString() + " approved by Functional Approver mail sent to ITFH- {0}", cr.PendingWithUser.Title);//return cr.ITFHUser;
//                else if (tasktype == "FAAPPROVAL" && task.StatusCode == 3)
//                {
//                    var tmplt = templateService.GetTemplate("CRREJECTED", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, cr.ITFHUser.Email, cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " rejected by FunctionalApprover mail sent to Initiator- {0}", cr.RCB.Title);
//                }
//                else if (tasktype == "FAAPPROVAL" && task.StatusCode == 4)
//                {
//                    var tmplt = templateService.GetTemplate("CRSENTBACK", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " sent back by Functional Approver mail sent to Initiator- {0}", cr.RCB.Title);//return cr.ITFHUser;
//                }
//                else if (cr.StatusCode == 80) return string.Format("CR " + cr.ChangeRequestCode.ToString() + " resubmitted mail sent to ITFH - {0}", cr.ITFHUser.Title);//return cr.ITFHUser;

//                else if (tasktype == "ASSIGNMENT" && task.StatusCode == 2)
//                {
//                    var strusers = "";
//                    cr.Phases.ForEach(p =>
//                    {
//                        if (p.Tasks != null)
//                        {
//                            p.Tasks.ForEach(t =>
//                            {
//                                strusers += t.AssignedTo.Title + ", ";
//                            });
//                        }
//                    });

//                    //ask

//                    return string.Format("Task/s has been assigned for CR {1} and mail has been sent to {0}", strusers, cr.ChangeRequestCode); //return cr.ITFHUser;
//                }

//                else if (tasktype == "SDD" && task.StatusCode == 2)
//                {
//                    var tmplt = templateService.GetTemplate("WIREUPLOADED", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, "", cr, task);

//                    return string.Format("Wireframe uploaded for CR " + cr.ChangeRequestCode.ToString() + " mail sent to Initiator- {0}", cr.RCB.Title); //return cr.ITFHUser;
//                }
//                else if (tasktype == "SDD" && task.StatusCode == 1)
//                {
//                    var tmplt = templateService.GetTemplate("WIREUPLOADEDDATE", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, "", cr, task);

//                    return string.Format("Wireframe upload date for CR " + cr.ChangeRequestCode.ToString() + " updated mail sent to Initiator- {0}", cr.RCB.Title); //return cr.ITFHUser;
//                }

////                else if (cr.StatusCode == 8 && task.StatusCode == 2) return string.Format("CR tasks assigned mail sent to ITFH- {0}", cr.PendingWithUser.Title); //return cr.ITFHUser;
//                //                else if (cr.StatusCode == 8 && task.StatusCode == 2) string.Format("CR tasks assigned mail sent to ITFH- {0}", cr.PendingWithUser.Title); //return cr.ITFHUser;
//                else if (tasktype == "NONSAPEXE" && task.StatusCode == 1 && task.SubStatusCode == "reviseddate")
//                {
//                    var tmplt = templateService.GetTemplate("DATEREVISED", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.ITFHUser.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " task date revised mail sent to ITFH- {0}", cr.ITFHUser.Title); //return cr.ITFHUser;
//                }
//                else if (tasktype == "NONSAPEXE" && task.StatusCode == 1 && task.SubStatusCode == "assigntoteam")
//                {
//                    var tmplt = templateService.GetTemplate("TASKASSIGNED", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", task.TeamMember.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " task assigned to team member mail sent to {0}", task.TeamMember.Title); //return cr.ITFHUser;
//                }
//                else if (tasktype == "NONSAPEXE" && task.StatusCode == 1 && task.SubStatusCode == "reassigned")
//                {
//                    var tmplt = templateService.GetTemplate("REASSIGNED", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", task.AssignedTo.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " task reassigned mail sent to ITFH- {0}", task.AssignedTo.Title); //return cr.ITFHUser;
//                }
//                else if (tasktype == "NONSAPEXE" && task.StatusCode == 2)
//                {
//                    var tmplt = templateService.GetTemplate("UPDATEDTASK", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.ITFHUser.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " updated task {1} completed and ready for ITAT mail sent to ITFH- {0}", cr.ITFHUser.Title, task.TaskCode); //return cr.ITFHUser;
//                }

//                    //else if (tasktype == "NONSAPEXE" && task.StatusCode == 5) return string.Format("Task assigned for CR " + cr.ChangeRequestCode.ToString() + " to Team - {0}", task.TeamMember.Title); //return cr.ITFHUser;
//                //else if (tasktype == "NONSAPEXE" && task.StatusCode == 4) return string.Format("Task reassignd for CR " + cr.ChangeRequestCode.ToString() + " to ITFH - {0}", task.AssignedTo.Title); //return cr.ITFHUser;

//                else if (tasktype == "ITAT" && task.StatusCode == 2)
//                {
//                    var tmplt = templateService.GetTemplate("ITATCOMP", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, "", cr, task);

//                    return string.Format("ITAT for CR " + cr.ChangeRequestCode.ToString() + " completed mail sent to Initiator for UAT - {0}", cr.RCB.Title); //return cr.ITFHUser;
//                }
//                else if (tasktype == "ITAT" && task.StatusCode == 1)
//                {
//                    var tmplt = templateService.GetTemplate("DATEREVISED", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, "", cr, task);

//                    return string.Format("ITAT for CR " + cr.ChangeRequestCode.ToString() + " dates revised mail sent to Initiator - {0}", cr.RCB.Title); //return cr.ITFHUser;
//                }
//                else if (tasktype == "UAT" && task.StatusCode == 2)
//                {
//                    var tmplt = templateService.GetTemplate("UATCOMP", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.ITFHUser.Email, "", cr, task);

//                    return string.Format("UAT for CR " + cr.ChangeRequestCode.ToString() + " completed by requestor mail sent to ITFH - {0}", cr.ITFHUser.Title); //return cr.ITFHUser;
//                }
//                else if (tasktype == "UAT" && task.StatusCode == 1)
//                {
//                    var tmplt = templateService.GetTemplate("ONHOLD", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.ITFHUser.Email, "", cr, task);


//                    return string.Format("UAT for CR " + cr.ChangeRequestCode.ToString() + " put on hold mail sent to ITFH - {0}", cr.ITFHUser.Title); //return cr.ITFHUser;
//                }
//                else if (tasktype == "UAT" && task.StatusCode == 3)
//                {
//                    var tmplt = templateService.GetTemplate("SHORTCLOSED", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.ITFHUser.Email, "", cr, task);


//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " short-closed due to chagne in scope mail sent to ITFH- {0}", cr.ITFHUser.Title); //return cr.ITFHUser;
//                }

//                else if (tasktype == "UATSO" && task.StatusCode == 3)
//                {
//                    var tmplt = templateService.GetTemplate("SHORTCLOSED", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, cr.HODApprover.Email, cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " short-closed due to change in scope mail sent all stack-holders {0}", ""); //return cr.ITFHUser;
//                }
//                ///////////////////////////till here 22 sep 2015
//                else if (tasktype == "UATSO" && task.StatusCode == 2 && cr.SecurityCheckRequired.HasValue && cr.SecurityCheckRequired.Value)
//                {
//                    var tmplt = templateService.GetTemplate("UATSORECEIVEDSEC", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, "", cr, task); //ask

//                    return string.Format("UAT Signoff received for CR " + cr.ChangeRequestCode.ToString() + " mail sent to Security - {0}", cr.ITFHUser.Title); //return cr.ITFHUser;
//                }
//                else if (tasktype == "UATSO" && task.StatusCode == 2 && (cr.InfraCosting == "L" && (cr.HardwareCostReceived.HasValue == false || cr.HardwareCostReceived.Value == false)))
//                {
//                    var tmplt = templateService.GetTemplate("UATSOINFRA", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, "", cr, task); //ask

//                    return string.Format("UAT Signoff received for CR " + cr.ChangeRequestCode.ToString() + " mail sent to INFRA for cost - {0}", Teckraft.Data.DataHelper.GetInfraUser().Title); //return cr.ITFHUser;
//                }
//                else if (tasktype == "UATSO" && task.StatusCode == 2)
//                {

//                    var tmplt = templateService.GetTemplate("UATSORECEIVED", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, cr.RCB.Email, cr, task);

//                    return string.Format("UAT Signoff received for CR " + cr.ChangeRequestCode.ToString() + " mail sent to ITFH - {0}", cr.RCB.Title); //return cr.ITFHUser;
//                }

//                else if (tasktype == "SECURITY" && task.StatusCode == 2 && (cr.InfraCosting == "L" && (cr.HardwareCostReceived.HasValue == false || cr.HardwareCostReceived.Value == false)))
//                {
//                    var tmplt = templateService.GetTemplate("SECURITYCOMPCOSTING", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.ITFHUser.Email, Teckraft.Data.DataHelper.GetInfraUser().Email, cr, task); //ask

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " Security-test completed mail sent to Infra for costing- {0}", Teckraft.Data.DataHelper.GetInfraUser().Title); //return cr.ITFHUser;
//                }
//                else if (tasktype == "SECURITY" && task.StatusCode == 2)
//                {
//                    // var tmplt = templateService.GetTemplate("SECURITYTEST", task.TaskType.TaskTypeCode);
//                    //  emailService.SendEmail(tmplt, "", cr.ITFHUser.Email, "", cr, task); //ask

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " Security-test completed mail sent to ITFH- {0}", cr.ITFHUser.Title); //return cr.ITFHUser;
//                }
//                //                else if (tasktype == "SECURITY" && task.StatusCode == 3) return string.Format("CR Security-test completed mail sent to ITFH- {0}", cr.ITFHUser.Title); //return cr.ITFHUser;
//                else if (tasktype == "ROLLREQ" && task.StatusCode == 2 && cr.TransportSAPRequired.HasValue && cr.TransportSAPRequired.Value == true)
//                {
//                    // var tmplt = templateService.GetTemplate("TRANSREQ", task.TaskType.TaskTypeCode);
//                    //  emailService.SendEmail(tmplt, "", cr.ITEvaluatorUser.Email, "", cr, task);

//                    return string.Format("SAP for CR " + cr.ChangeRequestCode.ToString() + " Transportation request received mail sent to IT Evaluator - {0}", cr.PendingWithUser.Title); //return cr.ITFHUser;
//                }
//                //                else if (cr.StatusCode == 13 && task.StatusCode == 2) return string.Format("CR short-closed due to chagne in scope mail sent to ITFH- {0}", cr.ITFHUser.Title); //return cr.ITFHUser;

////                else if (tasktype == "ROLLREQ" && task.StatusCode == 2 && cr.TransportSAPRequired.HasValue && cr.TransportSAPRequired.Value) return string.Format("CR request for SAP Transportation mail sent to Evaluator- {0}", cr.ITFHUser.Title); //return cr.ITFHUser;
//                else if (tasktype == "ROLLAPP" && task.StatusCode == 2)
//                {
//                    cr.BasisUser = Teckraft.Data.DataHelper.GetBasisUser();
//                    var tmplt = templateService.GetTemplate("TRANSREQAPPBASIS", task.TaskType.TaskTypeCode);
//                    //emailService.SendEmail(tmplt, "", cr.BasisUser.Email,cr.ITFHUser.Email, cr, task);
//                    emailService.SendEmail(tmplt, "", cr.BasisUser.Email, "aaliya.nkhan@gmail.com;" + cr.ITFHUser.Email, cr, task);
                    

//                    return string.Format("SAP Transportation request for CR " + cr.ChangeRequestCode.ToString() + " approved mail sent to Basis - {0}", cr.ITFHUser.Title); //return cr.ITFHUser;
//                }
//                else if (tasktype == "ROLLAPP" && task.StatusCode == 3)
//                {
//                    var tmplt = templateService.GetTemplate("REJECTED", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, cr.ITFHUser.Email, cr, task);

//                    return string.Format("SAP Transportation request for CR " + cr.ChangeRequestCode.ToString() + " rejected by IT Evaluator, CR Closed. mail sent to - {0}", cr.ITFHUser.Title); //return cr.ITFHUser;
//                }

//                else if (tasktype == "ROLLAPP" && task.StatusCode == 4)
//                {
//                    var tmplt = templateService.GetTemplate("SENTBACK", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.ITFHUser.Email, "", cr, task);

//                    return string.Format("SAP Transportation request for CR " + cr.ChangeRequestCode.ToString() + " sent back by IT Evaluator, mail sent to - {0}", cr.ITFHUser.Title); //return cr.ITFHUser;
//                }


//                else if (tasktype == "ROLLCONF" && task.StatusCode == 2)
//                {
//                    var tmplt = templateService.GetTemplate("CLOSED", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " closed by ITFH - {0}", cr.ITFHUser.Title); //return cr.ITFHUser;
//                }


//            }
//            else if (cr.CRTypeId == 4)
//            {
//                //if (cr.StatusCode == 1 && task == null) return string.Format("CR ", cr.ITFHUser);
//                if (tasktype == "MDCEVAL" && task.StatusCode == 2)
//                {
//                    //    var tmplt = templateService.GetTemplate("UPDATED", task.TaskType.TaskTypeCode);
//                    //   emailService.SendEmail(tmplt, "", cr.HODApprover.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " updated successfully mail sent to Function Approver - {0}", cr.PendingWithUser.Title); // Teckraft.Data.DataHelper.GetFAUser(cr.Division.Id, cr.BusinessFunction.Id, cr.ProjectCategory);
//                }
//                else if (tasktype == "MDCEVAL" && task.StatusCode == 3)
//                {
//                    var tmplt = templateService.GetTemplate("SENTBACK", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " sent back by MDC Evaluator mail sent to Initiator - {0}", cr.RCB.Title); //cr.RCB;
//                }
//                else if (tasktype == "FAAPPROVAL" && task.StatusCode == 2 && cr.ProcessApproverFlag.HasValue && cr.ProcessApproverFlag.Value)
//                {
//                    // var tmplt = templateService.GetTemplate("APPROVED", task.TaskType.TaskTypeCode);
//                    //   emailService.SendEmail(tmplt, "", cr.ProcessApprover.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " approved by Functional Approver mail sent to Process Approver - {0}", cr.PendingWithUser.Title);//return cr.ITFHUser;;
//                }
//                else if (tasktype == "FAAPPROVAL" && task.StatusCode == 2)
//                {
//                    //  var tmplt = templateService.GetTemplate("APPROVEDMDC", task.TaskType.TaskTypeCode);
//                    //  emailService.SendEmail(tmplt, "", cr.PendingWithUser.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " approved by Function Approver mail sent to MDC Team - {0}", cr.PendingWithUser.Title);//return cr.ITFHUser;
//                }
//                else if (tasktype == "FAAPPROVAL" && task.StatusCode == 3)
//                {
//                    var tmplt = templateService.GetTemplate("SENTBACK", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " sent back by Function Approver mail sent to Initiator- {0}", cr.RCB.Title); //return cr.ITFHUser;
//                }
//                else if (tasktype == "MDCEXE" && task.StatusCode == 1)
//                {
//                    var tmplt = templateService.GetTemplate("REVISED", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " updated successfully target revised mail sent to initiator- {0}", cr.RCB.Title); //return cr.ITFHUser;
//                }
//                else if (tasktype == "MDCEXE" && task.StatusCode == 2)
//                {
//                    var tmplt = templateService.GetTemplate("COMPLETED", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " completed successfully mail sent to initiator- {0}", cr.RCB.Title); //return cr.ITFHUser;
//                }
//                else if (tasktype == "MDCEXE" && task.StatusCode == 3)
//                {
//                    var tmplt = templateService.GetTemplate("SENTBACK", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " sent back by MDC Team mail sent to initiator- {0}", cr.RCB.Title); //return cr.ITFHUser;
//                }
//                else if (tasktype == "MDCCHANGE" && task.StatusCode == 2)
//                {
//                    var tmplt = templateService.GetTemplate("UPDATED", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.PendingWithUser.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " updated by initiator mail sent to MDC Team - {0}", cr.PendingWithUser.Title); //return cr.ITFHUser;
//                }
//                else if (tasktype == "MDCEVAL" && task.StatusCode == 5)
//                {
//                    var tmplt = templateService.GetTemplate("REJECTED", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " rejected by MDC Evaluator, CR Closed. mail sent to - {0}", cr.RCB.Title); // Teckraft.Data.DataHelper.GetFAUser(cr.Division.Id, cr.BusinessFunction.Id, cr.ProjectCategory);
//                }
//                else if (tasktype == "PAAPPROVAL" && task.StatusCode == 2)
//                {
//                    //  var tmplt = templateService.GetTemplate("APPROVED", task.TaskType.TaskTypeCode);
//                    //  emailService.SendEmail(tmplt, "", cr.PendingWithUser.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " approved by Process Approver mail sent to MDC Team - {0}", cr.PendingWithUser.Title);//return cr.ITFHUser;
//                }
//                else if (tasktype == "PAAPPROVAL" && task.StatusCode == 5)
//                {
//                    var tmplt = templateService.GetTemplate("REJECTED", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " rejected by Process Approver mail sent to Initiator- {0}", cr.RCB.Title); //return cr.ITFHUser;
//                }
//                else return "CR updated successfully!";

//            }
//            else if (cr.CRTypeId == 5)
//            {
//                //if (cr.StatusCode == 1 && task == null) return cr.ITFHUser;
//                if (tasktype == "GRCEVAL" && task.StatusCode == 1)
//                {
//                    var tmplt = templateService.GetTemplate("REASSIGNED", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.PendingWithUser.Email, "", cr, task); //ask

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " reassigned by GRC team mail sent to ITFH - {0}", cr.PendingWithUser.Title); //return cr.ITFHUser; //new Teckraft.Core.Domian.Settings.User() { Id = Teckraft.Data.DataHelper.getFA(cr.Division.Id, cr.BusinessFunction.Id, cr.ProjectCategory) };
//                }
//                else if (tasktype == "GRCEVAL" && task.StatusCode == 3)
//                {
//                    var tmplt = templateService.GetTemplate("SENTBACK", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " sent back by GRC team mail sent to Initiator - {0}", cr.RCB.Title); //return cr.RCB;
//                }
//                else if (tasktype == "GRCEVAL" && task.StatusCode == 2)
//                {
//                    //  var tmplt = templateService.GetTemplate("APPROVED", task.TaskType.TaskTypeCode);
//                    //   emailService.SendEmail(tmplt, "", cr.PendingWithUser.Email, "", cr, task); //ask

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " approved by GRC team mail sent to SAP HOD - {0}", cr.PendingWithUser.Title); //return cr.HODApprover;
//                }
//                else if (tasktype == "GRCEVAL" && task.StatusCode == 4)
//                {
//                    var tmplt = templateService.GetTemplate("REJECTED", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " rejcted by GRC team mail sent to initator - {0}", cr.RCB.Title); //return cr.HODApprover;
//                }

//                else if (tasktype == "SAPHODAPP" && task.StatusCode == 2)
//                {
//                    // var tmplt = templateService.GetTemplate("APPROVED", task.TaskType.TaskTypeCode);
//                    //   emailService.SendEmail(tmplt, "", cr.PendingWithUser.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " approved by SAP HOD mail sent to GRC Team - {0}", cr.PendingWithUser.Title); //return cr.ITFHUser;
//                }
//                else if (tasktype == "SAPHODAPP" && task.StatusCode == 3)
//                {
//                    var tmplt = templateService.GetTemplate("SENTBACK", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " sent back by SAP HOD mail sent to GRC Initiator- {0}", cr.RCB.Title); //return cr.ITFHUser;
//                }
//                else if (tasktype == "SAPHODAPP" && task.StatusCode == 4)
//                {
//                    var tmplt = templateService.GetTemplate("REJECTED", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " rejeced by SAP HOD mail sent to Initiator - {0}", cr.RCB.Title); //return cr.ITFHUser;
//                }
//                else if (tasktype == "GRCEXE" && task.StatusCode == 3)
//                {
//                    var tmplt = templateService.GetTemplate("SENTBACK", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.PendingWithUser.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " sent by GRC Execution Team mail sent to GRC Team - {0}", cr.PendingWithUser.Title);//return cr.ITFHUser;
//                }
//                else if (tasktype == "GRCEXE" && task.StatusCode == 2)
//                {
//                    //  var tmplt = templateService.GetTemplate("COMPLETED", task.TaskType.TaskTypeCode);
//                    //   emailService.SendEmail(tmplt, "", cr.RCB.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " completed by GRC Execution Team mail sent to Initiator - {0}", cr.RCB.Title);//return cr.ITFHUser;
//                }
//                else if (tasktype == "GRCEXE" && task.StatusCode == 1)
//                {
//                    var tmplt = templateService.GetTemplate("DATEREVISED", task.TaskType.TaskTypeCode);
//                    emailService.SendEmail(tmplt, "", cr.RCB.Email, "", cr, task);

//                    return string.Format("CR " + cr.ChangeRequestCode.ToString() + " dates revised by GRC Execution Team mail sent to Initiator - {0}", cr.RCB.Title);//return cr.ITFHUser;
//                }
//                else return null;
//            }


//            return string.Format("CR  " + cr.ChangeRequestCode.ToString() + " updated Successfully!", "");
//        }

        public static User GetAssignedToUser(string p)
        {
            return Teckraft.Data.DataHelper.GetInfraUser();
        }
    }
}
