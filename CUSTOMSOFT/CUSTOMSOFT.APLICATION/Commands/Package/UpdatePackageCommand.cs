using CUSTOMSOFT.CORE.DTO;
using CUSTOMSOFT.INFRAESTRUCTURE.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.APLICATION.Commands.Package
{
    public class UpdatePackageCommand : IRequest<PackageDTO>
    {
        public int PackageId { get; set; }
        public string CustomerName { get; set; }
        public string DeliveryAddress { get; set; }
        public decimal Weight { get; set; }
        public string StateDescription { get; set; }
        public UpdatePackageCommand(PackageDTO packageDTO)
        {
            PackageId = packageDTO.Id;
            StateDescription = packageDTO.StateDescription;
            CustomerName = packageDTO.CustomerName;
            DeliveryAddress = packageDTO.DeliveryAddress;
            Weight = packageDTO.Weight;
        }
    }
}
