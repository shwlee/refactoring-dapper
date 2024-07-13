using AppSettingsExercise.configs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AppSettingsExercise.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private AppSetting _appSetting;
    private readonly IOptionsMonitor<AppSetting> _optionsMonitor;

    public TestController(IOptionsMonitor<AppSetting> optionMonitor)
    {
        _appSetting = optionMonitor.CurrentValue;
        _optionsMonitor = optionMonitor;
        _optionsMonitor.OnChange(appSetting =>
        {
            _appSetting = appSetting;
        });
    }

    [HttpGet()]
    public async Task<IActionResult> Get(CancellationToken cancellation)
    {
        return Ok();
    }
}
