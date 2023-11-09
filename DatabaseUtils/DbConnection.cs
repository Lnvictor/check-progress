using Npgsql;

namespace DatabaseUtils
{
    public class DbConnection
    {
        private static DbConnection? connection;
        private NpgsqlConnection? npgsqlConnection;
        private string connectionString;

        private DbConnection(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public static DbConnection GetInstance(string connectionString)
        {
            if (connection == null)
            {
                connection = new DbConnection(connectionString);
            }
            return connection;
        }

        public NpgsqlConnection GetConnection() {
            if (npgsqlConnection == null)
            {
                npgsqlConnection = new NpgsqlConnection(connectionString);
                npgsqlConnection.Open();
            }
            return npgsqlConnection;
        }

        public bool CloseConnection()
        {
            try
            {
                if (npgsqlConnection != null)
                {
                    npgsqlConnection.Close();
                    npgsqlConnection.Dispose();
                    npgsqlConnection = null;
                }
                return true;
            }
            catch { return false; }
        }
    }
}