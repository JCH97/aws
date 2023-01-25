using LearningAWS.Infra.Database;

namespace LearningAWS.Infra.Installers;

public class DbInstaller : IInstaller
{
    public int Order { get; set; } = 1;

    public void Install(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddSingleton<IDbConnectionFactory>(_ =>
            new SqliteDbConnectionFactory(configuration.GetValue<string>("Database:ConnectionString")!));
        
        serviceCollection.AddSingleton<DatabaseInitializer>();
    }
}