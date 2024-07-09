using AppSettings.Domain.Domains.Weathers.DTOs;

namespace AppSettings.Infra.Contracts;

public interface IWeatherRepository
{
    Task<WeatherForecast> GetWeatherForecast(CancellationToken cancellation);
}
