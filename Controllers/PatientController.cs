using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniEMR.Models.PatientModels;
using MiniEMR.Services.Interfaces;

namespace MiniEMR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController(IPatientService patientService) :BaseAPIController
    {
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SearchPatients([FromQuery] string term)
        {
            var results = await patientService.SearchPatientsAsync(term);

            return Ok(results);
        }

        [HttpGet("all-patients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPatients()
        {
            var patients = await patientService.GetPatientsAsync();

            return Ok(patients);
        }

        [HttpGet("{patientId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPatientDetail(int patientId)
        {
            var patient = await patientService.GetPatientDetailAsync(patientId);
            if (patient == null)
            {
                return NotFound(new
                {
                    Message = $"Patient with Id {patientId} not found."
                });
            }

            return Ok(patient);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatient model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await patientService.CreatePatientAsync(model, CurrentUserId);

            return Ok(new
            {
                Message = "Patient created successfully."
            });
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePatient([FromBody] UpdatePatient model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await patientService.UpdatePatientAsync(model);
            return Ok(new
            {
                Message = "Patient updated successfully."
            });
        }
    }
}