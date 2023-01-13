using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleMicroservice.Application.DTO.Appointments;
using ScheduleMicroservice.Application.DTO.Result;
using ScheduleMicroservice.Application.Service.Abstractions;
using ScheduleMicroservice.Domain.Entities.Enums;

namespace ScheduleMicroService.Controllers;

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

    [HttpGet("Patient/{patientId}")]
    [Authorize(Roles = nameof(UserRole.Patient))]
    public async Task<IActionResult> GetAsPatient(string patientId)
    {
        var appointments = await _appointmentsService.GetAsPatientAsync(patientId);
        if (appointments == null)
            return NotFound();
        return Ok(appointments);
    }

    [HttpGet("Doctor/{doctorId}")]
    [Authorize(Roles = nameof(UserRole.Doctor))]
    public async Task<IActionResult> GetAsDoctor(string doctorId)
    {
        var appointments = await _appointmentsService.GetAsDoctorAsync(doctorId);
        if (appointments == null)
            return NotFound();
        return Ok(appointments);
    }

    [HttpGet]
    [Authorize(Roles = nameof(UserRole.Receptionist))]
    public async Task<IActionResult> GetAll()
    {
        var appointments = await _appointmentsService.GetAsync();
        return Ok(appointments);
    }

    [HttpPost]
    [Authorize(Roles = $"{nameof(UserRole.Patient)},{nameof(UserRole.Receptionist)}")]
    public async Task<IActionResult> Create(AppointmentForCreatedDto model)
    {
        var appointment = await _appointmentsService.CreateAsync(model);
        if (appointment == null)
            return BadRequest();
        return Created("", appointment);
    }

    [HttpPut("{appointmentId}")]
    [Authorize(Roles = nameof(UserRole.Receptionist))]
    public async Task<IActionResult> ChangeAppointmentStatus(string appointmentId)
    {
        await _appointmentsService.ApproveStatusAsync(appointmentId);
        return NoContent();
    }

    [HttpDelete("{appointmentId}")]
    [Authorize(Roles = nameof(UserRole.Receptionist))]
    public async Task<IActionResult> Delete(string appointmentId)
    {
        await _appointmentsService.DeleteAsync(appointmentId);
        return NoContent();
    }

    [HttpPut("{appointmentId}/reschedule")]
    [Authorize(Roles = $"{nameof(UserRole.Patient)},{nameof(UserRole.Receptionist)}")]
    public async Task<IActionResult> RescheduleAppointment(string appointmentId, AppointmentsForRescheduleDto model)
    {
        await _appointmentsService.RescheduleAppointmentAsync(appointmentId, model);
        return NoContent();
    }

    #endregion

    #region Result

    [HttpGet("{appointmentId}/result")]
    [Authorize(Roles = $"{nameof(UserRole.Patient)}, {nameof(UserRole.Doctor)}")]
    public async Task<IActionResult> GetResult(string appointmentId)
    {
        var result = await _appointmentsService.GetAppointmentWithResultAsync(appointmentId);
        return Ok(result);
    }

    [HttpPost("{appointmentId}/result")]
    [Authorize(Roles = nameof(UserRole.Doctor))]
    public async Task<IActionResult> CreateResult(string appointmentId, ResultForCreatedDto model)
    {
        var appointment = await _appointmentsService.GetByIdAsync(appointmentId);
        if (appointment == null)
            return NotFound();

        await _resultService.CreateAsync(model);

        return NoContent();
    }

    [HttpPut("{appointmentId}/result/{resultId}")]
    [Authorize(Roles = nameof(UserRole.Doctor))]
    public async Task<IActionResult> UpdateResult(string appointmentId, string resultId, ResultForUpdateDto model)
    {
        var appointment = await _appointmentsService.GetByIdAsync(appointmentId);
        if (appointment == null)
            return BadRequest();

        await _resultService.UpdateAsync(resultId, model);

        return NoContent();
    }

    #endregion
}