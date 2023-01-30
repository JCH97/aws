namespace LearningAWS.Domain.Env;

public record SqsEnvs
{
    public const string Key = "SQS";

    public string API_KEY { get; set; }

    public string API_SECRET { get; set; }
}

public record WeatherQueue
{
    public const string Key = "WeatherQueue";

    public string Name { get; set; }
}