namespace LearningAWS.Application.Dtos;

public class WeatherCreateDto
{
    public required string Date { get; init; }

    public required int TemperatureC { get; init; }

    public required string Region { get; init; }

    public string? Summary { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}