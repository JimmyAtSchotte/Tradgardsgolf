using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Cqrs.Tests.Pipelines.Responses;


public class CourseResponseFactory : IHandlerFactory<CourseResponse>
{
    public bool AppliesTo(ICommand command, HandlerResult previousResult)
    {
        return previousResult.IsOfType<Course>();
    }

    public IHandler<CourseResponse> Create(ICommand command, HandlerResult handlerResult)
    {
        var entity = handlerResult.GetValue<Course>();
        
        return new CourseResponseHandler(entity);
    }
}

public class CourseResponseHandler : IHandler<CourseResponse>
{
    private readonly Course _course;
    public CourseResponseHandler(Course course)
    {
        _course = course;
    }

    public CourseResponse Handle()
    {
        return new CourseResponse()
        {
            Id = _course.Id,
            Longitude = _course.Longitude,
            Latitude = _course.Latitude,
        };
    }
}