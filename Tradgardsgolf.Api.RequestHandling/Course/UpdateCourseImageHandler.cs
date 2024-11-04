using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using Tradgardsgolf.Api.ResponseFactory;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Core.Auth;
using Tradgardsgolf.Core.Exceptions;
using Tradgardsgolf.Core.Infrastructure;
using Tradgardsgolf.Core.Specifications;

namespace Tradgardsgolf.Api.RequestHandling.Course;

public class UpdateCourseImageHandler(
    IRepository repository,
    IAuthenticationService authenticationService,
    IFileService files,
    IResponseFactory<CourseResponse, Core.Entities.Course> courseResponseFactory)
    : IRequestHandler<UpdateCourseImageCommand, CourseResponse>
{
    public async Task<CourseResponse> Handle(UpdateCourseImageCommand request, CancellationToken cancellationToken)
    {
        var user = authenticationService.RequireAuthenticatedUser();
        var course = await repository.FirstOrDefaultAsync(Specs.ById<Core.Entities.Course>(request.Id), cancellationToken);

        if (user.UserId != course.OwnerGuid)
            throw new ForbiddenException();

        var bytes = CompressImage(Convert.FromBase64String(request.ImageBase64));
        var filename = $"{course.Id}_{DateTime.Now.Ticks}{request.Extension}";

        await files.Save(filename, bytes);

        if (!string.IsNullOrEmpty(course.Image))
            await files.Delete(course.Image);

        course.Image = filename;

        await repository.UpdateAsync(course, cancellationToken);

        return courseResponseFactory.Create(course);
    }

    private byte[] CompressImage(byte[] bytes)
    {
        using var image = Image.Load(bytes);

        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Size = new Size(686, 360),
            Mode = ResizeMode.Max
        }));

        using var outputStream = new MemoryStream();
        image.Save(outputStream, new JpegEncoder { Quality = 80 });
        return outputStream.ToArray();
    }
}