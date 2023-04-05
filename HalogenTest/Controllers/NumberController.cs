using HalogenTest.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    }
}