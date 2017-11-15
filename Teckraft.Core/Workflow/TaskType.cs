using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teckraft.Core.Workflow
{
    public class TaskType:Core.Domian.BaseEntity
    {
        public string TaskTypeCode { get; set; }
        public string TaskTypeDesc { get; set; }
    }
}