namespace LearningAWS.Infra.Installers;

public interface IInstaller
{
    public int Order { get; set; }

    public void Install(IServiceCollection serviceCollection, IConfiguration configuration);
}