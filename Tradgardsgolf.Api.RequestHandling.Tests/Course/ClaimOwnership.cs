using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.Extensions;
using Moq;
using Tradgardsgolf.Api.RequestHandling.Course;
using Tradgardsgolf.Api.ResponseFactory;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Contracts.Types;
using Tradgardsgolf.Core.Authentication;
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
        var authenticatedUser = Guid.NewGuid(); 
        
        var arrange = Arrange.Dependencies<SUT, SUT>(dependencies =>
        {
            dependencies.UseMock<IRepository<Core.Entities.Course>>(mock =>
            {
                mock.Setup(x => x.GetByIdAsync(course.Id, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(course); });
            
            dependencies.UseImplementation<IResponseFactory<CourseResponse, Core.Entities.Course>, CourseResponseFactory>();
            dependencies.UseImplementation<IResponseFactory<ImageReference, Core.Entities.Course>, ImageReferenceResponseFactory>();
            dependencies.UseMock<IAuthenticatedUser>(mock => mock.Setup(x => x.TryGetAuthenticatedUserId(out authenticatedUser)).Returns(true));
        });
        
        var handler = arrange.Resolve<SUT>();
        var command = new Contracts.Course.ClaimOwnership()
        {
            Id = course.Id
        };
        
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result.OwnerGuid, Is.EqualTo(authenticatedUser));
    }
    
    [Test]
    public async Task HasOwnerSinceBefore()
    {
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = 23);
        var authenticatedUser = Guid.NewGuid(); 
        
        var arrange = Arrange.Dependencies<SUT, SUT>(dependencies =>
        {
            dependencies.UseMock<IRepository<Core.Entities.Course>>(mock =>
            {
                mock.Setup(x => x.GetByIdAsync(course.Id, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(course);
            });
            dependencies.UseImplementation<IResponseFactory<CourseResponse, Core.Entities.Course>, CourseResponseFactory>();
            dependencies.UseImplementation<IResponseFactory<ImageReference, Core.Entities.Course>, ImageReferenceResponseFactory>();
            
            dependencies.UseMock<IAuthenticatedUser>(mock => mock.Setup(x => x.TryGetAuthenticatedUserId(out authenticatedUser)).Returns(true));
        });
        
        var handler = arrange.Resolve<SUT>();
        var command = new Contracts.Course.ClaimOwnership()
        {
            Id = course.Id
        };
        
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result.OwnerGuid, Is.Not.EqualTo(authenticatedUser));
            Assert.That(course.OwnerGuid, Is.Not.EqualTo(authenticatedUser));
        });
   
    }
    
    
    [Test]
    public void Unauthorized()
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
            
            dependencies.UseMock<IAuthenticatedUser>(mock => mock.Setup(x => x.TryGetAuthenticatedUserId(out It.Ref<Guid>.IsAny)).Returns(false));
        });
        
        var handler = arrange.Resolve<SUT>();
        var command = new Contracts.Course.ClaimOwnership()
        {
            Id = course.Id
        };
        
        Assert.ThrowsAsync<UnauthorizedException>(async () => await handler.Handle(command, CancellationToken.None));
    }
}