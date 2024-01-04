using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Infrastructure.Files;

public class AzureFileService : IFileService
{
    private BlobServiceClient _storage;
    private readonly string _containerName;

    public AzureFileService(string storageConnectionString, string containerName)
    {
        _containerName = containerName;
        _storage = new BlobServiceClient(storageConnectionString);
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