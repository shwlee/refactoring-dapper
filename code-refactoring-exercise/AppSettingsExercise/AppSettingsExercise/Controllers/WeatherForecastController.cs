using AppSettings.Domain.Contracts;
using AppSettings.Domain.Domains.Weathers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppSettingsExercise.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IAppSettingVault _appSettingVault;


    public WeatherForecastController(IMediator mediator, ILogger<WeatherForecastController> logger, IAppSettingVault appSettingVault)
    {
        _mediator = mediator;
        _logger = logger;
        _appSettingVault = appSettingVault;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get(CancellationToken cancellation)
    {
        var query = new GetWeatherForecastQuery();
        var result = await _mediator.Send(query, cancellation);

        return Ok(result);
    }
}
