
using CUSTOMSOFT.INFRAESTRUCTURE.Data;
using CUSTOMSOFT.INFRAESTRUCTURE.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CUSTOMSOFT.APLICATION.Queries
{
    public class AplicationNameHandler : IRequestHandler<AplicationNameQueries, Aplicacion>
    {
        private readonly IApplicationRepository _iApplicationRepository;

        public AplicationNameHandler(IApplicationRepository iApplicationRepository)
        {
            _iApplicationRepository = iApplicationRepository;
        }

        public async Task<Aplicacion> Handle(AplicationNameQueries request, CancellationToken cancellationToken)
        {
           var res = await _iApplicationRepository.GetByNameAsync(request.Name,request.ApiKey);

            return res;

        }
    }
}
