using LearningAWS.Application.Dtos;
using LearningAWS.Domain.Entities;

namespace LearningAWS.Infra.Repositories;

public interface IWeatherRepository
{
    Task<bool> CreateAsync(WeatherForecast domain);

    Task<IEnumerable<WeatherDto>> GetAllAsync();
}