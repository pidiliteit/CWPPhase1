using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teckraft.Core.Domian;
using Teckraft.Core.Domian.Settings;

namespace Teckraft.Core.Workflow
{
    public class Task:BaseEntity
    {
        public string Title { get; set; }

        public Domian.Settings.User AssignedTo { get; set; }

        public Guid UniqueId { get; set; }
        public Guid CorrelationId { get; set; }


        public int TeamId { get; set; }

        public TaskType TaskType { get; set; }
        public bool Completed { get; set; }

      
        public string Subject { get; set; }
        public int CRPhaseId { get; set; }
        public int TaskNo { get; set; }
       
        public int ParentTaskId { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public Nullable<System.DateTime> RevisedStartDate { get; set; }
        public Nullable<System.DateTime> RevisedEndDate { get; set; }
        public string RevisedDateRemark { get; set; }
        public int StatusCode { get; set; }
        public int NoOfDays { get; set; }

        public int RUB { get; set; }
        public Teckraft.Core.Domian.Transactions.CR CR { get; set; }
        public int CRStageId { get; set; }
        //public TaskUpdateAction CurrentTaskAction { get; set; }
        public DateTime? CompletedDate { get; set; }
        public Domian.Transactions.CRStage Stage { get; set; }
        public string Comments { get; set; }
        public User Level2 { get; set; }
        public string TaskCode { get {
            try
            {
                if (this.Stage != null && this.Stage.CR != null)
                {
                    return this.Stage.CR.ChangeRequestCode + "-" + this.Stage.StageCode.Substring(0, 1) +"-"+ this.TaskNo.ToString();
                }
                else return TaskNo.ToString();
            }
            catch { 
            }
            return "";
        } }
        public DateTime? TargetDate { get; set; }
        public Domian.Settings.User RUBUser { get; set; }

        public DateTime? RevisedUATDate { get; set; }

        public DateTime? UATDate { get; set; }

        public DateTime? RevisedITATDate { get; set; }

        public DateTime? ITATDate { get; set; }
        public User TeamMember { get; set; }
        public string SubStatusCode { get; set; }

        public Team Team { get; set; }
    }
}