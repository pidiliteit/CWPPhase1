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
    
    public partial class Division
    {
        public Division()
        {
            this.UserDetails = new HashSet<UserDetail>();
        }
    
        public int Id { get; set; }
        public string DivisionName { get; set; }
        public string DivisionCode { get; set; }
    
        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
