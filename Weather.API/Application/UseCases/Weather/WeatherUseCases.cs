using LearningAWS.Application.Dtos;
using LearningAWS.Domain.Contracts;
using LearningAWS.Domain.Entities;
using LearningAWS.Domain.Env;
using LearningAWS.Infra.Events;
using LearningAWS.Infra.Mappers;
using LearningAWS.Infra.Repositories;
using Microsoft.Extensions.Options;

namespace LearningAWS.Application.UseCases.Weather;

public class WeatherUseCases : IWeatherUseCases
{
    private readonly IWeatherRepository _weatherRepository;
    private readonly ISqsPublisher _sqsPublisher;
    private readonly IOptions<WeatherQueue> _weatherQueueOptions;

    public WeatherUseCases(IWeatherRepository weatherRepository,
                           ISqsPublisher sqsPublisher,
                           IOptions<WeatherQueue> weatherQueueOptions)
    {
        _weatherRepository = weatherRepository;
        _sqsPublisher = sqsPublisher;
        _weatherQueueOptions = weatherQueueOptions;
    }

    public async Task<bool> CreateAsync(WeatherCreateDto dto)
    {
        var domain = new WeatherForecast
            { Date = dto.Date, TemperatureC = dto.TemperatureC, Region = dto.Region, Summary = dto.Summary };

        var isOk = await _weatherRepository.CreateAsync(domain);

        if (isOk)
            await _sqsPublisher.Publish(domain.ToSqsMessage(), _weatherQueueOptions.Value.Name);

        return isOk;
    }

    public async Task<IEnumerable<WeatherDto>> GetAllAsync()
    {
        return await _weatherRepository.GetAllAsync();
    }
}