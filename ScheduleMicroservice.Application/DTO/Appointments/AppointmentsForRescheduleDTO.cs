using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ScheduleMicroservice.Application.DTO.Appointments
{
    public class AppointmentsForRescheduleDTO
    {
        [Required]
        public Guid DoctorId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public TimeSpan Time { get; set; }
    }
}
