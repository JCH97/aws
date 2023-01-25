using LearningAWS.Application.Dtos;
using LearningAWS.Infra.Repositories;

namespace LearningAWS.Application.UseCases.Weather;

public class WeatherUseCases : IWeatherUseCases
{
    private readonly IWeatherRepository _weatherRepository;

    public WeatherUseCases(IWeatherRepository weatherRepository)
    {
        _weatherRepository = weatherRepository;
    }

    public async Task<bool> CreateAsync(WeatherCreateDto dto)
    {
        return await _weatherRepository.CreateAsync(dto);
    }

    public async Task<IEnumerable<WeatherDto>> GetAllAsync()
    {
        return await _weatherRepository.GetAllAsync();
    }
}