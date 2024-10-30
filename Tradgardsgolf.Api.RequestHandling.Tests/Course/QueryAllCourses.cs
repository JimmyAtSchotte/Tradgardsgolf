using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.Extensions;
using FluentAssertions;
using Moq;
using Tradgardsgolf.Api.RequestHandling.Course;
using Tradgardsgolf.Api.ResponseFactory;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Contracts.Types;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Api.RequestHandling.Tests.Course;

[TestFixture]
public class QueryAllCourses
{
    [Test]
    public async Task ShouldListAllCourses()
    {
        var courses = new List<Core.Entities.Course>()
        {
            Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid()),
            Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid())
        };
        
        
        var arrange = Arrange.Dependencies<QueryAllCoursesHandler, QueryAllCoursesHandler>(dependencies =>
        {
            dependencies.UseMock<IRepository>(mock =>
            {
                mock.Setup(x => x.ListAsync<Core.Entities.Course>(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(courses);
            });

            dependencies.UseImplementation<IResponseFactory<CourseResponse, Core.Entities.Course>, CourseResponseFactory>();
            dependencies.UseImplementation<IResponseFactory<ImageReference, Core.Entities.Course>, ImageReferenceResponseFactory>();
        });

        var handler = arrange.Resolve<QueryAllCoursesHandler>();
        var command = new Contracts.Course.QueryAllCourses();
        
        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().HaveCount(courses.Count);
    }
}