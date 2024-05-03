


using CUSTOMSOFT.CORE.DTO;
using CUSTOMSOFT.INFRAESTRUCTURE.Models;

namespace CUSTOMSOFT.INFRAESTRUCTURE.Data
{
    public interface IApplicationRepository
    {
        Task<Aplicacion> AddAsync( ApplicationDTO application);
        Task<Aplicacion> GetByNameAsync(string aplicationName,string apiKey);
        Task<Aplicacion> GetByApiKeyAsync(string apiKey);
    }
}
