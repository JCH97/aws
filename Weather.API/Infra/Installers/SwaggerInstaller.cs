namespace LearningAWS.Infra.Installers;

public class SwaggerInstaller : IInstaller
{
    public int Order { get; set; } = int.MaxValue;


    public void Install(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen();
    }
}