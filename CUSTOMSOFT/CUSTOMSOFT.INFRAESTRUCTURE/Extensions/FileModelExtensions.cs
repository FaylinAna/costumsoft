using CUSTOMSOFT.CORE.DTO;
using CUSTOMSOFT.INFRAESTRUCTURE.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.INFRAESTRUCTURE.Extensions
{
    public static class FileModelExtensions
    {
        public static FileDto ToDTO(string fileName,string fileType,string path,int packageId)
        {
            var fileDto = new FileDto
            {
                FileName = fileName,
                FileType = fileType,
                FilePath = path,
                PackageId = packageId
            };

            return fileDto;
        }

    }
}
