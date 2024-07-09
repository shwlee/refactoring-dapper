namespace AppSettingsExercise.Exceptions;


[Serializable]
public class AppSettingNotSetException : Exception
{
    public string? NotSetField { get; }

    public AppSettingNotSetException(string notSetField) : this(notSetField, null, null)
    {
    }

    public AppSettingNotSetException(string? notSetField = null, string? message = null, Exception? inner = null) : base(message, inner) 
        => NotSetField = notSetField;
}
