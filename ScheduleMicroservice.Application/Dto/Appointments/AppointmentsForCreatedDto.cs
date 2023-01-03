using System.ComponentModel.DataAnnotations;

namespace ScheduleMicroservice.Application.DTO.Appointments;

public class AppointmentsForCreatedDto
{
    [Required] public Guid PatientId { get; set; }
    [Required] public Guid DoctorId { get; set; }
    [Required] public Guid ServiceId { get; set; }
    [Required] public DateTime Date { get; set; }
    [Required] public TimeSpan Time { get; set; }
    public bool Status { get; set; } = false;

    public string ServiceName { get; set; }
    public string DoctorFirstName { get; set; }
    public string DoctorLastName { get; set; }
    public string DoctorMiddleName { get; set; }
    public string PatientFirstName { get; set; }
    public string PatientLastName { get; set; }
    public string PatientMiddleName { get; set; }
}