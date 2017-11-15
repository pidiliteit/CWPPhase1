using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teckraft.Core.Workflow
{
    public class Process
    {
        public List<Task> Tasks { get; set; }
        public string ProcessName { get; set; }
    }
}