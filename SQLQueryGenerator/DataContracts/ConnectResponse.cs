using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLQueryGenerator.DataContracts
{
    public class ConnectResponse
    {
        public bool IsConnected { get; set; }

        public string ConnectionString { get; set; }
    }
}
