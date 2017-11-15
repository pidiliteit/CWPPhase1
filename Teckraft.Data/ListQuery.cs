using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teckraft.Core.Domian;

namespace Teckraft.Data
{
    public class ListQuery<T>:BaseEntity
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<QueryParameter> Parameters { get; set; }

        public void AddParameter(QueryParameter queryParameter)
        {
            if (this.Parameters == null) Parameters = new List<QueryParameter>();
            Parameters.Add(queryParameter);
        }

        public void AddParameter(string name, string value)
        {
            this.AddParameter(new QueryParameter() { Name = name, Value = value });
        }
    }
}
