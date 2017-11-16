using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teckraft.Core.Domian.Transactions
{
    public interface IActionResult

    {
         string Message { get; set; }
         bool Success { get; set; }
    }
}
