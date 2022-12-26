using ScheduleMicroservice.Application.DTO.Appointments;

namespace ScheduleMicroservice.Application.Service.Abstractions
{
    public interface IAppointmentsService
    {
        Task<List<AppointmentsDTO>> GetAsync();
        Task<List<AppointmentsDTO>> GetAsPatientAsync(string id);
        Task<List<AppointmentsDTO>> GetAsDoctorAsync(string id);
        Task<AppointmentsDTO> GetByIdAsync(string id);
        Task<AppointmentsDTO> CreateAsync(AppointmentsForCreatedDTO model);
        Task UpdateAsync(string id, AppointmentsForUpdateDTO model);
        Task DeleteAsync(string id);
        Task ChangeStatusAsync(string id, bool status);
        Task<AppointmentsWithResultDTO> GetAppointmentWithResultAsync(string id);
        Task RescheduleAppointmentAsync(string id, AppointmentsForRescheduleDTO model);
    }
}
