using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Library_WPF.Service
{
    public class DapperExecutor
    {
        public static string ConnectionString { get; set; }

        public DapperExecutor()
        {

        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
