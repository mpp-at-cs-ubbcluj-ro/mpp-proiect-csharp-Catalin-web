using System.Data.SQLite;
using System;

namespace Proiect
{
    public static class ConnectionUtils
    {
        public static SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection(Constants.ConnectionString);
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
