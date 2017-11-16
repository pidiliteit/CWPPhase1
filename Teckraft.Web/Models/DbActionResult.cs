using System;
using Teckraft.Core.Domian;

namespace Teckraft.Web.Models
{
   public class DbActionResult<T> :BaseEntity
    {
        public T Item { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Exp { get; set; }


        public Exception Exception { get; set; }
    }
}
