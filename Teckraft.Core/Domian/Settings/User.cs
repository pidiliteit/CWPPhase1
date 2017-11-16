using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teckraft.Core.Domian.SAP;
//using Teckraft.Core.Domian.SAP;

namespace Teckraft.Core.Domian.Settings
{
    public class User:BaseEntity
    {
        public string UserName { get; set; }
        public string Title { get; set; }
        public static User Current { get { return new User() { Id = 1 }; } }
        public Division Division { get; set; }
        public string MobileNo { get; set; }
       // public Company Company { get; set; }
        public string CurrentIP { get; set; }
        public string Email { get; set; }
       // public bool? IsITFH { get; set; }
        public string Role { get; set; }
        public string CSFRToken { get; set; }
        public String UserType { get; set; }
        public override string ToString()
        {
            return Title;
        }

       // public bool? IsEvaluator { get; set; }

        public bool IsTeamMember { get; set; }
       // public List<Team> Teams { get; set; }
    }
}
