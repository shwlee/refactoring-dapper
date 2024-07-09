using Microsoft.AspNetCore.Mvc;

namespace RefactoringExcercise_DapperTest.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration config)
    {
        _logger = logger;
        _configuration = config;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> GetOver30Temp()
    {
        var result = DapperUtil.GetOver30CWheathers(_configuration).Result;
        return result;
    }
}
