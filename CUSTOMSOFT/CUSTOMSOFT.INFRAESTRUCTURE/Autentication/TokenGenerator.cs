
using CUSTOMSOFT.CORE.DTO;
using CUSTOMSOFT.CORE.Interfaces;
using CUSTOMSOFT.INFRAESTRUCTURE.Data.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CUSTOMSOFT.INFRAESTRUCTURE.Autentication
{
    public class TokenGenerator : ITokenGenerator
    {
        public IConfiguration _configuration;

        public TokenGenerator(IConfiguration configuration)
        { _configuration = configuration; }

        public string GenerateJwt(ApplicationDTO application)
        {
            throw new NotImplementedException();
        }

        public string GenerateShortToken(string privateKey, string clientSecretKey)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, privateKey),
                new Claim(ClaimTypes.Name, clientSecretKey),
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));       
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var Sectoken = new JwtSecurityToken(_configuration.GetSection("Jwt:Issuer").Value,
              _configuration.GetSection("Jwt:Issuer").Value,
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(Sectoken);
        }

    

        
    }
}
