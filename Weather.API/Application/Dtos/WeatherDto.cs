using LearningAWS.Domain.Entities;
using LearningAWS.Domain.Interfaces;

namespace LearningAWS.Application.Dtos;

public class WeatherDto
{
    public int Id { get; set; }

    public string Date { get; set; }

    public int TemperatureC { get; set; }

    public string Region { get; set; }

    public string? Summary { get; set; }
}