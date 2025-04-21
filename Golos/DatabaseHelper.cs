using System.Data.OleDb;

namespace Voiting
{
    public static class DatabaseHelper
    {
        private const string ConnectionString =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Mi\Documents\Database312.mdb;";

        public static OleDbConnection GetConnection()
        {
            return new OleDbConnection(ConnectionString);
        }
    }
}
