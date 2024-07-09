using AppSettings.Domain.Contracts;
using AppSettings.Domain.Domains.Weathers.DTOs;
using AppSettings.Domain.Domains.Weathers.Queries;
using AppSettings.Infra.Contracts;
using MediatR;

namespace AppSettings.Application.Handlers.Weathers.Queries;
public class GetWeatherHandler(IAppSettingVault appSettingVault, IWeatherRepository repository) : IRequestHandler<GetWeatherForecastQuery, WeatherForecast>
{
    private readonly IAppSettingVault _appSettingVault = appSettingVault;
    private readonly IWeatherRepository _repository = repository;

    public async Task<WeatherForecast> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetWeatherForecast(cancellationToken);
        return result;
    }
}
