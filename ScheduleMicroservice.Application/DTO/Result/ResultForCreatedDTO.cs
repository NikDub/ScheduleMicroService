using System.ComponentModel.DataAnnotations;

namespace ScheduleMicroservice.Application.DTO.Result;

public class ResultForCreatedDto
{
    [Required] public Guid AppointmentId { get; set; }
    [Required] public string Complaints { get; set; }
    [Required] public string Conclusion { get; set; }
    [Required] public string Recommendations { get; set; }
}