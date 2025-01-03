﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Api.ResponseFactory;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Core.Auth;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;

namespace Tradgardsgolf.Api.RequestHandling.Course;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class ClaimOwnershipHandler(
    IRepository repository,
    IResponseFactory<CourseResponse, Core.Entities.Course> courseResponseFactory,
    IAuthenticationService authenticationService)
    : IRequestHandler<ClaimOwnershipCommand, CourseResponse>
{
    public async Task<CourseResponse> Handle(ClaimOwnershipCommand request, CancellationToken cancellationToken)
    {
        var user = authenticationService.RequireAuthenticatedUser();
        var course = await repository.FirstOrDefaultAsync(Specs.ById<Core.Entities.Course>(request.Id), cancellationToken);

        if (course.OwnerGuid != Guid.Empty)
            return courseResponseFactory.Create(course);

        course.OwnerGuid = user.UserId;

        await repository.UpdateAsync(course, cancellationToken);

        return courseResponseFactory.Create(course);
    }
}