using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLQueryGenerator.DataContracts
{
    public class ExecuteRequest
    {
        public string ConnectionString { get; set; }

        public bool IncludeIdentity { get; set; }

        public string Query { get; set; }
    }
}
