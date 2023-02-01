namespace Weather.API.Consumer.Contracts;

public class WeatherCreatedMessage : ISqsMessage
{
    public Events EventType { get; init; } = Events.WeatherCreated;

    public int Id { get; set; }

    public string Date { get; set; }

    public int TemperatureC { get; set; }

    public string Region { get; set; }

    public string? Summary { get; set; }
}