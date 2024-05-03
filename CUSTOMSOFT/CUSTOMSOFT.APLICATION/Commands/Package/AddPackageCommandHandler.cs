
using CUSTOMSOFT.APLICATION.Commands.Autentication;
using CUSTOMSOFT.CORE.DTO;
using CUSTOMSOFT.INFRAESTRUCTURE.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.APLICATION.Commands.Package
{
    public class AddPackageCommandHandler : IRequestHandler<AddPackageCommand,PackageDTO>
    {
        private readonly IPackageRepository _packageRepository;

        public AddPackageCommandHandler(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }
        public async Task<PackageDTO> Handle(AddPackageCommand request, CancellationToken cancellationToken)
        {
            var requestDto = new PackageDTO
            {
                TrackingNumber = Guid.NewGuid().ToString("N") + "-" + DateTime.Now.ToString("yyy-mm-dd"),
                CustomerName = request.CustomerName,
                DeliveryAddress = request.DeliveryAddress,
                Weight = request.Weight
            };
            var response = await _packageRepository.AddPackage(requestDto);

            return requestDto;
        }
    }

   
}
