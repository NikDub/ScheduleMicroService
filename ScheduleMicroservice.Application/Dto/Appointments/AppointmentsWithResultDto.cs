namespace ScheduleMicroservice.Application.DTO.Appointments;

public class AppointmentsWithResultDto
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid ServiceId { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public bool Status { get; set; }

    public string ServiceName { get; set; }
    public string DoctorFirstName { get; set; }
    public string DoctorLastName { get; set; }
    public string DoctorMiddleName { get; set; }
    public string PatientFirstName { get; set; }
    public string PatientLastName { get; set; }
    public string PatientMiddleName { get; set; }

    public Guid ResultId { get; set; }
    public string Complaints { get; set; }
    public string Conclusion { get; set; }
    public string Recommendations { get; set; }
}