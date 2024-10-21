namespace Tradgardsgolf.Contracts.Types;

public class ImageReference
{
    public string Url { get; set; }
    public string Path { get; set; }

    public override string ToString()
    {
        return $"{Url}{Path}";
    }
}