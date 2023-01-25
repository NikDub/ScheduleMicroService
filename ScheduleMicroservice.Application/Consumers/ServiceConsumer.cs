using MassTransit;
using ScheduleMicroservice.Infrastructure.Repository.Abstractions;
using SharedModel;

namespace ScheduleMicroservice.Application.Consumers
{
    public class ServiceConsumer : IConsumer<ServiceMessage>
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        public ServiceConsumer(IAppointmentsRepository appointmentsRepository)
        {
            _appointmentsRepository = appointmentsRepository;
        }

        public async Task Consume(ConsumeContext<ServiceMessage> context)
        {
            var message = context.Message;
            await _appointmentsRepository.UpdateServiceNameAsync(message.Id, message.ServiceName);
        }
    }
}
