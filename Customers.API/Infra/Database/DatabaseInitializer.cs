using Dapper;

namespace LearningAWS.Infra.Database;

public class DatabaseInitializer
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DatabaseInitializer(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task InitializeAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        await connection.ExecuteAsync(
            @"CREATE TABLE IF NOT EXISTS Weather (
        Id INTEGER PRIMARY KEY AUTOINCREMENT, 
        Summary TEXT NOT NULL,
        Region TEXT NOT NULL,
        TemperatureC INTEGER NOT NULL,
        Date TEXT NOT NULL)");
    }
}