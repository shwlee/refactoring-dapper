namespace AppSettings.Domain.Contracts;

public interface IAppSettingVault
{
    string GetAdminAuth();
    int GetAccessRetryCount();
    (string storageHost, string storageToken) GetStorageSet();
}
