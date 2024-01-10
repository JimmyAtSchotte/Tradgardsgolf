namespace Tradgardsgolf.Contracts;

public class ImageReference
{
    public string Url { get; set; }
    public string Path { get; set; }

    public override string ToString()
    {
        return $"{Url}{Path}";
    }

    public static ImageReference Create(string path)
    {
        if (string.IsNullOrEmpty(path))
            return null;

        return new ImageReference()
        {
            Path = path
        };
    }
}