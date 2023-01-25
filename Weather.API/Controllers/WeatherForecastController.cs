using LearningAWS.Application.Dtos;
using LearningAWS.Application.UseCases.Weather;
using LearningAWS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LearningAWS.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherUseCases _weatherUseCases;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,
                                     IWeatherUseCases weatherUseCases)
    {
        _logger = logger;
        _weatherUseCases = weatherUseCases;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<WeatherDto>>> GetAll()
    {
        return Ok(await _weatherUseCases.GetAllAsync());
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] WeatherCreateDto dto)
    {
        var result = await _weatherUseCases.CreateAsync(dto);

        return result ? Ok() : BadRequest();
    }

    // [HttpGet(Name = "GetWeatherForecast")]
    // public IEnumerable<WeatherForecast> Get()
    // {
    //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //                       {
    //                           Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
    //                           TemperatureC = Random.Shared.Next(-20, 55),
    //                           Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //                       })
    //                      .ToArray();
    // }
}