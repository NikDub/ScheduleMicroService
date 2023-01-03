using System.ComponentModel.DataAnnotations;

namespace ScheduleMicroservice.Application.DTO.Appointments;

public class AppointmentsForRescheduleDto
{
    [Required] public Guid DoctorId { get; set; }
    [Required] public DateTime Date { get; set; }
    [Required] public TimeSpan Time { get; set; }
}