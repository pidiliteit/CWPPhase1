using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teckraft.Core.Domian.Settings
{
    public class ChapterMaster:BaseEntity
    {
         public string ChapterName { get; set; }
         public string ChapterOwner { get; set; }
         public string ChapterNameFromSystem { get; set; }
         public string ChapterNameDisplay { get; set; }
         public string RunDate { get; set; }
         public bool EditFlag { get; set; }
         public System.DateTime RCT { get; set; }
         public Nullable<bool> EmailFlag { get; set; }
         public string EmailTo { get; set; }
        public int IssueCount { get; set; }
    }
}