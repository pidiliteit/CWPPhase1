﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teckraft.Data
{
    public class ListQueryTest
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<QueryParameter> Parameters { get; set; }
    }
}
