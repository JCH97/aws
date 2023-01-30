using LearningAWS.Application.UseCases.Weather;
using LearningAWS.Infra.Events;
using LearningAWS.Infra.Repositories;

namespace LearningAWS.Infra.Installers;

public class MiscInstallers : IInstaller
{
    public int Order { get; set; } = 2;


    public void Install(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddAuthorization();

        serviceCollection.AddScoped<IWeatherRepository, WeatherRepository>();
        serviceCollection.AddScoped<IWeatherUseCases, WeatherUseCases>();

        serviceCollection.AddSingleton<ISqsPublisher, SqsPublisher>();
    }
}