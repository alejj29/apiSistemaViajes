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
    public class VehiculosController : Controller
    {
        private readonly VehiculosService _VehiculosService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public VehiculosController(VehiculosService VehiculosService, IWebHostEnvironment hostingEnvironment)
        {
            _VehiculosService = VehiculosService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] VehiculosCreateRequest request)
        {
          

            var result = await _VehiculosService.Create(request);

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
        public async Task<IActionResult> Update([FromBody] VehiculosUpdateRequest request)
        {
            var result = await _VehiculosService.Update(request);

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
            var result = await _VehiculosService.Delete(id);

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
       
        [HttpGet("GetAllVehiculo/{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            var devices = await _VehiculosService.GetAll(id);
            return Ok(devices);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var device = await _VehiculosService.GetById(id);
            return Ok(device);
        }



    }
}
