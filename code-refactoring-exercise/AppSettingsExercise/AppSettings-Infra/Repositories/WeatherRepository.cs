using AppSettings.Domain.Contracts;
using AppSettings.Domain.Domains.Weathers.DTOs;
using AppSettings.Infra.Contracts;

namespace AppSettings.Infra.Repositories;
public sealed class WeatherRepository(IAppSettingVault appSettingVault, IRepoSettingVault repoSettingVault) : IWeatherRepository
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    private readonly IAppSettingVault _appSettingVault = appSettingVault;
    private readonly IRepoSettingVault _repoSettingVault = repoSettingVault;

    public Task<WeatherForecast> GetWeatherForecast(CancellationToken cancellation)
        => Task.FromResult(new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        });
}
