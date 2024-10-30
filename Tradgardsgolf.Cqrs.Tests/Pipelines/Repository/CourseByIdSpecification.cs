using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Cqrs.Tests.Pipelines.Commands;

namespace Tradgardsgolf.Cqrs.Tests.Pipelines.Repository;

public class FetchCourseByIdFactory : IHandlerFactory<Course>
{
    public bool AppliesTo(ICommand command, HandlerResult previousResult)
    {
        return command.IsOfType<UpdateCoursePositionCommand>();
    }

    public IHandler<Course> Create(ICommand command, HandlerResult handlerResult)
    {
        var cmd = command as UpdateCoursePositionCommand;
        
        return new FetchCourseById(cmd.CourseId);
    }
}

public class FetchCourseById : IHandler<Course>
{
    private readonly Guid _courseId;

    public FetchCourseById(Guid courseId)
    {
        _courseId = courseId;
    }
    
    public Course Handle()
    {
        return Course.Create(Guid.NewGuid(), p => p.Id = _courseId);
    }
}