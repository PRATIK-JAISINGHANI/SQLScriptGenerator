using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLQueryGenerator.DataContracts
{
    public class ExecuteResponse
    {
        public bool IsActionSuccessful { get; set; }

        public Array Scripts { get; set; }
    }
}
