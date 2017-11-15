using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teckraft.Core.Domian.Settings
{
    public class HodNames : BaseEntity
    {
        public string SAPDivisionName { get; set; }
        public string SAPDivisionCode { get; set; }
        public Nullable<int> count { get; set; }
        public string HodEmail { get; set; }
    }
}