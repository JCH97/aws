using System.Text.Json;
using MediatR;
using Weather.API.Consumer.Contracts;

namespace Weather.API.Consumer.MediatR;

public class WeatherCreatedHandler : IRequestHandler<WeatherCreatedMessage>
{
    private readonly ILogger<WeatherCreatedHandler> _logger;

    public WeatherCreatedHandler(ILogger<WeatherCreatedHandler> logger)
    {
        _logger = logger;
    }

    public Task<Unit> Handle(WeatherCreatedMessage request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(JsonSerializer.Serialize(request));

        return Unit.Task;
    }
}