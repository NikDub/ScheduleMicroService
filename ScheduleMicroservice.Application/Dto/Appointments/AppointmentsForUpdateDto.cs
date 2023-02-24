using ScheduleMicroservice.Domain.Entities.Enums;

namespace ScheduleMicroservice.Application.DTO.Appointments;

public class AppointmentsForUpdateDto
{
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid ServiceId { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public Duration Duration { get; set; }

    public string ServiceName { get; set; }
    public string DoctorFirstName { get; set; }
    public string DoctorLastName { get; set; }
    public string DoctorMiddleName { get; set; }
    public string PatientFirstName { get; set; }
    public string PatientLastName { get; set; }
    public string PatientMiddleName { get; set; }
}