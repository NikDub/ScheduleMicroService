using ScheduleMicroservice.Domain.Entities.Models;

namespace ScheduleMicroservice.Application.Repository.Abstractions
{
    public interface IAppointmentsRepository
    {
        Task<List<Appointment>> GetAsync();
        Task<Appointment> GetByIdAsync(string id);
        Task<Appointment> CreateAsync(Appointment model);
        Task UpdateAsync(string id, Appointment model);
        Task DeleteAsync(string id);
        Task<List<Appointment>> GetAsPatientAsync(string id);
        Task<List<Appointment>> GetAsDoctorAsync(string id);
        Task ChangeStatusAsync(string id, bool status);
        Task<Appointment> GetAppintmentWithResult(string id);
    }
}
