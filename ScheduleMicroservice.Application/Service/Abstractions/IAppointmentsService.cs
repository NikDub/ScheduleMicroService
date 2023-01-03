using ScheduleMicroservice.Application.DTO.Appointments;

namespace ScheduleMicroservice.Application.Service.Abstractions;

public interface IAppointmentsService
{
    Task<List<AppointmentsDto>> GetAsync();
    Task<List<AppointmentsDto>> GetAsPatientAsync(string id);
    Task<List<AppointmentsDto>> GetAsDoctorAsync(string id);
    Task<AppointmentsDto> GetByIdAsync(string id);
    Task<AppointmentsDto> CreateAsync(AppointmentsForCreatedDto model);
    Task UpdateAsync(string id, AppointmentsForUpdateDto model);
    Task DeleteAsync(string id);
    Task ChangeStatusAsync(string id, bool status);
    Task<AppointmentsWithResultDto> GetAppointmentWithResultAsync(string id);
    Task RescheduleAppointmentAsync(string id, AppointmentsForRescheduleDto model);
}