using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teckraft.Core.Domian.Transactions
{
    public class CRLog:BaseEntity
    {

        public string StageName { get; set; }
        public string Comments { get; set; }
        public string UserName { get; set; }
        public string StatusText { get; set; }

        public int StatusCode { get; set; }
        public string LogDetail { get; set; }
        public string LogType { get; set; }
        public DateTime? AssignedDate { get; set; }
    }
}
