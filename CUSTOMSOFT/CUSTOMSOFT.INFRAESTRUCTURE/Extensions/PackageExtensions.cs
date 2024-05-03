
using CUSTOMSOFT.CORE.DTO;
using CUSTOMSOFT.INFRAESTRUCTURE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.INFRAESTRUCTURE.Extensions
{
    
        public static class PackageExtensions
        {
            public static PackageDTO ToDTO(Package package)
            {
                var packageDTO = new PackageDTO
                {
                    Id = package.IdPackage,
                    TrackingNumber = package.TrackingNumber,
                    CustomerName = package.CustomerName,
                    DeliveryAddress = package.DeliveryAddress,
                    StateDate = package.StateDatePackages,
                    Weight = package.Weight,
                    States = new List<PackageStateDTO>()
                };

                foreach (var state in package.States)
                {
                    var stateDTO = new PackageStateDTO
                    {
                        Id = state.IdPackageStates,
                        State = state.State,
                        StateDatePackages = state.StateDatePackages
                    };
                    packageDTO.States.Add(stateDTO);
                }

                return packageDTO;
            }

        }

}
