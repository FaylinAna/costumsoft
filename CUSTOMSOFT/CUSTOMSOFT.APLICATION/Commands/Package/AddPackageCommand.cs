using CUSTOMSOFT.CORE.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.APLICATION.Commands.Package
{
    public class AddPackageCommand : IRequest<PackageDTO>
    {
        public string TrackingNumber { get; set; }
        public string CustomerName { get; set; }
        public string DeliveryAddress { get; set; }
        public decimal Weight { get; set; }

        public AddPackageCommand(PackageDTO packageDTO)
        {
            TrackingNumber = packageDTO.TrackingNumber;
            CustomerName = packageDTO.CustomerName;
            DeliveryAddress = packageDTO.DeliveryAddress;
            Weight = packageDTO.Weight;
        }
    }
}
