using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teckraft.Core.Domian.Settings
{
    public class Department : BaseEntity
    {
        public string DivisionName { get; set; }
        public string DivisionCode { get; set; }
    }
}