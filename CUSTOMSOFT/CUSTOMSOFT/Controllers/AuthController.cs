using CUSTOMSOFT.API.Filters;
using CUSTOMSOFT.API.Models;
using CUSTOMSOFT.APLICATION.Commands.Autentication;
using CUSTOMSOFT.APLICATION.Queries;
using CUSTOMSOFT.CORE.DTO;
using CUSTOMSOFT.CORE.Interfaces;
using CUSTOMSOFT.INFRAESTRUCTURE.Autentication;
using CUSTOMSOFT.INFRAESTRUCTURE.Data;
using CUSTOMSOFT.INFRAESTRUCTURE.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace CUSTOMSOFT.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {

        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAplication([FromBody] string appName)
        {
            if (string.IsNullOrEmpty(appName))
            {
                return BadRequest($"{nameof(appName)} is required");
            }

            try
            {
                var aplicationDetail = await _mediator.Send(new CreateAplicationCommand(new ApplicationDTO(appName)));

                return Ok(aplicationDetail);
            }
            catch (Exception ex)
            {
                var errorDetails = new ErrorDetails(ex);
                return StatusCode((int)errorDetails.StatusCode, errorDetails);
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetToken([FromBody] ApplicationModel application)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Application object is required");
            }

            try
            {
                if (application is null)
                {
                    return Unauthorized();
                }
                var query = new AplicationNameQueries
                {
                    Name = application.Nombre,
                    ApiKey = application.ApiKey
                };

                var response = await _mediator.Send(new AplicationNameQueries() { Name = application.Nombre, ApiKey = application.ApiKey });

                return Ok(new TokenModel { AccessToken = response.ClientSecretKey, ExpiresIn = 1024, TokenType = "bearer" });
            }
            catch (Exception ex)
            {
                var errorDetails = new ErrorDetails(ex);
                return StatusCode((int)errorDetails.StatusCode, errorDetails);
            }
        }
    }
}
