using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using FormSubmissionApi.Services;

namespace FormSubmissionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubmissionsController : ControllerBase
    {
        private readonly ISubmissionService _submissionService;

        public SubmissionsController(ISubmissionService submissionService)
        {
            _submissionService = submissionService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JsonElement data)
        {
            try
            {
                var submission = await _submissionService.AddSubmissionAsync(data);
                return Ok(new { message = "Submission saved", id = submission.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error saving submission: {ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var submissions = await _submissionService.GetAllSubmissionsAsync();
                return Ok(submissions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error retrieving submissions: {ex.Message}" });
            }
        }
    }
}