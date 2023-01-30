using LearningAWS.Domain.Env;

namespace LearningAWS.Infra.Installers;

public static class RegisterEnvs
{
    public static void RegisterEnvVars(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        RegisterSqs(serviceCollection, configuration);
    }

    private static void RegisterSqs(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<SqsEnvs>(configuration.GetSection(SqsEnvs.Key));
        serviceCollection.Configure<WeatherQueue>(configuration.GetSection(WeatherQueue.Key));
    }
}