using Microsoft.Data.Sqlite;
using System;

namespace Proiect
{
    public static class ConnectionUtils
    {
        public static SqliteConnection CreateConnection()
        {

            SqliteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SqliteConnection(Constants.ConnectionString);
         // Open the connection:
         try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {

            }
            return sqlite_conn;
        }
    }
}
