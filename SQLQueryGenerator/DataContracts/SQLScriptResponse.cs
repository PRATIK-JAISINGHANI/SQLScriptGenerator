using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLQueryGenerator.DataContracts
{
    public class SQLScriptResponse
    {
        public bool IsActionSuccessful { get; set; }

        public Array Script { get; set; }
    }
}
