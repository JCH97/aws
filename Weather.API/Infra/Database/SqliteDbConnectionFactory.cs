using System.Data;
using Microsoft.Data.Sqlite;

namespace LearningAWS.Infra.Database;

public interface IDbConnectionFactory
{
    public Task<IDbConnection> CreateConnectionAsync();
}

public class SqliteDbConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public SqliteDbConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IDbConnection> CreateConnectionAsync()
    {
        var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        return connection;
    }
}