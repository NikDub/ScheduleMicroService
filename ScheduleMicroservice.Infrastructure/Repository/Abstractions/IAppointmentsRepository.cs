using ScheduleMicroservice.Domain.Entities.Models;

namespace ScheduleMicroservice.Infrastructure.Repository.Abstractions;

public interface IAppointmentsRepository
{
    Task<List<Appointment>> GetAsync();
    Task<Appointment> GetByIdAsync(Guid id);
    Task<Appointment> CreateAsync(Appointment model);
    Task UpdateAsync(Guid id, Appointment model);
    Task DeleteAsync(Guid id);
    Task<List<Appointment>> GetAsPatientAsync(Guid id);
    Task<List<Appointment>> GetAsDoctorAsync(Guid id);
    Task ChangeStatusAsync(Guid id, bool status);
    Task<Appointment> GetAppointmentWithResultAsync(Guid id);
    Task UpdateServiceNameAsync(Guid serviceId, string serviceName);
    Task UpdateDoctorNameAsync(Guid doctorId, string doctorFirstName, string doctorLastName, string doctorMiddleName);
    Task UpdatePatientNameAsync(Guid patientId, string patientFirstName, string patientLastName, string patientMiddleName);
    Task<List<Appointment>> GetWeeklyAsDoctorAsync(Guid doctorId);
}