namespace Tradgardsgolf.Contracts.Settings;

public record SettingResponse<T>
{
    public T Value { get; set; }
}