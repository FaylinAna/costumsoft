using CUSTOMSOFT.CORE.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.APLICATION.Queries.Package
{
    public class GetAllPackagesQuery : IRequest<List<PackageDTO>>
    {
    }
}
