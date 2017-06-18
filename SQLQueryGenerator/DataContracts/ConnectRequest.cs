using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLQueryGenerator.DataContracts
{
    public class ConnectRequest
    {
        public string ServerName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string DatabaseName { get; set; }
    }
}
