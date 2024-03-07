using Azure.Core;
using Control14.NetCore.BusinessLogic.Services;
using Control14.NetCore.Domain;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace Control14.NetCore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CiudadesController : Controller
    {
        private readonly CiudadesService _CiudadesService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CiudadesController(CiudadesService CiudadesService, IWebHostEnvironment hostingEnvironment)
        {
            _CiudadesService = CiudadesService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CiudadesCreateRequest request)
        {
          

            var result = await _CiudadesService.Create(request);

            if (result.Success)
            {
                // Success
                return Ok(new { message = result.Message });
            }
            else
            {
                // Failure
                return BadRequest(new { message = result.Message });
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] CiudadesUpdateRequest request)
        {
            var result = await _CiudadesService.Update(request);

            if (result.Success)
            {
                // Success
                return Ok(new { message = result.Message });
            }
            else
            {
                // Failure
                return BadRequest(new { message = result.Message });
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _CiudadesService.Delete(id);

            if (result.Success)
            {
                // Success
                return Ok(new { message = result.Message });
            }
            else
            {
                // Failure
                return BadRequest(new { message = result.Message });
            }
        }
       
        [HttpGet("GetAllCiudades/{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            var devices = await _CiudadesService.GetAll(id);
            return Ok(devices);
        }


        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var device = await _CiudadesService.GetById(id);
            return Ok(device);
        }


    }
}
