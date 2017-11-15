using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teckraft.Core.Workflow
{
    public class Precedence
    {
        public List<Process> Processes { get; set; }
        public int SequenceNo { get; set; }
    }
}