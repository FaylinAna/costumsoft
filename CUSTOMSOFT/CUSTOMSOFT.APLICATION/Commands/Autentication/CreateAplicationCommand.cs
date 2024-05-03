
using CUSTOMSOFT.CORE.DTO;
using CUSTOMSOFT.INFRAESTRUCTURE.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.APLICATION.Commands.Autentication
{
   
    public class CreateAplicationCommand : IRequest<ApplicationDTO>
    {
        public string Nombre { get; set; }
        public string ApiKey { get; set; }
        public string ClientSecretKey { get; set; }
        public string PrivateKey { get; set; }
        public bool Activo { get; set; }

        public CreateAplicationCommand(ApplicationDTO aplicacion)
        {
            Nombre = aplicacion.Name;
            PrivateKey = aplicacion.PrivateKey;
            ClientSecretKey = aplicacion.ClientSecretKey;
            ApiKey = aplicacion.ApiKey;
            Activo = true;


        }
    }
}
