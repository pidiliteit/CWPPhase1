using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teckraft.Core.Domian.Settings
{
    public class IssueManagementCommentLog:BaseEntity
    {
        public string ID_Comments { get; set; }
        public Nullable<bool> Active { get; set; }
        public string ID_Request { get; set; }
        public string id_System_Name { get; set; }
        public string ID_Status { get; set; }
        public string ID_Pending_With_Email { get; set; }
        public Nullable<bool> UploadFlag { get; set; }
        public Nullable<System.DateTime> RCT { get; set; }
        public User RCB { get; set; }
        public Nullable<System.DateTime> RUT { get; set; }
        public User RUB { get; set; }
        public StatusMaster ID_Status_Id { get; set; }
        public bool EmailFlag { get; set; }
        public string RCBEmail { get; set; }
        public string RCBEmpCode { get; set; }
        public string RCBEmpName { get; set; }
        public string RUBEmail { get; set; }
        public string RUBEmpCode { get; set; }
        public string RUBEmpName { get; set; }
        public Nullable<System.DateTime> Target_Date { get; set; }
    }
}