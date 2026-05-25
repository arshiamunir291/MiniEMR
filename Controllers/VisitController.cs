using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniEMR.Models.VisitModels;
using MiniEMR.Services.Interfaces;

namespace MiniEMR.Controllers
{
    [Authorize(Roles = "Doctor")]
    [Route("api/[controller]")]
    [ApiController]
    public class VisitController(IVisitService visitService) : BaseAPIController
    {
        [HttpGet("start/{appointmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> StartVisit(int appointmentId)
        {
            var result = await visitService.StartVisitAsync(appointmentId, CurrentUserId);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveVisit([FromBody] SaveVisit model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await visitService.SaveVisitAsync(model, CurrentUserId);
            return Ok(new
            {
                Message = "Visit saved successfully."
            });
        }

        [HttpGet("drugs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetDrugs()
        {
            var result = await visitService.GetDrugsAsync();
            return Ok(result);
        }

        [HttpGet("start-emergency/{patientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> StartEmergencyVisit(int patientId)
        {
            var visit = await visitService.StartEmergencyVisitAsync(patientId, CurrentUserId);
            return Ok(visit);
        }
    }
}