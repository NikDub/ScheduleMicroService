using MassTransit;
using ScheduleMicroservice.Infrastructure.Repository.Abstractions;
using SharedModel;

namespace ScheduleMicroservice.Application.Consumers
{
    public class ProfileDoctorConsumer : IConsumer<DoctorMessage>
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        public ProfileDoctorConsumer(IAppointmentsRepository appointmentsRepository)
        {
            _appointmentsRepository = appointmentsRepository;
        }

        public async Task Consume(ConsumeContext<DoctorMessage> context)
        {
            var message = context.Message;
            await _appointmentsRepository.UpdateDoctorNameAsync(message.Id, message.FirstName, message.LastName, message.MiddleName);
        }
    }
}
