using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teckraft.Core.Domian.Settings;

namespace Teckraft.Core.Domian.Loging
{
    public class EventLog:BaseEntity
    {
        public string EventType { get; set; }
        public string Details { get; set; }
        public System.DateTime RCT { get; set; }
        public User RCB { get; set; }
        public string ApplicationName { get; set; }
        public string IP { get; set; }
        public string Url { get; set; }
    }
}