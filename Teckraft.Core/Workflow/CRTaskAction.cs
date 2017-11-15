using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teckraft.Core.Workflow
{
    public class CRTaskAction
    {
        public Task Item { get; set; }
        public int StatusCode { get; set; }
        public string Comments { get; set; }
    }
}