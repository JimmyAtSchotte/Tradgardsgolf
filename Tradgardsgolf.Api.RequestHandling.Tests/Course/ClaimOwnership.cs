﻿using Ardalis.Specification;
using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.Extensions;
using FluentAssertions;
using Moq;
using Tradgardsgolf.Api.ResponseFactory;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Contracts.Types;
using Tradgardsgolf.Core.Auth;
using Tradgardsgolf.Core.Infrastructure;
using SUT = Tradgardsgolf.Api.RequestHandling.Course.ClaimOwnershipHandler;

namespace Tradgardsgolf.Api.RequestHandling.Tests.Course;

[TestFixture]
public class ClaimOwnership
{
    [Test]
    public async Task ChangeOwner()
    {
        var course = Core.Entities.Course.Create(Guid.Empty, p => p.Id = Guid.NewGuid());
        var authenticatedUser = Guid.NewGuid();
        var updatedCourses = new List<Core.Entities.Course>();

        var arrange = Arrange.Dependencies<SUT, SUT>(dependencies =>
        {
            dependencies.UseMock<IRepository>(mock =>
            {
                mock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Core.Entities.Course>>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(course);

                mock.Setup(x => x.UpdateAsync(It.IsAny<Core.Entities.Course>(), It.IsAny<CancellationToken>()))
                    .Callback((Core.Entities.Course c, CancellationToken _) => updatedCourses.Add(c));
            });

            dependencies
                .UseImplementation<IResponseFactory<CourseResponse, Core.Entities.Course>, CourseResponseFactory>();
            dependencies
                .UseImplementation<IResponseFactory<ImageReference?, Core.Entities.Course>,
                    ImageReferenceResponseFactory>();
            dependencies.UseMock<IAuthenticationService>(mock => mock.Setup(x => x.RequireAuthenticatedUser()).Returns(
            new AuthenticatedUser
            {
                UserId = authenticatedUser
            }));
        });

        var handler = arrange.Resolve<SUT>();
        var command = new ClaimOwnershipCommand
        {
            Id = course.Id
        };

        var result = await handler.Handle(command, CancellationToken.None);

        result.OwnerGuid.Should().Be(authenticatedUser);
        updatedCourses.Should().HaveCount(1);
        updatedCourses.First().OwnerGuid.Should().Be(authenticatedUser);
    }

    [Test]
    public async Task HasOwnerSinceBefore()
    {
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        var authenticatedUser = Guid.NewGuid();

        var arrange = Arrange.Dependencies<SUT, SUT>(dependencies =>
        {
            dependencies.UseMock<IRepository>(mock =>
            {
                mock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<Core.Entities.Course>>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(course);
            });
            dependencies
                .UseImplementation<IResponseFactory<CourseResponse, Core.Entities.Course>, CourseResponseFactory>();
            dependencies
                .UseImplementation<IResponseFactory<ImageReference?, Core.Entities.Course>,
                    ImageReferenceResponseFactory>();

            dependencies.UseMock<IAuthenticationService>(mock => mock.Setup(x => x.RequireAuthenticatedUser()).Returns(
            new AuthenticatedUser
            {
                UserId = authenticatedUser
            }));
        });

        var handler = arrange.Resolve<SUT>();
        var command = new ClaimOwnershipCommand
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
}