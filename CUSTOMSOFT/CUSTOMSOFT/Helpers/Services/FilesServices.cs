using CUSTOMSOFT.API.Helpers.Interfaces;
using CUSTOMSOFT.API.Models;
using CUSTOMSOFT.CORE.DTO;
using CUSTOMSOFT.INFRAESTRUCTURE.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.API.Helpers.Services
{
    public class FilesServices : IFileService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FilesServices(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<FileDto> SaveFileAsync(IFormFile file, int packageId)
        {
            string uploadsDirectory = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads");
            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }
            string fileName = file.FileName;
            string fileExtension = Path.GetExtension(file.FileName);
            string filePath = Path.Combine(uploadsDirectory, fileName);
            using (FileStream fileStream = File.Create(filePath))
            {
                await file.CopyToAsync(fileStream);
            }

            return FileModelExtensions.ToDTO(fileName, fileExtension, filePath, packageId);
        }
    }
}
