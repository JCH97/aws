using Amazon.SQS;

namespace LearningAWS.Infra.Installers;

public class AwsInstaller : IInstaller
{
    public int Order { get; set; } = 3;

    public void Install(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddSingleton<IAmazonSQS, AmazonSQSClient>();
    }
}