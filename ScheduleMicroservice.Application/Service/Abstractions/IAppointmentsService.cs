using ScheduleMicroservice.Application.DTO.Appointments;

namespace ScheduleMicroservice.Application.Service.Abstractions;

public interface IAppointmentsService
{
    Task<List<AppointmentsDto>> GetAsync();
    Task<List<AppointmentsDto>> GetAsPatientAsync(Guid id);
    Task<List<AppointmentsDto>> GetAsDoctorAsync(Guid id);
    Task<AppointmentsDto> GetByIdAsync(Guid id);
    Task<AppointmentsDto> CreateAsync(AppointmentForCreatedDto model);
    Task DeleteAsync(Guid id);
    Task ApproveStatusAsync(Guid id);
    Task<AppointmentsWithResultDto> GetAppointmentWithResultAsync(Guid id);
    Task RescheduleAppointmentAsync(Guid id, AppointmentsForRescheduleDto model);
}