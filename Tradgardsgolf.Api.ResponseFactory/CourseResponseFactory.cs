using System.Diagnostics.CodeAnalysis;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Contracts.Types;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Api.ResponseFactory;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class CourseResponseFactory(IResponseFactory<ImageReference?, Course> imageReferenceResponseFactory)
    : IResponseFactory<CourseResponse, Course>
{
    public CourseResponse Create(Course course)
    {
        return new CourseResponse
        {
            Created = course.Created,
            Holes = course.Holes,
            Id = course.Id,
            Revision = course.Revision.GetValueOrDefault(),
            ImageReference = imageReferenceResponseFactory.Create(course),
            Latitude = course.Latitude,
            Longitude = course.Longitude,
            Name = course.Name,
            ScoreReset = course.ScoreReset,
            OwnerGuid = course.OwnerGuid
        };
    }
}