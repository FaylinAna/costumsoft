using CUSTOMSOFT.API.Helpers.Interfaces;
using CUSTOMSOFT.API.Models;
using CUSTOMSOFT.APLICATION.Commands.Files;
using CUSTOMSOFT.APLICATION.Commands.Package;
using CUSTOMSOFT.CORE.DTO;
using CUSTOMSOFT.INFRAESTRUCTURE.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Runtime.Serialization.Formatters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CUSTOMSOFT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IFileService _fileService;
        public FileController(IMediator mediator, IFileService fileService)
        {
            _mediator = mediator;
            _fileService = fileService;
        }

        // GET: api/<FileController>
        [HttpGet("action")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FileController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FileController>
        [HttpPost("[action]")]
        public async void UploadFile(IFormFile fieldInfo)
        {

            try
            {
                var packageId = Request.Headers["X-Package-ID"];
                var fileDto = await _fileService.SaveFileAsync(fieldInfo, int.Parse(packageId));
                var response = await _mediator.Send(new AddFileCommand(fileDto));

            }
            catch(Exception ex)
            {

            }

        }

        // PUT api/<FileController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FileController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
