using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace HalogenTest.Interfaces
{
    public interface IUploadService
    {
        List<int> Read();

        bool Upload(IFormFile formFile);
    }
}