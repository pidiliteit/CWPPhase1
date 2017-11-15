using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teckraft.Core.Caching;

namespace Teckraft.Core.Infrastructure
{
    public class TeckraftEngine:IEngine
    {
        private ICacheManager _cacheManager;
        private GeneralSetting _settings;
        public void Initialize()
        {
            _cacheManager = new MemoryCacheManager();
            _settings = new GeneralSetting();
        }

        public ICacheManager CacheManager { get {
            return _cacheManager;
        } }

        public GeneralSetting Settings { get { return _settings; } }

    }
}