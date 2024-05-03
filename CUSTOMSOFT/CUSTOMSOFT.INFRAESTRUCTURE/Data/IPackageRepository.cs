using CUSTOMSOFT.CORE.DTO;
using CUSTOMSOFT.INFRAESTRUCTURE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.INFRAESTRUCTURE.Data
{
    public interface IPackageRepository
    {
        Task<Package> GetByTrackingNumberAsync(string trackingNumber);
        Task<List<Package>> GetAllPackages();
        Task<Package> AddPackage(PackageDTO package);
        Task<Package> UpdatePackage(PackageDTO package);

       
    }
}
