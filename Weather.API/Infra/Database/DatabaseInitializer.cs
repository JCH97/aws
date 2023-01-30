using Dapper;
using LearningAWS.Domain.Env;
using Microsoft.Extensions.Options;

namespace LearningAWS.Infra.Database;

public static class DbInitializer
{
    public static async Task InitializeDbAsync(this IServiceProvider provider)
    {
        var databaseInitializer = provider.GetRequiredService<DatabaseInitializer>();
        await databaseInitializer.InitializeAsync();
    }
}

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