using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore.Provicers.SQLite
{
    public interface IConnection
    {
        SQLiteConnection CreateConnection();
    }
}
