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
        var environmentName = hostBuilder.Environment.EnvironmentName;
        services.ConfigureAppSettings(configBuilder, environmentName);

        // 추후 다른 의존성 설정이나 초기화 코드 추가.
        services.SetMediator();
        services.SetRepositories();
    }

    public static void ConfigureAppSettings(this IServiceCollection services, IConfigurationBuilder configBuilder, string environmentName)
    {
        var configPath = "./configs";
        configBuilder.AddJsonFile(Path.Combine(configPath, $"appsettings.{environmentName.ToLower()}.json"), false, true);
        configBuilder.AddJsonFile(Path.Combine(configPath, $"reposettings.{environmentName.ToLower()}.json"), false, true);
        var config = configBuilder.Build();

        services.Configure<AppSetting>(config.GetSection(nameof(AppSetting)));
        services.Configure<RepoSetting>(config.GetSection(nameof(RepoSetting)));

        //// IOptions 를 사용하면 Transient 로 등록
        //services.AddTransient<IAppSettingVault, AppSettingVault>();

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
