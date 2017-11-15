using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeckraftBuilds.WFActivities
{
    public class Argument<T>
    {
        private T t;
        public T Get() {
            return t;
        }

        public void Set(T value)
        {
            t = value;
        }

        
    }
}