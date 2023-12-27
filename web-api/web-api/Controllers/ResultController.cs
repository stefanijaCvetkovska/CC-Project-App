using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Mime;
using System.Text;
using web_api.Models;
using web_api.Services;

namespace web_api.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class ResultController : ControllerBase
    {
        private readonly ResultService _resultService;

        public ResultController(ResultService resultService)
        {
            _resultService = resultService;
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculateSum([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new Result
                { Status = 1, Message = "Wrong input format. Please provide a valid file." });
            }

            var result = await _resultService.CalculateAsync(file);

            if (result.Status.Equals(0))
            {
                return Ok(result);
            }
            else if (result.Status.Equals(1))
            {
                return BadRequest(result);
            }
            else
            {
                return StatusCode(500, result);
            }
        }

    }
}
