using System.Data.SQLite;
using System;
using System.Configuration;

namespace Proiect
{
    public static class ConnectionUtils
    {
        public static SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection(ConfigurationManager.AppSettings["connectionString"]);
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
