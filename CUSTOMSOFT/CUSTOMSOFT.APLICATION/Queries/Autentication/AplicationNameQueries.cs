
using CUSTOMSOFT.INFRAESTRUCTURE.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.APLICATION.Queries
{
    public class AplicationNameQueries : IRequest<Aplicacion>
    {
        public string Name { get; set; }
        public string ApiKey { get; set; }
    }
}
