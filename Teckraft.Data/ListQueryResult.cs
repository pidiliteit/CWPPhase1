using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teckraft.Core.Domian;
using Teckraft.Core.Domian.Transactions;

namespace Teckraft.Data
{
    public class ListQueryResult<T> where T:BaseEntity
    {
        public List<T> Items { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalRecords { get; set; }

    }
}
