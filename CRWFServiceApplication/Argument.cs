using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRWFServiceApplication
{
    public class Argument<T>
    {
        private T t;
        public T Get() {
            return t;
        }
    }
}