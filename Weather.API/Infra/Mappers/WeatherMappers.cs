using LearningAWS.Domain.Contracts;
using LearningAWS.Domain.Entities;

namespace LearningAWS.Infra.Mappers;

public static class WeatherMappers
{
    public static WeatherCreateMessage ToSqsMessage(this WeatherForecast domain)
    {
        return new WeatherCreateMessage
        {
            Id = domain.Id,
            Date = domain.Date,
            Region = domain.Region,
            TemperatureC = domain.TemperatureC,
            Summary = domain.Summary ?? string.Empty
        };
    }
}