using MassTransit;
using ScheduleMicroservice.Infrastructure.Repository.Abstractions;
using SharedModel;

namespace ScheduleMicroservice.Application.Consumers
{
    public class ProfilePatientConsumer : IConsumer<PatientMessage>
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        public ProfilePatientConsumer(IAppointmentsRepository appointmentsRepository)
        {
            _appointmentsRepository = appointmentsRepository;
        }

        public async Task Consume(ConsumeContext<PatientMessage> context)
        {
            var message = context.Message;
            await _appointmentsRepository.UpdatePatientNameAsync(message.Id, message.FirstName, message.LastName, message.MiddleName);
        }
    }
}
