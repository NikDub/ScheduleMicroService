namespace ScheduleMicroservice.Application.DTO.Result
{
    public class ResultDTO
    {
        public Guid Id { get; set; }
        public Guid AppointmentsId { get; set; }
        public string Complaints { get; set; }
        public string Conclusion { get; set; }
        public string Recommendations { get; set; }
    }
}
