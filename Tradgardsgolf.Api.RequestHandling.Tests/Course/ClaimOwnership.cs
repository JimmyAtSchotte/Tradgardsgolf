using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.Extensions;
using Moq;
using Tradgardsgolf.Api.ResponseFactory;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Contracts.Types;
using Tradgardsgolf.Core.Infrastructure;
using SUT = Tradgardsgolf.Api.RequestHandling.Course.ClaimOwnershipHandler;

namespace Tradgardsgolf.Api.RequestHandling.Tests.Course;

[TestFixture]
public class ClaimOwnership
{
    [Test]
    public async Task ChangeOwner()
    {
        var course = Core.Entities.Course.Create(Guid.Empty, p => p.Id = 23);
        
        var arrange = Arrange.Dependencies<SUT, SUT>(dependencies =>
        {
            dependencies.UseMock<IRepository<Core.Entities.Course>>(mock =>
            {
                mock.Setup(x => x.GetByIdAsync(course.Id, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(course);
            });
            dependencies.UseImplementation<IResponseFactory<CourseResponse, Core.Entities.Course>, CourseResponseFactory>();
            dependencies.UseImplementation<IResponseFactory<ImageReference, Core.Entities.Course>, ImageReferenceResponseFactory>();
        });
        
        var handler = arrange.Resolve<SUT>();
        var command = new Contracts.Course.ClaimOwnership()
        {
            Id = course.Id,
            Owner = Guid.NewGuid()
        };
        
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result.OwnerGuid, Is.EqualTo(command.Owner));
    }
    
    [Test]
    public async Task HasOwnerSinceBefore()
    {
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = 23);
        
        var arrange = Arrange.Dependencies<SUT, SUT>(dependencies =>
        {
            dependencies.UseMock<IRepository<Core.Entities.Course>>(mock =>
            {
                mock.Setup(x => x.GetByIdAsync(course.Id, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(course);
            });
            dependencies.UseImplementation<IResponseFactory<CourseResponse, Core.Entities.Course>, CourseResponseFactory>();
            dependencies.UseImplementation<IResponseFactory<ImageReference, Core.Entities.Course>, ImageReferenceResponseFactory>();
        });
        
        var handler = arrange.Resolve<SUT>();
        var command = new Contracts.Course.ClaimOwnership()
        {
            Id = course.Id,
            Owner = Guid.NewGuid()
        };
        
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.OwnerGuid, Is.Not.EqualTo(command.Owner));
            Assert.That(course.OwnerGuid, Is.Not.EqualTo(command.Owner));
        });
   
    }
}