using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teckraft.Data
{
    public class QueryParameter
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public CompareOperator Operator { get; set; }
    }
}
