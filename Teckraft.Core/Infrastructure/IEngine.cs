using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teckraft.Core.Caching;

namespace Teckraft.Core.Infrastructure
{
    public interface IEngine
    {
        void Initialize();
        ICacheManager CacheManager { get; }
        GeneralSetting Settings { get; }
    }
}
