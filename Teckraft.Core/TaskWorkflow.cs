
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teckraft.Core.Domian;

namespace Teckraft.Core.Workflow
{
    public class TaskWorkflow:BaseEntity
    {
      //  public TaskType NewTaskType { get; set; }

        public string NewStageText { get; set; }

        public string NewStatusText { get; set; }

        public int? NewStatusCode { get; set; }

        public string LogDetail { get; set; }

        public string NewStageCode { get; set; }

        public bool IsCompleted { get; set; }

        public bool? UpdateStatus { get; set; }
    }
}