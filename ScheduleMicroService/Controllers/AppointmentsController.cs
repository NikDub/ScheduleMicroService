using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleMicroservice.Application.DTO.Appointments;
using ScheduleMicroservice.Application.DTO.Result;
using ScheduleMicroservice.Application.Service.Abstractions;
using ScheduleMicroservice.Domain.Entities.Enums;

namespace ScheduleMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentsService _appointmentsService;
        private readonly IResultService _resultService;

        public AppointmentsController(IAppointmentsService appointmentsService, IResultService resultService)
        {
            _appointmentsService = appointmentsService;
            _resultService = resultService;
        }

        #region Appointments
        [HttpGet("Patient/{id}")]
        [Authorize(Roles = nameof(UserRole.Patient))]
        public async Task<IActionResult> GetAsPatient(string id)
        {
            var appointments = await _appointmentsService.GetAsPatientAsync(id);
            if (appointments == null)
                return NotFound();
            return Ok(appointments);
        }

        [HttpGet("Doctor/{id}")]
        [Authorize(Roles = nameof(UserRole.Doctor))]
        public async Task<IActionResult> GetAsDoctor(string id)
        {
            var appointments = await _appointmentsService.GetAsDoctorAsync(id);
            if (appointments == null)
                return NotFound();
            return Ok(appointments);
        }

        [HttpGet]
        [Authorize(Roles = nameof(UserRole.Receptionist))]
        public async Task<IActionResult> Get()
        {
            var appointments = await _appointmentsService.GetAsync();
            return Ok(appointments);
        }

        [HttpPost]
        [Authorize(Roles = $"{nameof(UserRole.Patient)},{nameof(UserRole.Receptionist)}")]
        public async Task<IActionResult> Create(AppointmentsForCreatedDTO model)
        {
            var appointment = await _appointmentsService.CreateAsync(model);
            if (appointment == null)
                return BadRequest();
            return Created("", appointment);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = nameof(UserRole.Receptionist))]
        public async Task<IActionResult> ApproveAppointment(string id)
        {
            await _appointmentsService.ChangeStatusAsync(id, true);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(UserRole.Receptionist))]
        public async Task<IActionResult> Delete(string id)
        {
            await _appointmentsService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}/reschedule")]
        [Authorize(Roles = $"{nameof(UserRole.Patient)},{nameof(UserRole.Receptionist)}")]
        public async Task<IActionResult> RescheduleAppointment(string id, AppointmentsForRescheduleDTO model)
        {
            await _appointmentsService.RescheduleAppointmentAsync(id, model);
            return NoContent();
        }
        #endregion

        #region Result
        [HttpGet("{id}/result")]
        [Authorize(Roles = $"{nameof(UserRole.Patient)}, {nameof(UserRole.Doctor)}")]
        public async Task<IActionResult> GetResult(string id)
        {
            var result = await _appointmentsService.GetAppointmentWithResultAsync(id);
            return Ok(result);
        }

        [HttpPost("{id}/result")]
        [Authorize(Roles = nameof(UserRole.Doctor))]
        public async Task<IActionResult> CreateResult(string id, ResultForCreatedDTO model)
        {
            if (id.ToLower() != model.AppointmentsId.ToString().ToLower())
                return BadRequest();

            var appointment = await _appointmentsService.GetByIdAsync(id);
            if (appointment == null)
                return BadRequest();

            await _resultService.CreateAsync(model);

            return NoContent();
        }

        [HttpPut("{id}/result/{resultid}")]
        [Authorize(Roles = nameof(UserRole.Doctor))]
        public async Task<IActionResult> UpdateResult(string id, string resultid, ResultForUpdateDTO model)
        {
            if (id.ToLower() != model.AppointmentsId.ToString().ToLower())
                return BadRequest();

            var appointment = await _appointmentsService.GetByIdAsync(id);
            if (appointment == null)
                return BadRequest();

            await _resultService.UpdateAsync(resultid, model);

            return NoContent();
        }

        #endregion
    }
}
