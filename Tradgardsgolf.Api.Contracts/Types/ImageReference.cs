namespace Tradgardsgolf.Contracts.Types;

public class ImageReference
{
    public string Url { get; set; } = string.Empty;
    public string Path { get; init; } = string.Empty;

    public override string ToString()
    {
        var normalizedUrl = Url.TrimEnd('/');
        var normalizedPath = Path.TrimStart('/');

        return $"{normalizedUrl}/{normalizedPath}";
    }
}