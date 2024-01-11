using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Api.ResponseFactory;
using Tradgardsgolf.Contracts;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Api.RequestHandling.Course
{
    public class ChangeCourseImageHandler(IRepository<Core.Entities.Course> repository, 
        IFileService fileService, 
        IResponseFactory<CourseResponse, Core.Entities.Course> courseResponseFactory) 
        : IRequestHandler<ChangeCourseImage, CourseResponse>
    {
        public async Task<CourseResponse> Handle(ChangeCourseImage request, CancellationToken cancellationToken)
        {
            var course = await repository.GetByIdAsync(request.Id, cancellationToken);
            var bytes = Convert.FromBase64String(request.ImageBase64);
            var filename = $"{course.Id}_{DateTime.Now.Ticks}{request.Extension}";
            
            await fileService.Save(filename, bytes);

            if (!string.IsNullOrEmpty(course.Image))
                await fileService.Delete(course.Image);

            course.Image = filename;
            
            await repository.UpdateAsync(course, cancellationToken);

            return courseResponseFactory.Create(course);
        }
    }
}

