using System.Collections.Generic;
using System.IO;
using System.Linq;
using HalogenTest.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.FileIO;

namespace HalogenTest.Services
{
    public class UploadService : IUploadService
    {
        private readonly string _filePath;

        public UploadService(IConfiguration configuration){
            var folderName = Path.Combine("Resources", "Images");
            var savedPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            _filePath = Path.Combine(savedPath, configuration["FileName"]);
        }

        public string GetFilePath() => _filePath;

        public List<int> Read(){
            if (!File.Exists(_filePath)) return null;

            var reader = new TextFieldParser(_filePath){
                TextFieldType = FieldType.Delimited,
            };

            reader.SetDelimiters(",");
            var entries = System.Array.Empty<string>();
            while (!reader.EndOfData){
                var line = reader.ReadFields();
                if (line != null) entries = entries.Concat(line).ToArray();
            }

            var numbers = entries.Skip(1).Select(int.Parse)
                .ToArray()
                .OrderBy(entry => entry >= 0).ToList();
            return numbers;
        }

        public bool Upload(IFormFile formFile){
            if (formFile.Length <= 0) return false;
            using var stream = new FileStream(_filePath, FileMode.Create);
            formFile.CopyTo(stream);
            return true;
        }
    }
}