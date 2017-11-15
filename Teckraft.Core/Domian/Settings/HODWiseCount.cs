using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teckraft.Core.Domian.Settings
{
    public class HODWiseCount : BaseEntity
    {
        //public string HODName { get; set; }
        //public Nullable<int> Count { get; set; }

        public string SAPDivisionName { get; set; }
        public Nullable<int> count { get; set; }
    }
}
 