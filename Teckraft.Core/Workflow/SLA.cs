using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teckraft.Core.Domian;

namespace Teckraft.Core.Workflow
{
    public class SLA:BaseEntity
    {
      
        public int CRTypeId { get; set; }
        public int ModuleId { get; set; }
        public int SubModuleId { get; set; }
        public int SLA1Days { get; set; }
        public int SLA2Days { get; set; }
    }
}