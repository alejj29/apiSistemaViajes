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
    public class PeriodsController : Controller
    {
        private readonly PeriodsService _PeriodsService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PeriodsController(PeriodsService Periodservice, IWebHostEnvironment hostingEnvironment)
        {
            _PeriodsService = Periodservice;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] PeriodsCreateRequest request)
        {
          

            var result = await _PeriodsService.Create(request);

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
        public async Task<IActionResult> Update([FromBody] PeriodsUpdateRequest request)
        {
            var result = await _PeriodsService.Update(request);

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
            var result = await _PeriodsService.Delete(id);

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
       
        [HttpGet("GetAllByCompanyId/{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            var devices = await _PeriodsService.GetAll(id);
            return Ok(devices);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var device = await _PeriodsService.GetById(id);
            return Ok(device);
        }

        [HttpPost("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus([FromBody] PeriodsUpdateStatusRequest request)
        {
            var result = await _PeriodsService.UpdateStatus(request);

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

        [HttpPost("UpdateStatusProgramming")]
        public async Task<IActionResult> UpdateStatusProgramming([FromBody] PeriodsUpdateStatusProgrammingRequest request)
        {
            var result = await _PeriodsService.UpdateStatusProgramming(request);

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

    }
}
