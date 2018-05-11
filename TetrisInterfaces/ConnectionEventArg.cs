using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisInterfaces
{
    public delegate void ConnectionBuild(object sender, ConnEventArg args);

    public class ConnEventArg : EventArgs
    {
        public ConnEventArg(SqlConnectionStringBuilder connStr)
        {
            ConnString = connStr;
        }

        public SqlConnectionStringBuilder ConnString { get; }
    }
}
