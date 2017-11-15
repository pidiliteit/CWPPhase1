using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teckraft.Core.Domian.SAP
{
    public class Division:BaseEntity
    {
        public string DivisionName { get; set; }
        public string DivisionCode { get; set; }
    }
}