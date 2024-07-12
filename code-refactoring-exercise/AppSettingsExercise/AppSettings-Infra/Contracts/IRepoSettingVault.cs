namespace AppSettings.Infra.Contracts;

public interface IRepoSettingVault
{
    string GetConnectionSting();
    int GetMaxPoolSize();
}
