using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.Extensions;
using EmbeddedResourceHelper;
using FluentAssertions;
using Moq;
using Tradgardsgolf.Api.RequestHandling.Course;
using Tradgardsgolf.Core.Auth;
using Tradgardsgolf.Core.Exceptions;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;

namespace Tradgardsgolf.Api.RequestHandling.Tests.Course;

[TestFixture]
public class UpdateCourseImage
{
    [Test]
    public async Task ShouldThrowForbiddenWhenNotTheOwner()
    {
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());

        var arrange = Arrange.Dependencies<UpdateCourseImageHandler, UpdateCourseImageHandler>(dependencies =>
        {
            dependencies.UseMock<IAuthenticationService>(mock => mock.Setup(x => x.RequireAuthenticatedUser()).Returns(
            new AuthenticatedUser
            {
                UserId = Guid.NewGuid()
            }));

            dependencies.UseMock<IRepository>(mock =>
            {
                mock.Setup(x => x.FirstOrDefaultAsync(Specs.ById<Core.Entities.Course>(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(course);
            });
        });

        var handler = arrange.Resolve<UpdateCourseImageHandler>();
        var command = new Contracts.Course.UpdateCourseImageCommand
        {
            Id = course.Id,
            Extension = ".png",
            ImageBase64 = "000"
        };

        await handler.Invoking(h => h.Handle(command, CancellationToken.None))
            .Should()
            .ThrowAsync<ForbiddenException>();
    }

    [Test]
    public async Task ShouldSaveFileWhenOwnerOfCourse()
    {
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var fileSpy = default(Mock<IFileService>);
        var updatedCourses = new List<Core.Entities.Course>();
        var savedFiles = new List<string>();

        var arrange = Arrange.Dependencies<UpdateCourseImageHandler, UpdateCourseImageHandler>(dependencies =>
        {
            dependencies.UseMock<IAuthenticationService>(mock => mock.Setup(x => x.RequireAuthenticatedUser()).Returns(
            new AuthenticatedUser
            {
                UserId = course.OwnerGuid
            }));

            dependencies.UseMock<IRepository>(mock =>
            {
                mock.Setup(x => x.FirstOrDefaultAsync(Specs.ById<Core.Entities.Course>(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(course);

                mock.Setup(x => x.UpdateAsync(It.IsAny<Core.Entities.Course>(), It.IsAny<CancellationToken>()))
                    .Callback((Core.Entities.Course c, CancellationToken _) => updatedCourses.Add(c));
            });

            dependencies.UseMock(mock =>
            {
                mock.Setup(x => x.Save(It.IsAny<string>(), It.IsAny<byte[]>()))
                    .Callback((string filename, byte[] _) => savedFiles.Add(filename));
            }, out fileSpy);
        });

        var handler = arrange.Resolve<UpdateCourseImageHandler>();
        var fileBytes = EmbeddedResource.GetAsByteArray(GetType().Assembly, "_Data/grass.jpg");
        var command = new Contracts.Course.UpdateCourseImageCommand
        {
            Id = course.Id,
            Extension = ".png",
            ImageBase64 = Convert.ToBase64String(fileBytes)
        };

        await handler.Handle(command, CancellationToken.None);

        fileSpy!.Verify(
        x => x.Save(It.Is<string>(s => s.StartsWith(course.Id.ToString()) && s.EndsWith(command.Extension)), It.IsAny<byte[]>()),
        Times.Once);
        updatedCourses.Should().HaveCount(1);
        updatedCourses.Should().Contain(x => x.Id == course.Id);
        savedFiles.Should().Contain(x => x == updatedCourses.First().Image);
    }


    [Test]
    public async Task ShouldRemovePreviousFile()
    {
        const string previousFile = "previosfile.png";
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p =>
        {
            p.Id = Guid.NewGuid();
            p.Image = previousFile;
        });
        var fileSpy = default(Mock<IFileService>);

        var arrange = Arrange.Dependencies<UpdateCourseImageHandler, UpdateCourseImageHandler>(dependencies =>
        {
            dependencies.UseMock<IAuthenticationService>(mock => mock.Setup(x => x.RequireAuthenticatedUser()).Returns(
            new AuthenticatedUser
            {
                UserId = course.OwnerGuid
            }));

            dependencies.UseMock<IRepository>(mock =>
            {
                mock.Setup(x => x.FirstOrDefaultAsync(Specs.ById<Core.Entities.Course>(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(course);
            });

            dependencies.UseMock(out fileSpy);
        });

        var handler = arrange.Resolve<UpdateCourseImageHandler>();
        var fileBytes = EmbeddedResource.GetAsByteArray(GetType().Assembly, "_Data/grass.jpg");
        var command = new Contracts.Course.UpdateCourseImageCommand
        {
            Id = course.Id,
            Extension = ".png",
            ImageBase64 = Convert.ToBase64String(fileBytes)
        };

        await handler.Handle(command, CancellationToken.None);

        fileSpy!.Verify(x => x.Delete(previousFile), Times.Once);
    }
}