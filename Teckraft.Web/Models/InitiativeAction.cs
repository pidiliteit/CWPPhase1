using System.Collections.Generic;
using Teckraft.Core.Domian.Settings;


namespace Teckraft.Web.Models
{
    public class InitiativeAction
    {
        public List<Initiative> Items { get; set; }
        public Initiative Item { get; set; }
        public int StatusCode { get; set; }
        //public string Comments { get; set; }
    }
}