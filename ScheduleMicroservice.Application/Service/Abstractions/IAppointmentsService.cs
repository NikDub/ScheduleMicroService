using ScheduleMicroservice.Application.DTO.Appointments;

namespace ScheduleMicroservice.Application.Service.Abstractions;

public interface IAppointmentsService
{
    Task<List<AppointmentsDto>> GetAsync();
    Task<List<AppointmentsDto>> GetAsPatientAsync(string id);
    Task<List<AppointmentsDto>> GetAsDoctorAsync(string id);
    Task<AppointmentsDto> GetByIdAsync(string id);
    Task<AppointmentsDto> CreateAsync(AppointmentForCreatedDto model);
    Task DeleteAsync(string id);
    Task ApproveStatusAsync(string id);
    Task<AppointmentsWithResultDto> GetAppointmentWithResultAsync(string id);
    Task RescheduleAppointmentAsync(string id, AppointmentsForRescheduleDto model);
}