using CUSTOMSOFT.CORE.DTO;

namespace CUSTOMSOFT.API.Helpers.Interfaces
{
    public interface IFileService
    {
        Task<FileDto> SaveFileAsync(IFormFile file,int packageId);

    }
}
