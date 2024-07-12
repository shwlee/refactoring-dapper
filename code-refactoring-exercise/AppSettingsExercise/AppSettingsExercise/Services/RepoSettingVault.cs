using AppSettings.Infra.Contracts;
using AppSettingsExercise.configs;
using AppSettingsExercise.Exceptions;
using Microsoft.Extensions.Options;

namespace AppSettingsExercise.Services;

public class RepoSettingVault : IRepoSettingVault
{
    private readonly IOptionsMonitor<RepoSetting> _repoSettingsMonitor;

    private RepoSetting? _repoSetting;

    public RepoSettingVault(IOptionsMonitor<RepoSetting> optionsMonitor)
    {
        _repoSettingsMonitor = optionsMonitor;
        _repoSetting = _repoSettingsMonitor.CurrentValue;
        _repoSettingsMonitor.OnChange(appSetting =>
        {
            _repoSetting = appSetting;
        });
    }

    public string GetConnectionSting()
        => _repoSetting?.ConnectionString ?? throw new RepoSettingNotSetException(nameof(RepoSetting.ConnectionString));

    public int GetMaxPoolSize()
        => _repoSetting?.MaxPoolSize ?? throw new RepoSettingNotSetException(nameof(RepoSetting.MaxPoolSize));
}
