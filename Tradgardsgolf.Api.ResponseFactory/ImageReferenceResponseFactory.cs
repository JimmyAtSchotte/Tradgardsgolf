using System.Diagnostics.CodeAnalysis;
using Tradgardsgolf.Contracts.Types;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Api.ResponseFactory;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class ImageReferenceResponseFactory : IResponseFactory<ImageReference?, Course>
{
    public ImageReference? Create(Course course)
    {
        if (string.IsNullOrEmpty(course.Image))
            return null;

        return new ImageReference
        {
            Path = course.Image
        };
    }
}