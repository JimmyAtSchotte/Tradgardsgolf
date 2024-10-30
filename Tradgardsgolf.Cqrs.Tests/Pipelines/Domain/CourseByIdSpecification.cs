using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Cqrs.Tests.Pipelines.Commands;

namespace Tradgardsgolf.Cqrs.Tests.Pipelines.Specifications;

public class UpdateCoursePositionFactory : IHandlerFactory<Course>
{
    public bool AppliesTo(ICommand command, HandlerResult previousResult)
    {
        return command.IsOfType<UpdateCoursePositionCommand>() && previousResult.IsOfType<Course>();
    }

    public IHandler<Course> Create(ICommand command, HandlerResult handlerResult)
    {
        return new UpdateCoursePositionHandler(handlerResult.GetValue<Course>(), command as UpdateCoursePositionCommand);
    }
}

public class UpdateCoursePositionHandler : IHandler<Course>
{
    private readonly Course _course;
    private readonly UpdateCoursePositionCommand _command;

    public UpdateCoursePositionHandler(Course course, UpdateCoursePositionCommand command)
    {
        _course = course;
        _command = command;
    }
    
    public Course Handle()
    {
        _course.Longitude = _command.Longitude;
        _course.Latitude = _command.Latitude;

        return _course;
    }
}