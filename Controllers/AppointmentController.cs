using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniEMR.Enums;
using MiniEMR.Models.AppointmentModels;
using MiniEMR.Services.Interfaces;

namespace MiniEMR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController(IAppointmentService appointmentService) : BaseAPIController
    {
        [HttpGet("dashboard")]
        [ProducesResponseType(typeof(AppointmentDashboardResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AppointmentDashboardResponse>> GetDashboard([FromQuery] DateTime date, [FromQuery] AppointmentStatus? status)

        {
            var result = await appointmentService.GetAppointmentsAsync(date, status, CurrentUserId);
            return Ok(result);
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(AppointmentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AppointmentResponse>> CreateAppointment(
            [FromBody] CreateAppointment request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await appointmentService.CreateAppointmentAsync(request, CurrentUserId);
            return Ok(result);
        }

        [HttpPost("{id}/checkin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CheckInAppointment(int id)
        {
            await appointmentService.CheckInAppointmentAsync(id, CurrentUserId);
            return Ok(new
            {
                Message = "Appointment checked in successfully."
            });
        }

        [HttpPost("{id}/cancel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelAppointment(int id, [FromBody] CancelAppointmentRequest request)

        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await appointmentService.CancelAppointmentAsync(id, request, CurrentUserId);
            return Ok(new
            {
                Message = "Appointment cancelled successfully."
            });
        }

        [HttpGet("available-slots")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAvailableSlots([FromQuery] int doctorId, [FromQuery] DateTime date)
        {
            var result = await appointmentService.GetAvailableSlotsAsync(doctorId, date);
            return Ok(result);
        }
    }
}