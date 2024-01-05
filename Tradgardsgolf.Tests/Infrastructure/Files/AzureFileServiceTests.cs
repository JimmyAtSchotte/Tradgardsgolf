using System;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Tradgardsgolf.Infrastructure.Files;

namespace Tradgardsgolf.Infrastructure.Tests.Infrastructure.Files;

[Ignore("Integration test")]
[TestFixture]
public class AzureFileServiceTests
{
    private const string StorageConnectionString = "<SET CONNECTION STRING>";
    private const string ContainerName = "<SET CONTAINER NAME>";

    [Test]
    public async Task UploadDownloadDelete()
    {
        var testData = "Hello world!";
        var bytes = Encoding.UTF8.GetBytes(testData);
        var fileName = "test.txt";
        
        var fileService = new AzureFileService(StorageConnectionString, ContainerName);
        await fileService.Save(fileName, bytes);
        var fileBytes = await fileService.Get(fileName);
        var text = Encoding.UTF8.GetString(fileBytes);
        
        await fileService.Delete(fileName);
        
        Assert.That(text, Is.EqualTo(testData));
    }
    
    [Test]
    public void FileNotFound()
    {
        var fileService = new AzureFileService(StorageConnectionString, ContainerName);
        var fileName = $"{Guid.NewGuid()}.txt";

        Assert.ThrowsAsync<Azure.RequestFailedException>(async () =>
        {
            await fileService.Get(fileName);
        });
    }
}