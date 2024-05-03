
using CUSTOMSOFT.CORE.DTO;
using CUSTOMSOFT.INFRAESTRUCTURE.Data;
using CUSTOMSOFT.INFRAESTRUCTURE.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.APLICATION.Queries.Package
{
    public class GetPackageByTrackingNumberQueryHandler : IRequestHandler<GetPackageByTrackingNumberQuery, PackageDTO>
    {
        private readonly IPackageRepository _packageRepository;

        public GetPackageByTrackingNumberQueryHandler(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task<PackageDTO?> Handle(GetPackageByTrackingNumberQuery request, CancellationToken cancellationToken)
        {
            var package = await _packageRepository.GetByTrackingNumberAsync(request.TrackingNumber);

            var response = PackageExtensions.ToDTO(package);
            return response;
        }

        
    }
}
