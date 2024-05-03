using CUSTOMSOFT.CORE.DTO;
using CUSTOMSOFT.INFRAESTRUCTURE.Data;
using CUSTOMSOFT.INFRAESTRUCTURE.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.APLICATION.Commands.Files
{
    public class AddFileCommandHandler : IRequestHandler<AddFileCommand, FileDto>
    {
        private readonly IFileRepository _fileRepository;

        public AddFileCommandHandler(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<FileDto> Handle(AddFileCommand request, CancellationToken cancellationToken)
        {
            var newFileDto = new FileDto();
            newFileDto.FileName = request.FileName;
            newFileDto.FilePath = request.FilePath;
            newFileDto.FileType = request.FileType;
            newFileDto.PackageId = request.PackageId;
            
            return await _fileRepository.AddFile(newFileDto);


        }
    }
}
