namespace AppSettingsExercise.Exceptions;


[Serializable]
public class RepoSettingNotSetException : Exception
{
    public string? NotSetField { get; }
    public RepoSettingNotSetException(string notSetField) : this(notSetField, null, null)
    {
    }

    public RepoSettingNotSetException(string? notSetField = null, string? message = null, Exception? inner = null)
        : base(message, inner)
        => NotSetField = notSetField;
}
