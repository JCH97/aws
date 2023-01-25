using System.Reflection;

namespace LearningAWS.Infra.Installers;

public static class InstallExtensions
{
    public static void Install(
        this IServiceCollection serviceCollection,
        IConfiguration configuration,
        params Type[] types)
    {
        var assemblies = types.Select(t => t.Assembly).ToArray();

        foreach (var assembly in assemblies)
        {
            var installers = assembly
                            .ExportedTypes
                            .Where(a => typeof(IInstaller).IsAssignableFrom(a) &&
                                        a is { IsInterface: false, IsAbstract: false })
                            .Select(Activator.CreateInstance)
                            .Cast<IInstaller>()
                            .OrderBy(x => x.Order)
                            .ToList();

            installers.ForEach(i => i.Install(serviceCollection, configuration));
        }
    }
}