namespace ScheduleMicroservice.Domain.Entities.Models;

public class Result
{
    public Guid Id { get; set; }
    public Guid AppointmentId { get; set; }
    public string Complaints { get; set; }
    public string Conclusion { get; set; }
    public string Recommendations { get; set; }
}