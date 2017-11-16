using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teckraft.Core.Caching
{
    public class CacheAttribute : Attribute
    {
        public CacheAttribute(string key, bool enable=true)
        {
            this.Enable = enable;
            this.Key = key;
            
        }

        public bool Enable { get; set; }

        public string Key { get; set; }
    }
}
