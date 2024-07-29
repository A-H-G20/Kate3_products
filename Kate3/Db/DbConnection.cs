using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kate3.Db
{
    public class DbConnection
    {
        private readonly string connectionString;
        public DbConnection()
        {
            connectionString = "Data Source=BOJASTARA\\SQLEXPRESS;Initial Catalog=Kate3;Integrated Security=True;Encrypt=False;";
        }
        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
