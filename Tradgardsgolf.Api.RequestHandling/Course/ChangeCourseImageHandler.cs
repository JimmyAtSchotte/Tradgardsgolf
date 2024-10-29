using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Api.ResponseFactory;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Core.Auth;
using Tradgardsgolf.Core.Exceptions;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;
using Tradgardsgolf.Core.Specifications.Course;

namespace Tradgardsgolf.Api.RequestHandling.Course;

public class ChangeCourseImageHandler(
    IRepository<Core.Entities.Course> courses,
    IAuthenticationService authenticationService,
    IFileService files,
    IResponseFactory<CourseResponse, Core.Entities.Course> courseResponseFactory)
    : IRequestHandler<ChangeCourseImage, CourseResponse>
{
    public async Task<CourseResponse> Handle(ChangeCourseImage request, CancellationToken cancellationToken)
    {
        var user = authenticationService.RequireAuthenticatedUser();
        var course = await courses.FirstOrDefaultAsync(Specs.Course.ById(request.Id), cancellationToken);

        if (user.UserId != course.OwnerGuid)
            throw new ForbiddenException();

        var bytes = Convert.FromBase64String(request.ImageBase64);
        var filename = $"{course.Id}_{DateTime.Now.Ticks}{request.Extension}";

        await files.Save(filename, bytes);

        if (!string.IsNullOrEmpty(course.Image))
            await files.Delete(course.Image);

        course.Image = filename;

        await courses.UpdateAsync(course, cancellationToken);

        return courseResponseFactory.Create(course);
    }
}