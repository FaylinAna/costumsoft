using CUSTOMSOFT.CORE.DTO;
using CUSTOMSOFT.INFRAESTRUCTURE.Data;
using CUSTOMSOFT.INFRAESTRUCTURE.Extensions;
using CUSTOMSOFT.INFRAESTRUCTURE.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.APLICATION.Commands.Package
{
    public class UpdatePackageCommandHandler : IRequestHandler<UpdatePackageCommand, PackageDTO>
    {

        private readonly IPackageRepository _packageRepository;

        public UpdatePackageCommandHandler(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task<PackageDTO> Handle(UpdatePackageCommand request, CancellationToken cancellationToken)
        {
            var requestDto = new PackageDTO
            {
                Id = request.PackageId,
                StateDescription = request.StateDescription,
                CustomerName = request.CustomerName,
                DeliveryAddress = request.DeliveryAddress,
                Weight = request.Weight
            };
            var response = await _packageRepository.UpdatePackage(requestDto);
            var responseDTO = PackageExtensions.ToDTO(response);
            return responseDTO;

        }
    }
}
