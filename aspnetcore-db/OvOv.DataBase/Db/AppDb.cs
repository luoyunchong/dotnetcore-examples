using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBase.Db
{
    public class AppDb : IDisposable
    {
        public MySqlConnection Connection;
        public AppDb(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }
        public void Dispose()
        {
            if (Connection != null && Connection.State != System.Data.ConnectionState.Closed)
            {
                Connection.Close();
            }
        }
    }
}
