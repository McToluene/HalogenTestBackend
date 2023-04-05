using System.IO;
using System.Threading.Tasks;
using HalogenTest.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace HalogenTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NumberController : ControllerBase
    {
        private readonly INumberResolver _numberResolver;
        private readonly IUploadService _uploadService;

        public NumberController(INumberResolver numberResolver, IUploadService uploadService){
            _numberResolver = numberResolver;
            _uploadService = uploadService;
        }
        
        [HttpGet, DisableRequestSizeLimit]
        [Route("download")]
        public async Task<IActionResult> Download(){
            var filePath = _uploadService.GetFilePath();
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var memory = new MemoryStream();
            await using (var stream = new FileStream(filePath, FileMode.Open))
                await stream.CopyToAsync(memory);
            memory.Position = 0;
            return File(memory, GetContentType(filePath), filePath);
        }

        [HttpPost("upload"), DisableRequestSizeLimit]
        public IActionResult Upload(){
            var file = Request.Form.Files[0];
            var result = _uploadService.Upload(file);
            if (!result)
                return BadRequest("Failed to upload file");
            return Ok();
        }

        [HttpGet("process")]
        public IActionResult Process(){
            var results = _numberResolver.Process();
            if (results == null)
                return NotFound("File not found!");
            return Ok(results);
        }
        
        private static string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out var contentType))
                contentType = "application/octet-stream";
            return contentType;
        }
        
    }
}