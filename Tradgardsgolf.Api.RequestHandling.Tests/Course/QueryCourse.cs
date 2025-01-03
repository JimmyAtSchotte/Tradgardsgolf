﻿using ArrangeDependencies.Autofac;
using ArrangeDependencies.Autofac.Extensions;
using FluentAssertions;
using Moq;
using Tradgardsgolf.Api.RequestHandling.Course;
using Tradgardsgolf.Api.ResponseFactory;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Contracts.Types;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;

namespace Tradgardsgolf.Api.RequestHandling.Tests.Course;

[TestFixture]
public class QueryCourse
{
    [Test]
    public async Task ShouldFindCourse()
    {
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid());
        
        var arrange = Arrange.Dependencies<QueryCourseHandler, QueryCourseHandler>(dependencies =>
        {
            dependencies.UseMock<IRepository>(mock =>
            {
                mock.Setup(x => x.FirstOrDefaultAsync(Specs.ById<Core.Entities.Course>(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(course);
            });

            dependencies.UseImplementation<IResponseFactory<CourseResponse, Core.Entities.Course>, CourseResponseFactory>();
            dependencies.UseImplementation<IResponseFactory<ImageReference?, Core.Entities.Course>, ImageReferenceResponseFactory>();
        });

        var handler = arrange.Resolve<QueryCourseHandler>();
        var command = new Contracts.Course.QueryCourse
        {
            Id = course.Id
        };
        
        var result = await handler.Handle(command, CancellationToken.None);

        result.Id.Should().Be(course.Id);
        result.ImageReference.Should().BeNull();
    }
    
    [Test]
    public async Task ShouldFindCourseWithImage()
    {
        var course = Core.Entities.Course.Create(Guid.NewGuid(), p =>
        {
            p.Id = Guid.NewGuid();
            p.Image = "myimage.png";
        });
        
        var arrange = Arrange.Dependencies<QueryCourseHandler, QueryCourseHandler>(dependencies =>
        {
            dependencies.UseMock<IRepository>(mock =>
            {
                mock.Setup(x => x.FirstOrDefaultAsync(Specs.ById<Core.Entities.Course>(course.Id), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(course);
            });

            dependencies.UseImplementation<IResponseFactory<CourseResponse, Core.Entities.Course>, CourseResponseFactory>();
            dependencies.UseImplementation<IResponseFactory<ImageReference?, Core.Entities.Course>, ImageReferenceResponseFactory>();
        });

        var handler = arrange.Resolve<QueryCourseHandler>();
        var command = new Contracts.Course.QueryCourse
        {
            Id = course.Id
        };
        
        var result = await handler.Handle(command, CancellationToken.None);

        result.Id.Should().Be(course.Id);
        result.ImageReference?.Path.Should().Be(course.Image);
    }
}