using AppSettings.Application;
using AppSettings.Domain.Contracts;
using AppSettings.Infra.Contracts;
using AppSettings.Infra.Repositories;
using AppSettingsExercise.configs;
using AppSettingsExercise.Services;

namespace AppSettingsExercise.Extensions;

public static class AppExtensions
{
    public static void Init(this IHostApplicationBuilder hostBuilder)
    {
        var services = hostBuilder.Services;
        var configBuilder = hostBuilder.Configuration;
        var environment = hostBuilder.Environment;

        services.ConfigureAppSettings(configBuilder, environment);

        // 추후 다른 의존성 설정이나 초기화 코드 추가.
        services.SetMediator();
        services.SetRepositories();
    }

    public static void ConfigureAppSettings(this IServiceCollection services, IConfigurationBuilder configBuilder, IHostEnvironment hostEnv)
    {

        var configPath = Path.Combine(hostEnv.ContentRootPath, "configs");
        configBuilder.AddJsonFile(Path.Combine(configPath, "appsettings.json"), optional: false, reloadOnChange: true)
                      .AddJsonFile(Path.Combine(configPath, $"appsettings.{hostEnv.EnvironmentName}.json"), optional: true, reloadOnChange: true);
        configBuilder.AddJsonFile(Path.Combine(configPath, "reposettings.json"), optional: false, reloadOnChange: true)
                      .AddJsonFile(Path.Combine(configPath, $"reposettings.{hostEnv.EnvironmentName}.json"), optional: true, reloadOnChange: true);
        var config = configBuilder.Build();

        services.Configure<AppSetting>(config.GetSection(nameof(AppSetting)));
        services.Configure<RepoSetting>(config.GetSection(nameof(RepoSetting)));

        // IOptionsMonitor 로 사용하면 singleton 으로 등록
        services.AddSingleton<IAppSettingVault, AppSettingVault>();
        services.AddSingleton<IRepoSettingVault, RepoSettingVault>();
        services.AddOptions();
    }

    public static void SetMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AppSettingsApplication).Assembly));
    }

    public static void SetRepositories(this IServiceCollection services)
    {
        services.AddTransient<IWeatherRepository, WeatherRepository>();
    }
}
