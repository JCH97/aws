using LearningAWS.Application.Dtos;

namespace LearningAWS.Application.UseCases.Weather;

public interface IWeatherUseCases
{
    public Task<bool> CreateAsync(WeatherCreateDto dto);

    public Task<IEnumerable<WeatherDto>> GetAllAsync();
}