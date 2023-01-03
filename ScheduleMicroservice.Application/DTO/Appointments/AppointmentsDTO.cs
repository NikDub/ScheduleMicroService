namespace ScheduleMicroservice.Application.DTO.Appointments;

public class AppointmentsDto
{
    public string Id { get; set; }
    public string PatientId { get; set; }
    public string DoctorId { get; set; }
    public string ServiceId { get; set; }
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
}