using LearningAWS.Application.Dtos;

namespace LearningAWS.Infra.Repositories;

public interface IWeatherRepository
{
    Task<bool> CreateAsync(WeatherCreateDto dto);

    Task<IEnumerable<WeatherDto>> GetAllAsync();
}