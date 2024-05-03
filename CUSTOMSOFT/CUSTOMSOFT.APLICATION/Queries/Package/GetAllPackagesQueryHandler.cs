using CUSTOMSOFT.CORE.DTO;
using CUSTOMSOFT.INFRAESTRUCTURE.Data;
using CUSTOMSOFT.INFRAESTRUCTURE.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.APLICATION.Queries.Package
{
    public class GetAllPackagesQueryHandler : IRequestHandler<GetAllPackagesQuery, List<PackageDTO>>
    {
        private readonly IPackageRepository _packageRepository;

        public GetAllPackagesQueryHandler(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task<List<PackageDTO>> Handle(GetAllPackagesQuery request, CancellationToken cancellationToken)
        {
            var packages = await _packageRepository.GetAllPackages();
            var packageDTOs = packages.Select(PackageExtensions.ToDTO).ToList();
            return packageDTOs;
        }
     }
    }
