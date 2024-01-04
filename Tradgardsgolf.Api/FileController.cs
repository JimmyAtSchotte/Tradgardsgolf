using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    
    [HttpPost]
    public async Task Upload(List<IFormFile> files)
    {
        foreach (var formFile in files.Where(formFile => formFile.Length > 0))
        {
            using var memory = new MemoryStream();
            await formFile.CopyToAsync(memory);
            var bytes = memory.ToArray();
            await fileService.Save(formFile.FileName, bytes);
        }
    }
    
    [HttpDelete]
    [Route("{filename}")]
    public async Task Delete([FromRoute] string filename)
    {
        await fileService.Delete(filename);
    }
}