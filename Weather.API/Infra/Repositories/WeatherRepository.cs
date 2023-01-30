using Dapper;
using LearningAWS.Application.Dtos;
using LearningAWS.Domain.Entities;
using LearningAWS.Infra.Database;

namespace LearningAWS.Infra.Repositories;

public class WeatherRepository : IWeatherRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public WeatherRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> CreateAsync(WeatherForecast domain)
    {
        var connection = await _connectionFactory.CreateConnectionAsync();
        var ans =
            await connection.ExecuteAsync(
                @"INSERT INTO Weather (Summary, Region, TemperatureC, Date) VALUES (@Summary, @Region, @TemperatureC, @Date)",
                domain);

        return ans > 0;
    }

    public async Task<IEnumerable<WeatherDto>> GetAllAsync()
    {
        var connection = await _connectionFactory.CreateConnectionAsync();

        return await connection.QueryAsync<WeatherDto>(
            @"SELECT * FROM Weather");
    }
}