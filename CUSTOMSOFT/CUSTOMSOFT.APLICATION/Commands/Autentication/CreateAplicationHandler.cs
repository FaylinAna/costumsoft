using CUSTOMSOFT.CORE.DTO;
using CUSTOMSOFT.INFRAESTRUCTURE.Data;
using CUSTOMSOFT.INFRAESTRUCTURE.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.APLICATION.Commands.Autentication
{
    public class CreateAplicationHandler : IRequestHandler<CreateAplicationCommand, ApplicationDTO>
    {
        private readonly IApplicationRepository _iApplicationRepository;

        public CreateAplicationHandler(IApplicationRepository iApplicationRepository)
        {
            _iApplicationRepository = iApplicationRepository;
        }

        public async Task<ApplicationDTO> Handle(CreateAplicationCommand command, CancellationToken cancellationToken)
        {
            var AplicationDetails = new ApplicationDTO(command.Nombre);
            var res = await _iApplicationRepository.AddAsync(AplicationDetails);
            return new ApplicationDTO(command.Nombre);

        }


    }
}
