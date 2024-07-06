
using CUSTOMSOFT.CORE.DTO;
using CUSTOMSOFT.INFRAESTRUCTURE.Data;
using CUSTOMSOFT.INFRAESTRUCTURE.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.APLICATION.Queries.File
{
    public class GetFileByPackageIdQueryHandler : IRequestHandler<GetFileByPackageIdQuery,FileDto>
    {
        private readonly IFileRepository _fileRepository;

        public GetFileByPackageIdQueryHandler(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

       

       async Task<FileDto> IRequestHandler<GetFileByPackageIdQuery, FileDto>.Handle(GetFileByPackageIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var package = await _fileRepository.GetFileByPackageId(request.PackageId);


                //return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
