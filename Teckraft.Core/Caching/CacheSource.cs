using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teckraft.Core.Caching
{
    public class CacheSource:Attribute
    {
        public CacheSource(string cacheSourceKey, bool enable = true) {
            this.CacheSourceKey = cacheSourceKey;
            this.Enable = enable;
        }

        public string CacheSourceKey { get; set; }

        public bool Enable { get; set; }
    }
}