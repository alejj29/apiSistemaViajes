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
    public class ViajeController : Controller
    {
        private readonly ViajeService _ViajeService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ViajeController(ViajeService ViajeService, IWebHostEnvironment hostingEnvironment)
        {
            _ViajeService = ViajeService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ViajeCreateRequest request)
        {
          

            var result = await _ViajeService.Create(request);

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
        public async Task<IActionResult> Update([FromBody] ViajeUpdateRequest request)
        {
            var result = await _ViajeService.Update(request);

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
            var result = await _ViajeService.Delete(id);

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
       
        [HttpGet("GetAllViaje/{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            var devices = await _ViajeService.GetAll(id);
            return Ok(devices);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var device = await _ViajeService.GetById(id);
            return Ok(device);
        }



    }
}
