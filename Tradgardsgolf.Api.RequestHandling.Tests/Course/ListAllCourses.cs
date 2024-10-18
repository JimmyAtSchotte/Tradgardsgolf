using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.Extensions;
using FluentAssertions;
using Moq;
using Tradgardsgolf.Api.RequestHandling.Course;
using Tradgardsgolf.Api.ResponseFactory;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Contracts.Types;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications.Course;

namespace Tradgardsgolf.Api.RequestHandling.Tests.Course;

[TestFixture]
public class ListAllCourses
{
    [Test]
    public async Task ShouldListAllCourses()
    {
        var courses = new List<Core.Entities.Course>()
        {
            Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid()),
            Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid())
        };
        
        
        var arrange = Arrange.Dependencies<ListAllCoursesHandler, ListAllCoursesHandler>(dependencies =>
        {
            dependencies.UseMock<IRepository<Core.Entities.Course>>(mock =>
            {
                mock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(courses);
            });

            dependencies.UseImplementation<IResponseFactory<CourseResponse, Core.Entities.Course>, CourseResponseFactory>();
            dependencies.UseImplementation<IResponseFactory<ImageReference, Core.Entities.Course>, ImageReferenceResponseFactory>();
        });

        var handler = arrange.Resolve<ListAllCoursesHandler>();
        var command = new ListAllCoursesCommand();
        
        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().HaveCount(courses.Count);
    }
}