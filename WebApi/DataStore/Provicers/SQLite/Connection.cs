using System.Data.SQLite;

namespace DataStore.Provicers.SQLite
{
    public class Connection : IConnection
    {
        private string _connectionString;

        public Connection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection(_connectionString);
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
