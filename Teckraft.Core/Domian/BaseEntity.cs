using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Teckraft.Core.Domian.SAP;
using Teckraft.Core.Domian.Settings;

namespace Teckraft.Core.Domian
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime RCT { get; set; }
        public DateTime RUT { get; set; }
        public User RCB { get; set; }
         

    }
}
