//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Teckraft.Data.Sql
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChapterMaster
    {
        public int Id { get; set; }
        public string ChapterNameFromSystem { get; set; }
        public string ChapterNameDisplay { get; set; }
        public Nullable<bool> EmailFlag { get; set; }
        public string EmailTo { get; set; }
        public System.DateTime RCT { get; set; }
        public string EmailCC { get; set; }
    }
}
