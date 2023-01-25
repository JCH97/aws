namespace LearningAWS.Application.Dtos;

public class WeatherDto
{
    public int Id { get; init; }

    public required string Date { get; init; }

    public required int TemperatureC { get; init; }

    public required string Region { get; init; }

    public string? Summary { get; set; }
}