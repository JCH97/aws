using FluentValidation.AspNetCore;

namespace LearningAWS.Infra.Installers;

public class ControllersInstaller : IInstaller
{
    public int Order { get; set; } = int.MaxValue;

    public void Install(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection
           .AddControllers()
           .AddFluentValidation(f =>
            {
                f.RegisterValidatorsFromAssemblyContaining<IMarkerApi>();
                f.DisableDataAnnotationsValidation = true;
            });
    }
}