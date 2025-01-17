using System;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Threading.Tasks;

using NPoco;

namespace AzureBlobStorageSampleApp.Functions
{
    public abstract class BaseDatabaseService
    {
        readonly static string _connectionString = Environment.GetEnvironmentVariable("PhotoDatabaseConnectionString");

        protected static TResult PerformDatabaseFunction<TResult>(Func<Database, TResult> databaseFunction)
        {
            using var connection = new Database(_connectionString, DatabaseType.SqlServer2012, SqlClientFactory.Instance);

            try
            {
                return databaseFunction(connection);
            }
            catch (Exception e)
            {
                Debug.WriteLine("");
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.ToString());
                Debug.WriteLine("");

                throw;
            }
        }

        protected static async Task<TResult> PerformDatabaseFunction<TResult>(Func<Database, Task<TResult>> databaseFunction)
        {
            using var connection = new Database(_connectionString, DatabaseType.SqlServer2012, SqlClientFactory.Instance);

            try
            {
                return await databaseFunction(connection).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Debug.WriteLine("");
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.ToString());
                Debug.WriteLine("");

                throw;
            }
        }
    }
}
