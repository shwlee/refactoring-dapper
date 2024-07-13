using AppSettings.Domain.Contracts;
using AppSettingsExercise.configs;
using AppSettingsExercise.Exceptions;
using Microsoft.Extensions.Options;

namespace AppSettingsExercise.Services;

public class AppSettingVault : IAppSettingVault
{
    private readonly IConfiguration _config;
    private readonly IOptionsMonitor<AppSetting> _appSettingsMonitor;

    private AppSetting? _appSetting;

    // IOptions<T> 만 사용하면 아래만 사용해도 문제없다.
    //public AppSettingVault(IConfiguration config, IOptions<AppSetting> options)
    //{
    //    _config = config;
    //    _appSetting = options?.Value ?? throw new ArgumentNullException(nameof(options));
    //}

    public AppSettingVault(IConfiguration config, IOptionsMonitor<AppSetting> optionsMonitor)
    {
        _config = config;
        _appSettingsMonitor = optionsMonitor;
        _appSetting = _appSettingsMonitor.CurrentValue;
        _appSettingsMonitor.OnChange(appSetting =>
        {
            Console.WriteLine("appsettings.json reloaded.");
            _appSetting = appSetting;
        });
    }

    public int GetAccessRetryCount()
        => _appSetting?.AccessRetryCount ?? throw new AppSettingNotSetException(nameof(AppSetting.AccessRetryCount));

    public string GetAdminAuth()
        => _appSetting?.AdminAuth ?? throw new AppSettingNotSetException(nameof(AppSetting.AdminAuth));

    public (string storageHost, string storageToken) GetStorageSet()
    {
        if (_appSetting?.Storage is null)
        {
            throw new AppSettingNotSetException(nameof(AppSetting.AdminAuth));
        }

        var host = _appSetting.Storage.Host ?? throw new AppSettingNotSetException(nameof(StorageSet.Host));
        var token = _appSetting.Storage.Token ?? throw new AppSettingNotSetException(nameof(StorageSet.Token));
        return (host, token);
    }
}