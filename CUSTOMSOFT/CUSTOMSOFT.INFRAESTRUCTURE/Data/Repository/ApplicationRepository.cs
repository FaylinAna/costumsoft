
using CUSTOMSOFT.CORE.DTO;
using CUSTOMSOFT.INFRAESTRUCTURE.Autentication;
using CUSTOMSOFT.INFRAESTRUCTURE.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace CUSTOMSOFT.INFRAESTRUCTURE.Data.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ConnectionSettings _connectionSettings;
        private readonly IConfiguration _configuration;
        private readonly string encripKeys;
        public ApplicationRepository(ConnectionSettings connectionSettings, IConfiguration configuration)
        {
            _connectionSettings = connectionSettings;
            _configuration = configuration;
        }
        public async Task<Aplicacion> GetByNameAsync(string aplicationName, string apiKey)
        {
            try { 
                    var Token = "";
                    var parameters = new DynamicParameters();
                    parameters.Add("p_nombre", aplicationName, DbType.String, ParameterDirection.Input);
                    parameters.Add("p_apiKey", apiKey, DbType.String, ParameterDirection.Input);

                    using (var connection = _connectionSettings.OpenSQLConnectionAsync())
                    {

                        var res = (List<Aplicacion>)await connection
                                                 .QueryAsync<Aplicacion>
                                                    ("SELECT * FROM  public.get_credencials_by_parameters(@p_nombre,@p_apiKey)", parameters);
                        res.First().ClientSecretKey = GenerateShortToken(res.First().PrivateKey, res.First().ClientSecretKey);

                        return res.First();


                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<Aplicacion> AddAsync(ApplicationDTO application)
        {
            try { 
                var newApplication = new ApplicationDTO(application.Name);
                using (var connection = _connectionSettings.OpenSQLConnectionAsync())
                {

                    var parameters = GetParameterList(newApplication);
                    var res = await connection.QueryAsync("SELECT add_application(@p_nombre, @p_apikey, @p_clientsecretkey, @p_privatekey)", parameters);
                }
                return new Aplicacion { Nombre = newApplication.Name,ApiKey = newApplication.ApiKey};
            }catch(Exception ex)
                {
                    throw ex;
                }
}

        public string GenerateShortToken(string privateKey, string clientSecretKey)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, privateKey),
                new Claim(ClaimTypes.Name, clientSecretKey),
            };

            var dd = _configuration.GetSection("Jwt:Key").Value;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var Sectored = new JwtSecurityToken(_configuration.GetSection("Jwt:Issuer").Value,
              _configuration.GetSection("Jwt:Issuer").Value,
              claims,
              expires: DateTime.Now.AddHours(1),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(Sectored);
        }
        public static DynamicParameters GetParameterList(ApplicationDTO aplicacion)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_nombre", aplicacion.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("p_apikey", aplicacion.ApiKey, DbType.String, ParameterDirection.Input);
            parameters.Add("p_clientsecretkey", aplicacion.ClientSecretKey, DbType.String, ParameterDirection.Input);
            parameters.Add("p_privatekey", aplicacion.PrivateKey, DbType.String, ParameterDirection.Input);

            return parameters;
        }

        public Task<Aplicacion> GetByApiKeyAsync(string apiKey)
        {
            throw new NotImplementedException();
        }

    }
}
