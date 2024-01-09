namespace Tradgardsgolf.Infrastructure.Files;

public class AzureStorageOptions
{
    public string ConnectionString { get; set; }
    public string Container { get; set; }
    public string EndpointUrl { get; set; }
}