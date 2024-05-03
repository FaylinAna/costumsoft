using CUSTOMSOFT.CORE.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.APLICATION.Commands.Files
{
    public class AddFileCommand : IRequest<FileDto>
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
        public int PackageId { get; set; }

       public AddFileCommand(FileDto fileDto)
        {
            FileName = fileDto.FileName;
            FileType = fileDto.FileType;
            FilePath = fileDto.FilePath;
            PackageId = fileDto.PackageId;
        }
    }
}
