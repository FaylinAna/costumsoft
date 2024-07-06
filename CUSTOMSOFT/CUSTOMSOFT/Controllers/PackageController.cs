using CUSTOMSOFT.API.Models;
using CUSTOMSOFT.APLICATION.Commands.Autentication;
using CUSTOMSOFT.APLICATION.Commands.Package;
using CUSTOMSOFT.APLICATION.Queries;
using CUSTOMSOFT.APLICATION.Queries.Package;
using CUSTOMSOFT.CORE.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Dapper.SqlMapper;
using static System.Net.Mime.MediaTypeNames;

namespace CUSTOMSOFT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PackageController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetPackagesByTrackingNumber")]
        public async Task<IActionResult> GetPackagesByTrackingNumber(string trackingNumber)
        {
            try
            {
                var response = await _mediator.Send(new GetFileByPackageIdQuery() { TrackingNumber = trackingNumber });
                return Ok(response);

            }catch (Exception ex)
            {
                var errorDetails = new ErrorDetails(ex);
                errorDetails.Title = "Validation Error, No content";
                return StatusCode(204, errorDetails);
            }
            
        }

        [HttpGet("GetAllPackages")]
        public async Task<IActionResult> GetAllPackages()
        {
            try
            {
                var response = await _mediator.Send(new GetAllPackagesQuery());
                return Ok(response);

            }
            catch (Exception ex)
            {
                var errorDetails = new ErrorDetails(ex);
                return StatusCode((int)errorDetails.StatusCode, errorDetails);
            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddPackage([FromBody] PackageAddModel package)
        {
            if (package is null)
            {
                return BadRequest("package object is required");
            }

            try
            {
                var response = await _mediator.Send(new AddPackageCommand(new PackageDTO{
                    CustomerName = package.CustomerName,
                    DeliveryAddress = package.DeliveryAddress,
                    Weight = package.Weight,
                }));

                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorDetails = new ErrorDetails(ex);
                return StatusCode((int)errorDetails.StatusCode, errorDetails);

            }
        }


        [HttpPatch("[action]")]
        public async Task<IActionResult> UpdatePackage([FromBody] PackageAddModel package)
        {
            if (package is null)
            {
                return BadRequest("package object is required");
            }

            try
            {
                var response = await _mediator.Send(new UpdatePackageCommand(new PackageDTO
                {
                    Id = (int)package.Id,
                    CustomerName = package.CustomerName,
                    
                    DeliveryAddress = package.DeliveryAddress,
                    Weight = package.Weight,
                    StateDescription = package.StateDescription,
                    
                }));

                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorDetails = new ErrorDetails(ex);
                return StatusCode((int)errorDetails.StatusCode, errorDetails);
            }
        }
    }
}
