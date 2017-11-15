using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teckraft.Core.Domian.Settings
{
    public class UserWiseColumnName:BaseEntity
    {
        public Nullable<int> ChapterId { get; set; }
        public Nullable<bool> ID_Issue_Detail1 { get; set; }
        public Nullable<bool> chapternamedisplay { get; set; }
        public Nullable<bool> id_data_load_date { get; set; }
        public Nullable<bool> title { get; set; }
        public Nullable<bool> chapterOwner { get; set; }
        public Nullable<bool> id_request { get; set; }
        public Nullable<bool> id_status { get; set; }
        public Nullable<bool> id_reported_by_name { get; set; }
        public Nullable<bool> id_reported_by_email { get; set; }
        public Nullable<bool> id_dept { get; set; }
        public Nullable<bool> id_location { get; set; }
        public Nullable<bool> id_pending_with_name { get; set; }
        public Nullable<bool> id_pending_with_email { get; set; }
        
        public Nullable<bool> ID_Logged_Date { get; set; }
        public Nullable<bool> pendingSince { get; set; }
        public Nullable<bool> id_tat_status { get; set; }
        public Nullable<bool> id_comments { get; set; }
        public Nullable<bool> ID_Target_Date { get; set; }
        public Nullable<bool> ID_Category { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> RCB { get; set; }
        public Nullable<System.DateTime> RCT { get; set; }
        public Nullable<System.DateTime> RUT { get; set; }
        public Nullable<int> RUB { get; set; }
        public string chapterName { get; set; }
    }
}