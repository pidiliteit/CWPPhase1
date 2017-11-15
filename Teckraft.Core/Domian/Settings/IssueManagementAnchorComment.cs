using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teckraft.Core.Domian.Settings
{
    public class IssueManagementAnchorComment:BaseEntity
    {
        public string ID_Comments { get; set; }
        public Nullable<bool> Active { get; set; }
        public string ID_Request { get; set; }
        public string id_System_Name { get; set; }
        public Nullable<System.DateTime> RCT { get; set; }
        public User RCB { get; set; }
        public Nullable<System.DateTime> RUT { get; set; }
        public User RUB { get; set; }
        public string ID_Pending_With_Email { get; set; }

    }
}