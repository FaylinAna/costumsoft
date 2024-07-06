using CUSTOMSOFT.CORE.DTO;
using CUSTOMSOFT.INFRAESTRUCTURE.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.INFRAESTRUCTURE.Data.Repository
{
    public class FilesRepository : IFileRepository
    {
        private readonly ConnectionSettings _connectionSettings;

        public FilesRepository(ConnectionSettings connectionSettings)
        {
            _connectionSettings = connectionSettings;
        }

        public async Task<List<FileDto>> GetFileByPackageId(int packageId)
        {
            try
            {
                using var connection = _connectionSettings.OpenSQLConnectionAsync();
                var parameters = new DynamicParameters();
                parameters.Add("p_PackageId", packageId, DbType.Int16, ParameterDirection.Input);
                var res = await connection.QueryFirstAsync<FileDto>("SELECT getFilesByPackageId(@p_PackageId)", parameters);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<FileDto> AddFile(FileDto fileDto)
        {
            try {
                using var connection = _connectionSettings.OpenSQLConnectionAsync();

                var parameters = GetParameterList(fileDto);
                var res = await connection.QueryFirstAsync("SELECT addFile(@p_FileName, @p_FileType, @p_FilePath,@p_PackageId)", parameters);
                return fileDto;
            }catch(Exception ex)
            {
                throw ex;
            }
           
        }

        public static DynamicParameters GetParameterList(FileDto fileDto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_FileName", fileDto.FileName, DbType.String, ParameterDirection.Input);
            parameters.Add("p_FileType", fileDto.FileType, DbType.String, ParameterDirection.Input);
            parameters.Add("p_FilePath", fileDto.FilePath, DbType.String, ParameterDirection.Input);
            parameters.Add("p_PackageId", fileDto.PackageId, DbType.Int16, ParameterDirection.Input);

            return parameters;
        }




    }
}
