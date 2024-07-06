
using CUSTOMSOFT.CORE.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.APLICATION.Queries.File
{
    public class GetFileByPackageIdQuery : IRequest<FileDto>
    {
        public int PackageId { get; set; }
        
    }
}
