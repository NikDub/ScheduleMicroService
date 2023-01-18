namespace ScheduleMicroservice.Domain.Entities.Models;

public class Result
{
    public string Id { get; set; }
    public string AppointmentId { get; set; }
    public string Complaints { get; set; }
    public string Conclusion { get; set; }
    public string Recommendations { get; set; }
}