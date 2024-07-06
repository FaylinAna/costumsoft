using CUSTOMSOFT.CORE.DTO;
using CUSTOMSOFT.INFRAESTRUCTURE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.INFRAESTRUCTURE.Data
{
    public interface IFileRepository
    {
        Task<FileDto> AddFile(FileDto fileDto);
        Task<List<FileDto>> GetFileByPackageId(int packageId);

    }
}
