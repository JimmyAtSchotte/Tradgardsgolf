using FluentAssertions;
using Tradgardsgolf.Contracts.Types;

namespace Tradgardsgolf.Api.Contracts.Tests.Types;

[TestFixture]
public class ImageReferenceTests
{
    [TestCase("https://localhost/", "images/test.jpg")]
    [TestCase("https://localhost", "/images/test.jpg")]   
    [TestCase("https://localhost", "images/test.jpg")]
    [TestCase("https://localhost/images", "test.jpg")]
    public void ShouldCombineToFullUrl(string url, string path)
    {
        var imageReference = new ImageReference
        {
            Url = url,
            Path = path
        };
        
        imageReference.ToString().Should().Be("https://localhost/images/test.jpg");
    }
}