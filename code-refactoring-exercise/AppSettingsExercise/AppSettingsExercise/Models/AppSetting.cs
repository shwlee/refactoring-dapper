namespace AppSettingsExercise.Models;

public class AppSetting
{
    public string? AdminAuth { get; set; }
    public int AccessRetryCount { get; set; }
    public StorageSet? Storage { get; set; }
}

public class StorageSet
{
    public string? Host { get; set; }
    public string? Token { get; set; }
}
