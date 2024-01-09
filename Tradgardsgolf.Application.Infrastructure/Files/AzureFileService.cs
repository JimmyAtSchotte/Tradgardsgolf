using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Infrastructure.Files;

public class AzureFileService : IFileService
{
    private readonly BlobServiceClient _storage;
    private readonly string _containerName;


    public AzureFileService(IOptionsMonitor<AzureStorageOptions> azureStorageOptions)
    {
        _containerName = azureStorageOptions.CurrentValue.Container;
        _storage = new BlobServiceClient(azureStorageOptions.CurrentValue.ConnectionString);
    }
    
    public async Task<byte[]> Get(string fileName)
    {
        var container =  _storage.GetBlobContainerClient(_containerName);
        var blob = container.GetBlobClient(fileName);
        var download = await blob.DownloadAsync();
        
        using var memoryStream = new MemoryStream();
        await download.Value.Content.CopyToAsync(memoryStream);
        var bytes = memoryStream.ToArray();
        memoryStream.Close();

        return memoryStream.ToArray();
    }

    public async Task Save(string filename, byte[] bytes)
    {
        var container =  _storage.GetBlobContainerClient(_containerName);
        var blob = container.GetBlobClient(filename);

        if (await blob.ExistsAsync())
            await blob.DeleteAsync();

        using var memoryStream = new MemoryStream(bytes);
        await blob.UploadAsync(memoryStream);
        memoryStream.Close();
    }

    public async Task Delete(string filename)
    {
        var container =  _storage.GetBlobContainerClient(_containerName);
        var blob = container.GetBlobClient(filename);
        
        if (await blob.ExistsAsync())
            await blob.DeleteAsync();
    }
}