using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Api;

[ApiController]
[Route("api/file")]
public class FileController(IFileService fileService) : ControllerBase
{
    [HttpGet]
    [Route("{filename}")]
    public async Task<FileResult> Download([FromRoute] string filename)
    {
        var fileBytes = await fileService.Get(filename);
        return new FileContentResult(fileBytes, "image/jpeg");
    }
}