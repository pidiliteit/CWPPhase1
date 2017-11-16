using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teckraft.Core.Domian.Settings
{
    public class IssueDetails:BaseEntity
    {
       public  string  ChapterName {get;set;}
       public string RunDate {get;set;}
       public string ChapterOwner {get;set;}
       public string RequestNo {get;set;}
       public string IssueDetail1 {get;set;}
       public string Status {get;set;}
        public string ReportedByName{get;set;}
        public string ReportedByEmail { get; set; }
        public string Department {get;set;}
        public string Location {get;set;}
        public string PendingWithName {get;set;}
        public string PendingWithEmail { get; set; }
        public string ResponsibleWithName { get; set; }
        public string ResponsibleWithEmail { get; set; }

        public int PendingSince { get; set; }

        public string PendingStatus { get; set; }
        public string LoggedDate { get; set; }
        public string Comments { get; set; }
        public string SystemChapterName { get; set; }
        public string TAT { get; set; }
        public string ID_Category { get; set; }
        public string ID_Target_Date { get; set; }

        public string AnchorSentBackComments { get; set; }
        public string AnchorCommentDate { get; set; }
        public string AnchorName { get; set; }
     }
}