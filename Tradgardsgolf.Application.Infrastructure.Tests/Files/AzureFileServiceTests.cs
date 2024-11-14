using System.Text;
using Azure;
using Microsoft.Extensions.Options;
using Moq;
using Tradgardsgolf.Infrastructure.Files;

namespace Tradgardsgolf.Application.Infrastructure.Tests.Files;

[Ignore("Integration test")]
[TestFixture]
public class AzureFileServiceTests
{
    private readonly AzureStorageOptions _azureStorageOptions = new()
    {
        ConnectionString = "<SET CONNECTION STRING>",
        Container = "<SET CONTAINER NAME>"
    };

    private IOptionsMonitor<AzureStorageOptions> Options()
    {
        var mock = new Mock<IOptionsMonitor<AzureStorageOptions>>();
        mock.SetupGet(x => x.CurrentValue).Returns(_azureStorageOptions);
        return mock.Object;
    }


    [Test]
    public async Task UploadDownloadDelete()
    {
        const string testData = "Hello world!";
        var bytes = Encoding.UTF8.GetBytes(testData);
        const string fileName = "test.txt";

        var fileService = new AzureFileService(Options());
        await fileService.Save(fileName, bytes);
        var fileBytes = await fileService.Get(fileName);
        var text = Encoding.UTF8.GetString(fileBytes);

        await fileService.Delete(fileName);

        Assert.That(text, Is.EqualTo(testData));
    }

    [Test]
    public void FileNotFound()
    {
        var fileService = new AzureFileService(Options());
        var fileName = $"{Guid.NewGuid()}.txt";

        Assert.ThrowsAsync<RequestFailedException>(async () => { await fileService.Get(fileName); });
    }
}