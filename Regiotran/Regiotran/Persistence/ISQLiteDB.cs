using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Regiotran.Persistence
{
    public interface ISQLiteDB
    {
        SQLiteAsyncConnection GetConnection();
    }
}
