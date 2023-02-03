namespace SnsPublisher;

public class WeatherCreated
{
    public int Id { get; set; }

    public string Date { get; set; }

    public int TemperatureC { get; set; }

    public string Region { get; set; }

    public string? Summary { get; set; }
}