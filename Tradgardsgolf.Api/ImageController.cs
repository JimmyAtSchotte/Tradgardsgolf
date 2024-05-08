using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Api;

[ApiController]
[Route("images")]
public class ImageController(IFileService fileService) : ControllerBase
{
    [HttpGet]
    [Route("{filename}")]
    public async Task<FileResult> Get([FromRoute] string filename)
    {
        var fileBytes = await fileService.Get(filename);
        return new FileContentResult(fileBytes, "image/jpeg");
    }
}