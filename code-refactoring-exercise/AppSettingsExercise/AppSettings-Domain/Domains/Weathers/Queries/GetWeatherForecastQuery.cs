using AppSettings.Domain.Domains.Weathers.DTOs;
using MediatR;

namespace AppSettings.Domain.Domains.Weathers.Queries;
public record GetWeatherForecastQuery() : IRequest<WeatherForecast>;
