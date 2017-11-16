using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teckraft.Core.Caching
{
    public class RemoveCache:Attribute
    {
        public RemoveCache(string[] keys, bool enable = true)
        {
            this.Enable = enable;
            this.Key = keys;
            
        }

        public bool Enable { get; set; }

        public string[] Key { get; set; }
    }
}